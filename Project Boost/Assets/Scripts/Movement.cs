using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem baseThrustParticles;

    Rigidbody rocketRigidBody;
    AudioSource rocketBoostAudio;


    void Start(){
        rocketRigidBody = GetComponent<Rigidbody>();
        rocketBoostAudio = GetComponent<AudioSource>();
    }

    void Update(){
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            StartThrusting();

        }
        else{
            rocketBoostAudio.Stop();
            baseThrustParticles.Stop();
        }
    }
    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            RightThrusting();
        }
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            LeftThrusting();
        }
        else{
            leftThrustParticles.Stop();
            rightThrustParticles.Stop();
        }
    }



    void StartThrusting(){
        rocketRigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
            if (!rocketBoostAudio.isPlaying){
                rocketBoostAudio.PlayOneShot(mainEngine);
            }
            if (!baseThrustParticles.isPlaying){
                baseThrustParticles.Play();
            }
    }

    void RightThrusting(){
        ApplyRotation(rotationThrust);
            if (!rightThrustParticles.isPlaying){
                rightThrustParticles.Play();
            }
    }

    void LeftThrusting(){
        ApplyRotation(-rotationThrust);
            if (!leftThrustParticles.isPlaying){
                leftThrustParticles.Play();
            }
    }

    void ApplyRotation(float rotatoinThisFrame){
        rocketRigidBody.freezeRotation = true; //Freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * Time.deltaTime * rotatoinThisFrame);
        rocketRigidBody.freezeRotation = false; //Unfreezing rotation so physics rotation takes over.
    }
}
