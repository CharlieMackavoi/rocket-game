using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour{
    
    Color triggeredObject = Color.white;
    public GameObject triggerScript;

    void OnCollisionEnter(Collision other){
            if(other.gameObject.tag == "Player"){
                Renderer render = GetComponent<Renderer>();
                triggeredObject = render.material.color;
                render.material.color = Color.black;

                triggerScript.GetComponent<Oscillator>().enabled = true;
            }

    }
            

}
