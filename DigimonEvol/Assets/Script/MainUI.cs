using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ControlObject;
using LitJson;
using System.IO;

namespace ControlUI
{
    public class MainUI : MonoBehaviour
    {
        public static MainUI instance;
        public Dictionary<int, PlayerData> listUser = new Dictionary<int, PlayerData>();
        public static Dictionary<int, Quest> listQuest = new Dictionary<int, Quest>();

        [Header("Player")]
        public Slider hpPlayer;
        public Slider manaPlayer;
        public Slider expPlayer;
        public Text hpTextPlayer;
        public Text manaTextPlayer;
        public Text expTextPlayer;
        public Image avatarPlayer;
        public Image invenAvatarPlayer;
        public Text levelTextPlayer;
        public Text nameTextPlayer;
        

        [SerializeField]
        public string nameTamer;
        public string nameDigimon;

        [Header("Digimon")]
        public Slider hpDig;
        public Slider manaDigi;
        public Slider expDigi;
        public Text hpTextDigi;
        public Text manaTextDigi;
        public Text expTextDigi;
        public Image avatarDigi;
        public Image invenAvatarDigi;
        public Text levelTextDigi;
        public Text nameTextDigi;

        [Header("Skill & Evo & Item")]
        public Image Evo1;
        public Image Evo2;
        public Image Evo3;
        public Image Evo4;
        public Button btnEvo1;
        public Button btnEvo2;
        public Button btnEvo3;
        public Button btnEvo4;

        [Header("Config")]
        public GameObject config;


        [Header("Inventory")]
        public GameObject inventory;
        public GameObject storage;
        public GameObject character;

        public static bool configClicked;
        LoadAllInfo systemManager;

        [Header("Account Info")]
        private int idAccount;
        private PlayerData mainPlayer;

        // Use this for initialization
        void Start()
        {
            ResetUI();
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                DestroyImmediate(instance.gameObject);
                instance = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
            GetInput();
        }
        
        void GetInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ConfigButton();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {

            }
            else if (Input.GetKeyDown(KeyCode.C))
            {

            }
        }

        /// <summary>
        /// Reset UI when game start.
        /// </summary>
        void ResetUI()
        {
            listUser = UIManager.listUser;
            config.SetActive(false);
            inventory.SetActive(false);
            storage.SetActive(false);
            SetHealthPlayer();
            SetEvoPannel();            
            GetInfoAccount();
            LoadQuestList(0);
        }

        /// <summary>
        /// Info Account go main game.
        /// </summary>
        public void GetInfoAccount()
        {
            idAccount = UIManager.idOfPlayer;
            mainPlayer = listUser[UIManager.idOfPlayer];
            Debug.Log("======"+mainPlayer.player.avatarID);
            nameTextPlayer.text = mainPlayer.player.name.ToString();
            levelTextPlayer.text = mainPlayer.player.level.ToString();
            expTextPlayer.text = SetExpPlayer((float)mainPlayer.player.experience);
            expPlayer.value =(float) mainPlayer.player.experience;
            SetAvatarPlayer(mainPlayer.player.id);
            

            nameTextDigi.text = mainPlayer.player.digimon.name;
            levelTextDigi.text = mainPlayer.player.digimon.level.ToString();
            expTextDigi.text = SetExpDigi((float)mainPlayer.player.digimon.experience);
            expDigi.value = (float)mainPlayer.player.digimon.experience;
            SetAvatarDigimon(mainPlayer.player.digimon.id);
        }

        public void SetAvatarPlayer(int num)
        {
            string path = "Digimon/Avatar/Player/" + num;
            Debug.Log("Set avatarPlayer");
            avatarPlayer.sprite = Resources.Load<Sprite>(path);
            invenAvatarPlayer.sprite = Resources.Load<Sprite>(path);
        }

        public void SetAvatarDigimon(int num)
        {
            if(num==0)
            {
                avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Agumon");
                invenAvatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Agumon");
            }
            else
            {
                avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Gabumon");
                invenAvatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Gabumon");
            }
        }

        public void SetAvartaEvo(int num)
        {
            if (num == 1)
            {
                if (ControlObject.ObjectControl.numDigi == 0)
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Greymon");
                }
                else if (ControlObject.ObjectControl.numDigi == 1)
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/gabuchamp");
                }
                else
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/patachamp");
                }
            }
            else if (num == 2)
            {
                if (ControlObject.ObjectControl.numDigi == 0)
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/MetalGreymon");
                }
                else if (ControlObject.ObjectControl.numDigi == 1)
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/gabusuper");
                }
                else
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/patasuper");
                }
            }
            else if (num == 3)
            {
                if (ControlObject.ObjectControl.numDigi == 0)
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/WarGreymon");
                }
                else if (ControlObject.ObjectControl.numDigi == 1)
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/gabumega");
                }
                else
                {
                    avatarDigi.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Mega");
                }
            }
        }

        /// <summary>
        /// Set picture for evo line pannel.
        /// </summary>
        public void SetEvoPannel()
        {
            if (listUser[UIManager.idOfPlayer].player.digimon.id == 0)
            {
                btnEvo1.interactable = false;
                Evo1.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Agumon");
                Evo2.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Greymon");
                Evo3.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/MetalGreymon");
                Evo4.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/WarGreymon");
            }
            else
            {
                Evo1.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/Gabumon");
                Evo2.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/gabuchamp");
                Evo3.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/gabusuper");
                Evo4.sprite = Resources.Load<Sprite>("Digimon/Avatar/Digi/gabumega");
            }
        }

        public void LoadQuestList(int id)
        {
            print("has go in quest");
            Quest newQuest = JsonUtility.FromJson<Quest>(Resources.Load<TextAsset>("Quest/" + id.ToString()).text);
            listQuest.Add(newQuest.id, newQuest);
            print(JsonUtility.ToJson(newQuest));
        }

        /// <summary>
        /// Interface in config clicked
        /// </summary>
        public void InterfaceButton()
        {

        }

        /// <summary>
        /// Graphic in config clicked
        /// </summary>
        public void GraphicButton()
        {

        }

        /// <summary>
        /// Sound in config clicked
        /// </summary>
        public void SoundButton()
        {

        }

        /// <summary>
        /// Short Cut in config clicked
        /// </summary>
        public void ShortcutButton()
        {

        }

        /// <summary>
        /// Log out in config clicked
        /// </summary>
        public void LogOutButton()
        {

        }

        /// <summary>
        /// Guild click 
        /// </summary>
        public void GuildButton()
        {

        }

        /// <summary>
        /// Shop click
        /// </summary>
        public void ShopButton()
        {

        }


        public void characterButton()
        {
            if (character.activeSelf == false)
            {
                character.SetActive(true);
            }
            else
            {
                character.SetActive(false);
            }
        }
        /// <summary>
        /// Bag click
        /// </summary>
        public void BagButton()
        {
            if (inventory.activeSelf == false)
            {
                inventory.SetActive(true);
            }
            else
            {
                inventory.SetActive(false);
            }
            
        }

        /// <summary>
        /// Quest click
        /// </summary>
        public void QuestButton()
        {

        }

        /// <summary>
        /// Quit Game
        /// </summary>
        public void ExitGame()
        {
            JsonData playerJson;
            playerJson = JsonMapper.ToJson(listUser[UIManager.idOfPlayer]);
            File.WriteAllText(Application.streamingAssetsPath + UIManager.idOfPlayer + ".txt", playerJson.ToString());
            Application.Quit();
        }

        /// <summary>
        /// Set button Wheel ("Config")
        /// </summary>
        public void ConfigButton()
        {
            configClicked = true;
            if (config.activeSelf)
            {
                config.SetActive(false);
            }
            else
            {
                config.SetActive(true);
            }
        }

        /// <summary>
        /// Control HP bar player.
        /// </summary>
        public void SetHealthPlayer()
        {
            hpPlayer.maxValue = 500;
            hpPlayer.value = 500;

            hpTextPlayer.text = hpPlayer.value + "/" + hpPlayer.maxValue;

        }

        /// <summary>
        /// Control Man bar player.
        /// </summary>
        public void SetManaPlayer()
        {


        }

        /// <summary>
        /// Control Exp bar player.
        /// </summary>
        public string SetExpPlayer(float x)
        {
            expTextPlayer.text = x.ToString() + " %";
            return expTextPlayer.text;
        }

        /// <summary>
        /// Control HP bar digimon.
        /// </summary>
        public void SetHealthDigi()
        {
            hpPlayer.maxValue = 500;

        }

        /// <summary>
        /// Control Man bar digimon.
        /// </summary>
        public void SetManaDigi()
        {


        }

        /// <summary>
        /// Control Exp bar digimon.
        /// </summary>
        public string SetExpDigi(float x)
        {
            expTextDigi.text = x.ToString() + " %";
            return expTextDigi.text;
        }


    }
}
