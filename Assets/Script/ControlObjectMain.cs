using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlObject;
using ControlUI;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ControlObjectMain : MonoBehaviour
{
    public Dictionary<int, PlayerData> listUser = new Dictionary<int, PlayerData>();

    [HideInInspector]
    public List<GameObject> playerListMain;
    [HideInInspector]
    public List<GameObject> digiListMain;
    [HideInInspector]
    public List<GameObject> digiChampListMain;
    [HideInInspector]
    public List<GameObject> digiSuperListMain;
    [HideInInspector]
    public List<GameObject> digiMegaListMain;

    NavMeshAgent playerAgent;
    public LayerMask movementMask;

    [Header("Player Control All")]
    public static int health;
    public static int mana;
    public static int exp;
    private int numEvo = 0;
    private int numDigi;
    private int numPlayer;

    [Header("Control Call Anim")]
    public static bool evo;
    public static bool attacking;

    [SerializeField]
    public static int digiIndex = 0; //List start from 0.
    public static int playerIndex = 0; //List start from 0.
    private bool moving = false;
    private Vector3 located;
    private MainUI controlUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="BeAttacked")
        {
            Debug.Log("Creep attacking");
            MainUI.instance.hpPlayer.value -= 5;
        }
    }
    // Use this for initialization
    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        CallFunction();
    }

    // Update is called once per frame
    void Update()
    {
        if(MonterFollow.instance.creep!=null)
        {
            checkRange();
        }
        else
        {
            //CreepController.instance.circle.Stop();
        }


        if (Input.GetMouseButtonDown(1)&&!evo)
        {
            GetInteraction();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            attacking = true;
        }
        else if (Input.GetMouseButtonDown(0) && !MainUI.configClicked)
        {
            located = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {

            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            numEvo++;
            evo = true;
        }
        if (attacking == true)
        {
            CallAttackDigi();
        }
        if (this.transform.position.x - located.x == 0 && !attacking)
        {
            CallIdlePlayer();
        }
    }

    /// <summary>
    /// Move player to the point click.
    /// </summary>
    /// <param name="point"></param>
    public void MoveToPoint(Vector3 point)
    {
        if (!evo)
        {
            located = point;
            playerAgent.SetDestination(point);
        }
    }

    public void checkRange()
    {
        float distance = Vector3.Distance(MonterFollow.instance.transform.position, MonterFollow.instance.creep.transform.position);
        //Debug.Log(distance);
        if ( distance >10)
        {
            Debug.Log("coming");
            CreepController.instance.circle.Stop();
            MonterFollow.instance.creep = null;
        }
    }

    /// <summary>
    /// Move player with click.
    /// </summary>
    void GetInteraction()
    {
        /*Vector3 direction = Input.mousePosition - this.transform.position;
        direction.y = 0;
        playerListMain[ObjectControl.numPlayer].transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.9f);
        */
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (MainUI.configClicked)
        {
            MainUI.configClicked = false;
        }
        else
        {
            if (Physics.Raycast(interactionRay, out interactionInfo, 110, movementMask))
            {
                if(interactionInfo.collider.tag=="Yushima")
                {
                    Debug.Log("Yushima");
                }
                else
                {
                    MoveToPoint(interactionInfo.point);
                    CallWalkPlayer();
                }                
            }
        }
    }

    /// <summary>
    /// Call animator idle of player;
    /// </summary>
    public void CallIdlePlayer()
    {
        playerListMain[numPlayer].GetComponent<Animator>().SetInteger("Condition", 0);
    }

    /// <summary>
    /// Call animator walk of player;
    /// </summary>
    public void CallWalkPlayer()
    {
        playerListMain[numPlayer].GetComponent<Animator>().SetInteger("Condition", 2);
    }

    /// <summary>
    /// Call animator idle of digimon;
    /// </summary>
    public void CallIdleDigi()
    {
        digiListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 0);
        digiChampListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 0);
        digiSuperListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 0);
        digiMegaListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 0);
    }

    /// <summary>
    /// Function attack Digimon;
    /// </summary>
    public void CallAttackDigi()
    {
        StartCoroutine(DelayAttack());
    }

    /// <summary>
    /// repeat attack ;
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayAttack()
    {
        digiListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 2);
        digiChampListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 2);
        digiSuperListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 2);
        digiMegaListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 2);
        yield return new WaitForSeconds(0.5f);
        digiListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 0);
        digiChampListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 0);
        digiSuperListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 0);
        digiMegaListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 0);
        attacking = false;
    }

    /// <summary>
    /// Call animator walk of digimon;
    /// </summary>
    public void CallWalkDigi()
    {
        digiListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 1);
        digiChampListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 1);
        digiSuperListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 1);
        digiMegaListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 1);
    }

    /// <summary>
    /// Call animator walk of digimon;
    /// </summary>
    public void CallEvoDigi()
    {
        if (numEvo == 1)
        {
            StartCoroutine(CallEvo1());
        }
        else if (numEvo == 2)
        {
            StartCoroutine(CallEvo2());
        }
        else
        {
            StartCoroutine(CallEvo3());
        }
        evo = false;
    }

    /// <summary>
    /// Call add object to list;
    /// </summary>
    public void CallFunction()
    {
        listUser = LoadAllInfo.playerDictionary;
        numPlayer= listUser[UIManager.idOfPlayer].player.id;
        numDigi = listUser[UIManager.idOfPlayer].player.digimon.id;
        //Debug.Log("Call Function");
        addPlayerMain();
        addDigiMain();
        addDigiChampMain();
        addDigiSuperMain();
        addDigiMegaMain();
        CallIdlePlayer();
        CallIdleDigi();
    }

    /// <summary>
    /// Add player from resouces/player/MainSceen to list.
    /// </summary>
    public void addPlayerMain()
    {

        GameObject[] players = Resources.LoadAll<GameObject>("Player/MainSceen");
        int count = 0;
        foreach (GameObject c in players)
        {
            count++;
            //Debug.Log(count);
            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(GameObject.Find("PlayerList").transform);
            playerListMain.Add(_char);
            _char.SetActive(false);
        }
        playerListMain[numPlayer].SetActive(true);
    }

    /// <summary>
    /// Add digimon from resouces/player/MainSceen to list.
    /// </summary>
    public void addDigiMain()
    {
        GameObject[] players = Resources.LoadAll<GameObject>("Digimon/MainSceen/Stand");
        foreach (GameObject c in players)
        {
            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(GameObject.Find("DigimonList").transform);
            digiListMain.Add(_char);
            _char.SetActive(false);
        }
        digiListMain[numDigi].SetActive(true);
    }

    /// <summary>
    /// Add Champ digimon from resouces/player/MainSceen to list.
    /// </summary>
    public void addDigiChampMain()
    {
        GameObject[] players = Resources.LoadAll<GameObject>("Digimon/MainSceen/Champion");
        foreach (GameObject c in players)
        {
            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(GameObject.Find("ChampList").transform);
            digiChampListMain.Add(_char);
            _char.SetActive(false);
        }
        //digiChampListMain[ObjectControl.numDigi].SetActive(true);
    }

    /// <summary>
    /// Add Super digimon from resouces/player/MainSceen to list.
    /// </summary>
    public void addDigiSuperMain()
    {
        GameObject[] players = Resources.LoadAll<GameObject>("Digimon/MainSceen/Super");
        foreach (GameObject c in players)
        {
            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(GameObject.Find("SuperList").transform);
            digiSuperListMain.Add(_char);
            _char.SetActive(false);
        }
        //digiListMain[ObjectControl.numDigi].SetActive(true);
    }

    /// <summary>
    /// Add Mega champ from resouces/player/MainSceen to list.
    /// </summary>
    public void addDigiMegaMain()
    {
        GameObject[] players = Resources.LoadAll<GameObject>("Digimon/MainSceen/Mega");
        foreach (GameObject c in players)
        {
            GameObject _char = Instantiate(c) as GameObject;
            _char.transform.SetParent(GameObject.Find("MegaList").transform);
            digiMegaListMain.Add(_char);
            _char.SetActive(false);
        }
        // digiListMain[ObjectControl.numDigi].SetActive(true);
    }

    public void StandardEvo()
    {
        digiListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 3);
    }

    public void ChampEvo()
    {
        digiChampListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 3);
    }

    public void SuperEvo()
    {
        digiSuperListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 3);
    }

    IEnumerator CallEvo1()
    {
        yield return new WaitForSeconds(0.5f);
        digiListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 3);
        //Invoke("StandardEvo", 1f);
        yield return new WaitForSeconds(4f);
        digiListMain[numDigi].SetActive(false);
        digiChampListMain[numDigi].SetActive(true);
        digiChampListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 5);
        controlUI.SetAvartaEvo(1);
        yield return new WaitForSeconds(1f);
        evo = false;
    }

    IEnumerator CallEvo2()
    {
        yield return new WaitForSeconds(0.5f);
        digiChampListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 3);
        //Invoke("ChampEvo", 1f);
        yield return new WaitForSeconds(4f);
        digiChampListMain[numDigi].SetActive(false);
        digiSuperListMain[numDigi].SetActive(true);
        digiSuperListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 5);
        controlUI.SetAvartaEvo(2);
        yield return new WaitForSeconds(1f);
        evo = false;
    }

    IEnumerator CallEvo3()
    {
        yield return new WaitForSeconds(0.5f);
        digiSuperListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 3);
        //Invoke("SuperEvo", 1f);
        yield return new WaitForSeconds(4f);
        digiSuperListMain[numDigi].SetActive(false);
        digiMegaListMain[numDigi].SetActive(true);
        digiMegaListMain[numDigi].GetComponent<Animator>().SetInteger("Condition", 5);
        controlUI.SetAvartaEvo(3);
        yield return new WaitForSeconds(1f);
        evo = false;
    }

}





