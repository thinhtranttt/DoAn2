using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadAllInfo : MonoBehaviour {
    public static LoadAllInfo instance;

    public static Dictionary<int, PlayerData> playerDictionary = new Dictionary<int, PlayerData>();
    public static int maxDictionary;
    JsonData playerJson;

    public static string user;
    public static string pass;

    private int id = 2;
	// Use this for initialization
	void Start () {
        LoadPlayers();
        //AddPlayer();
        //Debug.Log(maxDictionary);
	}

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    // Update is called once per frame
    void Update () {
        
	}

    /// <summary>
    /// Read user from textdocument file in Json Files/
    /// </summary>
    /// <param name="id"></param>
    public void LoadPlayers()
    {
        int i = 0;
        while(i<maxDictionary)
        {
            playerDictionary.Remove(i);
            i++;
        }        
        int num = 0;
        bool readed = true;
        while (readed)
        {
            string path = Application.streamingAssetsPath + "/" + num + ".txt";
            FileInfo file = new FileInfo(path);
            //FileInfo file = new FileInfo(Application.dataPath + "/Resources/Json Files/" + num + ".txt");
            if(!file.Exists)
            {
                readed = false;
            }
            else
            {
                //PlayerData newPlayer = JsonUtility.FromJson<PlayerData>(Resources.Load<TextAsset>("Json Files/"+num).text);
                //BinaryFormatter bf = new BinaryFormatter();
                //FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
                
                string jsonString = File.ReadAllText(path);
                PlayerData newPlayer = JsonUtility.FromJson<PlayerData>(jsonString);
                playerDictionary.Add(newPlayer.id, newPlayer);
                print(JsonUtility.ToJson(newPlayer));
                num++;
            }
        }
        maxDictionary = num;
    }

    /// <summary>
    /// Save user in textdocument file.
    /// </summary>
    public void SavePlayer(PlayerData player)
    {
        playerJson = JsonMapper.ToJson(player);
        File.WriteAllText(Application.streamingAssetsPath +"/"+ player.id + ".txt", playerJson.ToString());
    }
    /*public void SaveAccount(int idUser, PlayerData.User user,PlayerData.Player player1)
    {     
        PlayerData player = new PlayerData(idUser,user,player1);
        playerJson = JsonMapper.ToJson(player);
        File.WriteAllText(Application.dataPath + "/Resources/Json Files/"+idUser+".txt", playerJson.ToString());
        Debug.Log(playerJson);
    }*/
}
