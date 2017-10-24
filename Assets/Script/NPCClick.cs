using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCClick : MonoBehaviour {

    public void OnMouseDown()
    {
        if(this.transform.tag=="Yushima")
        {
            Debug.Log("Yushima");
        }
    }
}
