using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour {

    public ParticleSystem circle, circlecenter, smoke, particle;


	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            circle.Play();
            circlecenter.Play();
            smoke.Play();
            particle.Play();
        }
            

	}
}
