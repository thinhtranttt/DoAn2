using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControlUI;

public class MonterFollow : MonoBehaviour {
    public static MonterFollow instance;
    public Transform player;
    public Transform creep;

	private CreepController tmpCreep;
    ControlObjectMain control;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="BeAttacked")
        {
            Debug.Log("Enemy attacking");
            //MainUI.instance.hpDig.value -= 5;
        }
    }
    // Use this for initialization
    void Start () {
        control = GameObject.FindObjectOfType(typeof(ControlObjectMain)) as ControlObjectMain;
    }

    // Update is called once per frame
    void Update () {
        FollowPlayer();
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void FollowMonter(Transform p,Transform c)
    {
       
    }

    /// <summary>
    /// Still Fixxing............
    /// </summary>
    public void FollowPlayer()
    {
        if(creep!=null&&ControlObjectMain.attacking)
        {
			tmpCreep = creep.transform.GetComponent<CreepController> ();
            Vector3 direction = this.transform.position - creep.position;            
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(creep.transform.rotation, Quaternion.LookRotation(direction), 0.9f);           
            if(direction.magnitude<2)
            {
                control.CallAttackDigi();
            }
            else
            {
                this.transform.Translate(0, 0, 0.15f);
                control.CallWalkDigi();
            }            
        }
        else if (Vector3.Distance(player.position, this.transform.position) > 3)
        {
            Vector3 direction = player.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.9f);
            if (ControlObjectMain.evo)
            {
                control.CallEvoDigi();
            }
            else if (ControlObjectMain.attacking)
            {
                control.CallAttackDigi();
            }else
            {
                this.transform.Translate(0, 0, 0.15f);
                control.CallWalkDigi();
            }
        }
        else
        {
            //Debug.Log(ControlObjectMain.evo);
            if (ControlObjectMain.evo)
            {
                control.CallEvoDigi();
            }
            else if (ControlObjectMain.attacking)
            {

                control.CallAttackDigi();
            }
            else
            {
                control.CallIdleDigi();
            }

        }
    }
}
