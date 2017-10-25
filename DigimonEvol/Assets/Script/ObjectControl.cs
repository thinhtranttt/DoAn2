using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControlObject
{
    public class ObjectControl : MonoBehaviour
    {
        public static ObjectControl Instance { get; set; }

        public static List<GameObject> loadPlayerList;
        public static List<GameObject> loadDigimonList;

        [HideInInspector]
        public List<GameObject> playerList;
        [HideInInspector]
        public List<GameObject> digiList;

        [SerializeField]
        public int digiIndex = 0; //List start from 0.
        public int playerIndex = 0; //List start from 0.
        public static int numPlayer;
        public static int numDigi;

        // Use this for initialization
        void Start()
        {
            CallAddFunction();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                DestroyImmediate(Instance.gameObject);
                Instance = this;
            }
        }

        /// <summary>
        /// Call function to active object player and digimon.
        /// </summary>
        public void CallAddFunction()
        {
            addPlayer();
            addDigi();            
        }
        /// <summary>
        /// Add player from resources to object with name "Player".
        /// </summary>
        public void addPlayer()
        {
            GameObject[] player = Resources.LoadAll<GameObject>("Player/SelectSceen");
            foreach (GameObject c in player)
            {
                GameObject _char = Instantiate(c) as GameObject;
                _char.transform.SetParent(GameObject.Find("Player").transform);
                playerList.Add(_char);
                _char.SetActive(false);
                //playerList[playerIndex].SetActive(true);
            }
            loadPlayerList = playerList;
        }

        /// <summary>
        /// Add Digimon from resources to object with name "Digimon".
        /// </summary>
        public void addDigi()
        {
            GameObject[] player = Resources.LoadAll<GameObject>("Digimon/SelectSceen");
            foreach (GameObject c in player)
            {
                GameObject _char = Instantiate(c) as GameObject;
                _char.transform.SetParent(GameObject.Find("Digimon").transform);
                digiList.Add(_char);
                _char.SetActive(false);
                //digiList[playerIndex].SetActive(true);
            }
        }

        /// <summary>
        /// Show player follow index
        /// </summary>
        /// <param name="num"></param>
        public void SetActivePlayer(int num)
        {
            playerList[num].SetActive(true);
        }

        /// <summary>
        /// Hide player follow index
        /// </summary>
        /// <param name="num"></param>
        public void SetNonActivePlayer(int num)
        {
            playerList[num].SetActive(false);
        }

        /// <summary>
        /// Show digimon follow index
        /// </summary>
        /// <param name="num"></param>
        public void setActiveDigimon(int num)
        {
            digiList[num].SetActive(true);
        }

        /// <summary>
        /// Hide digimon follow index
        /// </summary>
        /// <param name="num"></param>
        public void setNonActiveDigimon(int num)
        {
            digiList[num].SetActive(false);
        }

        /// <summary>
        /// Show object from player and digimon.
        /// </summary>
        public void SetActiveObject()
        {
            playerList[playerIndex].SetActive(true);
            digiList[playerIndex].SetActive(true);
        }

        /// <summary>
        /// show next object of player in listplayer.
        /// </summary>
        public void playerNext()
        {
            playerList[playerIndex].SetActive(false);
            if (playerIndex + 1 == playerList.Count)
            {
                playerIndex = 0;
            }
            else
            {
                playerIndex++;
            }
            playerList[playerIndex].SetActive(true);
            numPlayer = playerIndex;
        }

        /// <summary>
        /// Show next object of digimon in listdigimon.
        /// </summary>
        public void digiNext()
        {
            digiList[digiIndex].SetActive(false);
            if (digiIndex + 1 == digiList.Count)
            {
                digiIndex = 0;
            }
            else
            {
                digiIndex++;
            }
            digiList[digiIndex].SetActive(true);
            numDigi = digiIndex;
        }

        /// <summary>
        /// Save index of object when player was chose.
        /// </summary>
        public void playerOK()
        {
            if (playerIndex > playerList.Count)
            {
                numPlayer = playerIndex - 1;
            }
            else
            {
                numPlayer = playerIndex;
            }
        }

        /// <summary>
        /// Save index of object when digimon was chose.
        /// </summary>
        public void clickOKDigi()
        {
            if (digiIndex > digiList.Count)
            {
                numDigi = digiIndex - 1;
            }
            else
            {
                numDigi = digiIndex;
            }
            
        }
    }
}
