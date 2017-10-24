using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ControlObject;
using LitJson;
using System;
using System.IO;

public class UIManager : MonoBehaviour {
    public static int idOfPlayer;
    public static Dictionary<int, PlayerData> listUser = new Dictionary<int, PlayerData>();
    

    [Header("Object Preferrence")]
    public GameObject introLogoUI;    
    public GameObject backGround;
    public GameObject loginUI;
    public GameObject serverUI;
    public GameObject selectorPlayerUI;
    public GameObject errorFormUI;
    public Text introText;
    public Text userText;
    public Text passText;
    public Text maskOutput;
    public InputField PasswordInput;
    public GameObject Register;
    public GameObject RegisteredSuccessed;

    [Header("Select Player")]
    public Text player1;
    public Text player2;
    public Text player3;
    public Text player4;
    public Text locationMap;
    public Text userSelectText;
    public Text digiSelectText;
    public Text userLevelSelect;
    public Text digiLevelSelect;
    public InputField playerName;
    public InputField digiName;
    public GameObject btnCreate;
    public GameObject btnDelete;
    public GameObject infoAccount;
    public GameObject inputPlayer;
    public Text inputTextPlayer;
    public GameObject inputDigimon;
    public Text inputTextDigimon;
    public GameObject btnOkPlayer;
    public GameObject btnOkDigimon;
    public GameObject btnStart;
    public GameObject playerNext;
    public GameObject digimonNext;
    public GameObject btnOkInputDigimon;
    public GameObject btnOkInputPlayer;
    public GameObject btnBack;

    [Header("Test")]
    private string userName;
    private string passWord;
    private string[] maskArray = new string[16];
    int maskIndex = 0;
    private string digiNameUser;
    private string playerNameUser;
    private bool accountAvailable = false;
    private List<GameObject> players = ObjectControl.loadPlayerList;

    [Header("Using another script")]
    ObjectControl obj;
    LoadAllInfo systemManager;
    JsonData json;

    [Header("Register Form")]
    public GameObject userValid;
    public Text userRegister;
    public Text passRegister;
    public Text emailRegister;
    

    // Use this for initialization
    void Start () {
        CallFunctionWhenStar();
	}
	
    public void CallFunctionWhenStar()
    {
        ResetCanvas();
        FadeText();
        obj = GameObject.FindObjectOfType(typeof(ObjectControl)) as ObjectControl;
        listUser = LoadAllInfo.playerDictionary;
        //players = ObjectControl.playerList;
        //digimons = ObjectControl.digiList;
    }

	// Update is called once per frame
	void Update () {
        HideText();
	}


    /// <summary>
    /// set event for btnStart to start game.
    /// </summary>
    public void StartGame()
    {
        print(JsonUtility.ToJson(listUser[idOfPlayer]));
        Application.LoadLevel(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// set event for btnOK of Input name of digimon.
    /// </summary>
    public void OkInputDigimon()
    {
        digiNameUser = inputTextDigimon.text;
        digiSelectText.text = digiNameUser;
        digiLevelSelect.text = "Lv.1";
        infoAccount.SetActive(true);
        btnStart.SetActive(true);
        inputDigimon.SetActive(false);
        btnOkInputDigimon.SetActive(false);
        listUser[idOfPlayer].player.digimon.name = digiNameUser;
    }

    /// <summary>
    /// Set event for btnOkDigimon.
    /// </summary>
    public void OKDigimon()
    {
        obj.clickOKDigi();
        btnOkDigimon.SetActive(false);
        digimonNext.SetActive(false);
        inputDigimon.SetActive(true);
        btnOkInputDigimon.SetActive(true);
        listUser[idOfPlayer].player.digimon.id = ObjectControl.numDigi;
        listUser[idOfPlayer].player.digimon.avatarID = ObjectControl.numDigi;
    }

    /// <summary>
    /// Set event for btnOkInput when get name of player.
    /// </summary>
    public void OkInput()
    {
        playerNameUser = inputTextPlayer.text;
        userSelectText.text = playerNameUser;
        userLevelSelect.text = "Lv.1";
        player1.text = playerNameUser;
        btnOkDigimon.SetActive(true);
        digimonNext.SetActive(true);
        inputPlayer.SetActive(false);
        btnOkInputPlayer.SetActive(false);
        listUser[idOfPlayer].player.name = playerNameUser;
    }

    /// <summary>
    /// Set btn OKPlayer when it is clicked.
    /// </summary>
    public void OkPlayer()
    {
        obj.playerOK();
        btnOkPlayer.SetActive(false);
        playerNext.SetActive(false);
        inputPlayer.SetActive(true);
        btnOkInputPlayer.SetActive(true);
        listUser[idOfPlayer].player.id= ObjectControl.numPlayer;
        listUser[idOfPlayer].player.avatarID = ObjectControl.numPlayer;
    }

    /// <summary>
    /// Go next object to choose another player.
    /// </summary>
    public void NextPlayer()
    {
        obj.playerNext();
    }

    /// <summary>
    /// Go next object to choose another Digimon.
    /// </summary>
    public void NextDigimon()
    {
        obj.digiNext();
    }

    /// <summary>
    /// set event for btnCreate.
    /// </summary>
    public void CreateClicked()
    {
        btnCreate.SetActive(false);        
        playerNext.SetActive(true);
        btnOkPlayer.SetActive(true);
    }

    /// <summary>
    /// Set click on button User1.
    /// </summary>
    public void NewUser1Clicked()
    {
        string text = player1.GetComponent<Text>().text.ToString();
        if(text=="Create New")
        {
            btnCreate.SetActive(true);
        }
        else
        {
            btnStart.SetActive(true);
            obj.SetActivePlayer(listUser[idOfPlayer].player.id);
            obj.setActiveDigimon(listUser[idOfPlayer].player.digimon.id);
        }
        
    }

    /// <summary>
    /// Reset Select Sceen.
    /// </summary>
    public void ResetSelect()
    {
        inputPlayer.SetActive(false);
        inputDigimon.SetActive(false);
        btnCreate.SetActive(false);
        btnDelete.SetActive(false);
        infoAccount.SetActive(false);
        btnOkPlayer.SetActive(false);
        btnOkDigimon.SetActive(false);
        btnStart.SetActive(false);
        playerNext.SetActive(false);
        digimonNext.SetActive(false);
        btnOkInputDigimon.SetActive(false);
        btnOkInputPlayer.SetActive(false);
        btnBack.SetActive(false);
        
    }

    /// <summary>
    /// Reset all UI when game start.
    /// </summary>
    public void ResetCanvas()
    {
        introLogoUI.SetActive(true);
        backGround.SetActive(false);
        loginUI.SetActive(false);
        serverUI.SetActive(false);
        selectorPlayerUI.SetActive(false);
        errorFormUI.SetActive(false);
        Register.SetActive(false);
        RegisteredSuccessed.SetActive(false);
        userValid.SetActive(false);
    }

    /// <summary>
    /// Click ok button in server to go in select player Sceen.
    /// </summary>
    public void ClickOkServer()
    {
        repairSelectPlayer();
    }

    /// <summary>
    /// Reset all canvas in SelectPlayer Canvas.
    /// </summary>
    public void repairSelectPlayer()
    {
        serverUI.SetActive(false);
        backGround.SetActive(false);
        selectorPlayerUI.SetActive(true);
        ResetSelect();
        if(listUser[idOfPlayer].player.name==null|| listUser[idOfPlayer].player.name == "")
        {
            player1.text = "Create New";
            accountAvailable = false;
        }
        else
        {
            player1.text = listUser[idOfPlayer].player.name;
            accountAvailable = true;
        }
    }
    /// <summary>
    /// Click cancel button in server to go back login form.
    /// </summary>
    public void ClickCancelServer()
    {
        serverUI.SetActive(false);
        loginUI.SetActive(true);
    }

    /// <summary>
    /// Control text in InputField Password.
    /// </summary>
    public void MaskPasswordFunction()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            maskIndex--;
            maskOutput.text = maskArray[maskIndex];
        }else
        {
            maskIndex++;
            maskOutput.text = maskArray[maskIndex];
        }
    }

    /// <summary>
    /// Hide text in password InputField.
    /// </summary>
    public void HideText()
    {
        string active = loginUI.activeSelf.ToString();
        if (active=="True")
        {
            maskArray[0] = "";
            string mask = "";
            for (int count = 1; count <= 15; count++)
            {
                maskArray[count] = mask + "*";
                mask = mask + "*";
            }
        }
        
    }

    /// <summary>
    /// Event Register when click register on Register pannel.
    /// </summary>
    public void ButtonRegistered()
    {
        bool valid = false;
        for(int i=0;i<LoadAllInfo.maxDictionary;i++)
        {
            if(listUser[i].user.userName==userRegister.text||userRegister.text==""||emailRegister.text==""||passRegister.text=="")
            {
                valid = true;
            }
        }
        if(valid)
        {
            userValid.SetActive(true);
        }
        else
        {
            PlayerData.User newUser = new PlayerData.User();
            newUser.userName = userRegister.text;
            newUser.passWord = passRegister.text;
            PlayerData.Player.Digimon newDigimon = new PlayerData.Player.Digimon();
            PlayerData.Player createPlayer = new PlayerData.Player(newDigimon);
            PlayerData playermore = new PlayerData(LoadAllInfo.maxDictionary, newUser, createPlayer);
            WriteFileUser(playermore);
            LoadPlayers();
            RegisteredSuccessed.SetActive(true);
        }
    }

    /// <summary>
    /// Save Account in File text.
    /// </summary>
    /// <param name="player"></param>
    public void WriteFileUser(PlayerData player)
    {
        string path = Application.streamingAssetsPath + "/" + player.id + ".txt";
        json = JsonMapper.ToJson(player);
        File.WriteAllText(path,json.ToString());
    }

    /// <summary>
    /// Load all account in to dictionary (listuser)
    /// </summary>
    public void LoadPlayers()
    {
        for(int i=0;i<LoadAllInfo.maxDictionary;i++)
        {
            listUser.Remove(i);
        }
        int num = 0;
        bool readed = true;
        while (readed)
        {
            string path = Application.streamingAssetsPath + "/" + num + ".txt";
            //FileInfo file = new FileInfo(Application.dataPath + "/Resources/Json Files/" + num + ".txt");
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                readed = false;
            }
            else
            {
                Debug.Log("aaa" + num);
                string jsonString = File.ReadAllText(path);
                PlayerData newPlayer = JsonUtility.FromJson<PlayerData>(jsonString);
                //PlayerData newPlayer = JsonUtility.FromJson<PlayerData>(Resources.Load<TextAsset>("Json Files/" + num).text);
                listUser.Add(newPlayer.id, newPlayer);
                print(JsonUtility.ToJson(newPlayer));
                num++;
                
            }
        }
        LoadAllInfo.maxDictionary = num;
    }

    public void ButtonRegistedDone()
    {
        RegisteredSuccessed.SetActive(false);
    }

    public void createPlayerNull()
    {

    }

    /// <summary>
    /// show when registed fail with username have already
    /// </summary>
    public void ButtonOkValidUser()
    {
        userValid.SetActive(false);
    }

    /// <summary>
    /// Exit Register pannel
    /// </summary>
    public void ButtonCancelRegister()
    {
        Register.SetActive(false);
    }

    /// <summary>
    /// Register pannel open
    /// </summary>
    public void ButtonRegister()
    {
        Register.SetActive(true);
    }

    /// <summary>
    /// Set click in Login button to log in game.
    /// </summary>
    public void LoginClick()
    {
        userName = userText.text;
        passWord = passText.text;
        int num = 0;
        bool logged = false;
        while(num<LoadAllInfo.maxDictionary&&!logged)
        {
            if (userName == LoadAllInfo.playerDictionary[num].user.userName && passWord == LoadAllInfo.playerDictionary[num].user.passWord)
            {
                loginUI.SetActive(false);
                serverUI.SetActive(true);
                logged = true;
                idOfPlayer = num;
            }
            num++;
            print(num);
        }
        if (!logged)
        {
            errorFormUI.SetActive(true);
        }      
        
    }

    /// <summary>
    /// Function hide error form when logging false.
    /// </summary>
    public void ClickOkError()
    {
        errorFormUI.SetActive(false);
    }

    /// <summary>
    /// Animator Text introduce in UIIntro;
    /// </summary>
    public void FadeText()
    {
        StartCoroutine(CallFadeText());
        
    }

    /// <summary>
    /// Fade Text out IntroUI;
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(FadeOutDelay());
    }

    /// <summary>
    /// Fade text in IntroUI;
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(FadeInDelay());
    }

    IEnumerator CallFadeText()
    {
        yield return new WaitForSeconds(1f);
        FadeIn();
        yield return new WaitForSeconds(5f);
        FadeOut();
        yield return new WaitForSeconds(3.5f);
        introLogoUI.SetActive(false);
        backGround.SetActive(true);
        loginUI.SetActive(true);
    }

    /// <summary>
    /// Set function FadeOut with IEnumerator;
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeOutDelay()
    {
        CanvasGroup canvasGroup = introText.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime/2;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }

    /// <summary>
    /// Set function Fade in with IEnumrator;
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeInDelay()
    {
        CanvasGroup canvasGroup = introText.GetComponent<CanvasGroup>();
        while (canvasGroup.alpha <=1 )
        {
            canvasGroup.alpha += Time.deltaTime / 5;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }
}
