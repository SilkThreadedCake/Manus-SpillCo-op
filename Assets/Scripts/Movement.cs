using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Tooltip("The speed of movement forward")] [Range(0, 10)] private float forwardWalkingSpeed = 1;
    [SerializeField] [Tooltip("The speed of movement backwards")] [Range(0, 10)] private float backwardWalkingSpeed = 1;
    [SerializeField] [Tooltip("The speed of movement to the sides")] [Range(0, 10)] private float sideWalkingSpeed = 1;

    [Tooltip("This is the horizontal input title")] public string horizontal = "Horizontal";
    [Tooltip("This is the vertical input title")] public string vertical = "Vertical";

    private float horInput;
    private float verInput;
    [Tooltip("How much forward/Backward will tilt to a side")] [Range(-1, 1)] public float xOffset;
    [Tooltip("How much left/side will tilt forward/backwars")] [Range(-1, 1)] public float yOffset;

    private Rigidbody RigidB;

    private void Start()
    {
        RigidB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Walking();
    }

    private void Walking()
    {
        horInput = Input.GetAxis(horizontal); //Grabs the value of Horizontal input
        verInput = Input.GetAxis(vertical); //Grabs the value of Vertical input

        float rightMove = verInput * backwardWalkingSpeed + yOffset * -1;
        float leftMove = verInput * forwardWalkingSpeed + yOffset;//Add time.deltatime
        float forwardMove = horInput * sideWalkingSpeed + xOffset;
        float backwardsMove = horInput * sideWalkingSpeed + xOffset * -1;

        float cameraRotation = Camera.main.transform.rotation.eulerAngles.y; //Creates camerarotation refrence for y


        if (Input.GetAxis(horizontal) > 0 || (Input.GetAxis(vertical) > 0)) //Is the movement forward or to the right
        {
            RigidB.velocity = Quaternion.Euler(0, cameraRotation, 0) * new Vector3(forwardMove, 0, leftMove); //Make movement with a potential offset to make it angled
        }
        else if (Input.GetAxis(horizontal) < 0 || (Input.GetAxis(vertical) < 0)) //Is the movement backwards or to the left
        {
            RigidB.velocity = Quaternion.Euler(0, cameraRotation, 0) * new Vector3(backwardsMove, 0, rightMove); //Make movement with a potnetial offset oposite to the previous one to make it angled
        }
        else { RigidB.velocity = new Vector3(0, 0, 0); } //If not, make sure the movement resets.
    }
}