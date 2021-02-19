using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera settings")]
    [Tooltip("The speed of the camera movement")][Range(0,200)]public float mouseSensitivity = 100f; //Controlling the camera speed

    [Tooltip("How high the camera can go upwards")][Range(-90, 0)] public float upViewLimit = -90; //Limiting camera movement range
    [Tooltip("How low the camera can go downwards")] [Range(0, 90)] public float downViewLimit = 90;

    [Tooltip("The parent body of the camera")]public Transform playerBody; //A refrence of the body of the player(If you want the whole body to move, not just the camera)

    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//Locks the cursor in the screen
    }

    private void Update()
    {
        if (!Movement.reading) { cameraRotation(); } //Uses the same as movement does to prevent movement during reading phase.
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
    }


    private void cameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;//Grabs the MouseX input and adds speed*time
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;//Grabs the MouseY input and adds speed*time

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, upViewLimit, downViewLimit);//Limits the range of looking up & down

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetKey(KeyCode.Escape)) { Cursor.lockState = CursorLockMode.None; }//Release of the cursor
    }

    private void QuitGame() //Added simply as a end game.
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            UnityEditor.EditorApplication.isPlaying = false;
        }
#else
        Application.Quit();
#endif
    }
}
