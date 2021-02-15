using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalSeb : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Tooltip("The speed walking causes")] [Range(0,10)] private float walkingSpeed = 1;

    [Tooltip("Input to walk Horizontal")] public string horizontal ="Horizontal";
    [Tooltip("Input to walk Vertical")] public string vertical = "Vertical";

    //Possible use, make bool for all 4 directions, use hor/verInput +- to sever a specific directions movement.
    // Possible idea, direction are slighltly off, Forward has 4degrees.

    private float horInput;
    private float verInput;
    [Header ("How much forward/Backward will tilt to a side")][Range(-1,1)]public float xOffset;
    [Header("How much left/side will tilt forward/backwars")] [Range(-1,1)]public float yOffset;

    [Header("Camera")]
    [Range(0,5)]public float horCameraSpeed;
    [Range(0,5)]public float verCameraSpeed;

    private float yaw = 0f;
    private float pitch = 0f;

    private Rigidbody RigidB;

    private void Start()
    {
        RigidB = GetComponent<Rigidbody>();
        CameraControlls();
    }

    private void Update()
    {
        Walking();
        CameraControlls();//No use yet
    }


    private void Walking()
    {
        horInput = Input.GetAxis(horizontal); //Grabs the value of Horizontal
        verInput = Input.GetAxis(vertical); //Grabs the value of Vertical

        if (Input.GetAxis(horizontal) >0 || (Input.GetAxis(vertical)>0))
        {
            RigidB.velocity = new Vector3(horInput * walkingSpeed + xOffset, 0, verInput * walkingSpeed + yOffset);
        }
        else if (Input.GetAxis(horizontal) < 0 || (Input.GetAxis(vertical) < 0))
        {
            RigidB.velocity = new Vector3(horInput * walkingSpeed + xOffset*-1, 0, verInput * walkingSpeed + yOffset*-1);
        }
        else { RigidB.velocity = new Vector3(horInput * walkingSpeed, 0, verInput * walkingSpeed); }
        
    }

    private void CameraControlls() //Currently moves the camera.. Kinda, by rotating the player completely, does not rotate movement direction.REMAKE
    {
        yaw += horCameraSpeed * Input.GetAxis("Mouse X");
        pitch += verCameraSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

}
