using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRigidBody;
    AudioSource rocketBoostAudio;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;


    // Start is called before the first frame update
    void Start(){
        rocketRigidBody = GetComponent<Rigidbody>();
        rocketBoostAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        ProcessThrust();
        ProcessRotation();
        
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            rocketRigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if(!rocketBoostAudio.isPlaying){
                rocketBoostAudio.Play();
            }
            
        }
        else{
            rocketBoostAudio.Stop();
        }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotatoinThisFrame){
        rocketRigidBody.freezeRotation = true; //Freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * Time.deltaTime * rotatoinThisFrame);
        rocketRigidBody.freezeRotation = false; //Unfreezing rotation so physics rotation takes over.
    }
}
