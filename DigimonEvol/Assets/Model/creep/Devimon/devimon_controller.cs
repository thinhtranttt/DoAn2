using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class devimon_controller : MonoBehaviour {
   
    public ParticleSystem circle, circlecenter, smoke, particle;

    [Header("combat")]
    private bool attacking;


	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
        ResetBoss();
       
		
	}

    void GetInput() {
        // attack
       
    }

    public void ResetBoss()
    {
        circle.Stop();
        circlecenter.Stop();
        smoke.Stop();
        particle.Stop();
    }
    
}
