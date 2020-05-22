using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fart : MonoBehaviour
{
    Rigidbody rigidBody;
    float RotationSpeed = 160.0f;
    AudioSource audioSource;
    bool playFart;
    bool toggleFartSound;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        playFart = true;
        toggleFartSound = true;
    }

    // Update is called once per frame
    void Update()
    {
       
        ProcessInput();
        PlayGumboFart();

        
    }
    //controls for user and define how thrust will work
    private void ProcessInput()
    {
       
        //thrust
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rigidBody.AddRelativeForce(Vector3.up);
           
                 
        }
        //rotation
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward*RotationSpeed*Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.back * RotationSpeed * Time.deltaTime);
        }
    }
    //audio for gumbo and his fart rocket
    private void PlayGumboFart()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            
            //start fart sound for gumbo
            if (playFart == true && toggleFartSound == true)
            {
                audioSource.Play();
                toggleFartSound = false;
            }

        }
        //stop fart sound for gumbo
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            audioSource.Stop();
            toggleFartSound = true;
        }
    }
}
