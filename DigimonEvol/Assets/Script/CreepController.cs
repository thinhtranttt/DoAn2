using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CreepController : MonoBehaviour {

    public static CreepController instance;
    public Transform Digimon;
    public List<GameObject> DestinationPoints;
    [Header("Set Distance")]
    public float speed;
    public float alertDistance;
    public float walkingDistance;
    public float attackingDistance;
    public float remainingDistance;
    public int minTime;
    public int maxTime;
    //public Slider hpPlayer;

	[Header("Health")]
    public GameObject HPMANABar;  
	private Image hpImg;
	public float maxHp = 120;
	public float curHp = 120;
	public float dame = 0;
	public float armor = 0;

    [Header("Paticle System")]
    public ParticleSystem circle;

    private Animator anim;
    private NavMeshAgent agent;
    private int selectDestination;
    private Vector3 direction;    
    

    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.tag == "getHit")
        {
            Debug.Log("come in triger");
            if (ControlObjectMain.attacking)
            {
				UpdateHPCreep ();
				if(curHp<=0)
                {
                    CallCreepDie();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        circle.Play();
        Debug.Log("Choosed");
        MonterFollow.instance.creep = this.transform;
        
    }

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        circle.Stop();
		hpImg =HPMANABar.transform.GetChild(0).GetComponent<Image>();
	}

	void UpdateHPCreep()
	{
		curHp -= Digimon.transform.GetComponent<PlayerInventory>().currentDamage;
		float fillAmount = curHp / maxHp;
		hpImg.fillAmount = fillAmount;
	}

	// Update is called once per frame
	void Update () {
        SetStatusCreep();
	}

    public void TurnOffParticle()
    {
        circle.Stop();
    }
    private void Awake()
    {
        if (instance == null) instance = this;
        
    }

    private void FixedUpdate()
    {
        
    }
    public void SetStatusCreep()
    {
        //HPMANABar.SetActive(false);
        float distance = Vector3.Distance(Digimon.position, transform.position);
        if(agent.enabled==true && agent.remainingDistance<remainingDistance)
        {
            agent.enabled = false;
            anim.SetBool("isMove", false);
            anim.SetBool("isIdle", true);
            StartCoroutine(RandomMovement());
            //Debug.Log("Arrive................");

        }
        if (distance<alertDistance&&distance>walkingDistance)
        {
            agent.enabled = false;
            //Debug.Log("isAlert");
            anim.SetBool("isIdle", false);
            anim.SetBool("isMove", false);
        }
        else if(distance <= walkingDistance&&distance>attackingDistance)
        {
            agent.enabled = true;
            //Debug.Log("Moving call");
            /*direction = Digimon.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.9f );
            transform.Translate(0, 0, speed);*/

            agent.SetDestination(Digimon.transform.position);

            anim.SetBool("isMove", true);
            anim.SetBool("isAttack", false);
            anim.SetBool("isIdle", false);

            
        }else if(distance<=attackingDistance)
        {
            
            if (direction.magnitude <= attackingDistance)
            {
                direction = Digimon.position - transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.9f);
                //StartCoroutine(repeatAttack());
                anim.SetBool("isAttack", true);
                anim.SetBool("isMove", false);
            }
        }
        else if(anim.GetBool("isIdle")==false&&agent.enabled==false)
        {
            anim.SetBool("isAttack", false);
            anim.SetBool("isIdle", true);
            anim.SetBool("isMove", false);
            StartCoroutine(RandomMovement());
        }
    }

    /// <summary>
    /// Function Animator Die
    /// </summary>
    public void CallCreepDie()
    {
        StartCoroutine(delayDie());
    }

    /// <summary>
    /// Delay Die and destroy.
    /// </summary>
    /// <returns></returns>
    IEnumerator delayDie()
    {
        anim.SetBool("isDie", true);
        yield return new WaitForSeconds(3f);
        DestroyObject(this.gameObject);
		DestroyObject (anim);
    }

    IEnumerator repeatAttack()
    {
        anim.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttack", false);
    }
    
    public IEnumerator RandomMovement()
    {
        int index = Random.Range(minTime, maxTime);
        //Debug.Log("RandomTime: " + index);
        yield return new WaitForSeconds (index);
        int index2 = Random.Range(1, 3);
        switch(index2)
        {
            case 1:
                //Debug.Log("KeepIdle....");
                StartCoroutine(RandomMovement());
                break;
            case 2:
                //Debug.Log("Move....");
                agent.enabled = true;
                int lastDestination = selectDestination;
                selectDestination = Random.Range(0, DestinationPoints.Count);

                while(selectDestination==lastDestination||DestinationPoints[selectDestination].GetComponent<DestinationPoint>().isUsed==true)
                {
                    selectDestination = Random.Range(0, DestinationPoints.Count);
                }

                DestinationPoints[lastDestination].GetComponent<DestinationPoint>().isUsed =false;
                agent.SetDestination(DestinationPoints[selectDestination].transform.position);
                DestinationPoints[selectDestination].GetComponent<DestinationPoint>().isUsed = true;
                anim.SetBool("isAttack", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isMove", true);
                break;
        }
    }
}
