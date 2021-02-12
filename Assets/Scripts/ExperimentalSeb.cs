using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperimentalSeb : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Tooltip("The speed walking causes")] [Range(0,10)] private float walkingSpeed = 1;

    [Tooltip("Input to walk Horizontal")] public string horizontal ="Horizontal";
    [Tooltip("Input to walk Vertical")] public string vertical = "Vertical";


    private float horInput;
    private float verInput;
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

        if (Input.GetAxis(horizontal) !=0 || (Input.GetAxis(vertical)!=0))
        {
            RigidB.velocity = new Vector3(horInput * walkingSpeed, 0, verInput * walkingSpeed);
        }
        
    }

    private void CameraControlls()
    {

    }

}
