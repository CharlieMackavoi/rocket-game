using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rocketRigidBody;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;


    // Start is called before the first frame update
    void Start()
    {
        rocketRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space)){
            rocketRigidBody.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if(Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotatoinThisFrame){
        rocketRigidBody.freezeRotation = true; //freezing rotation so we can manually rotate.
        transform.Rotate(Vector3.forward * Time.deltaTime * rotatoinThisFrame);
        rocketRigidBody.freezeRotation = false; //unfreezing rotation so physics rotation takes over
    }
}
