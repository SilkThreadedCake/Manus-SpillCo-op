using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrab : MonoBehaviour
{
    // Referance to the character camera
    [SerializeField]
    private Camera charactercamera;

    // Referance to the slot for holding picked item
    [SerializeField]
    private Transform slot;

    // Referance to the currently held item
    private PickableItem pickedItem;

    private void Update()
    {
        // Execute when button pressed
        if (Input.GetButtonDown("Fire1"))
        {
            // Check if player picked something up already
            if (pickedItem)
            {
                // If yes, dropp item
                DropItem(pickedItem);
            }
            else
            {
                // If no, then try to pick up the item in front of player
                var ray = charactercamera.ViewportPointToRay(Vector3.one * 0.5f);
                
                // Create ray from center of screen
                RaycastHit hit;

                // Shoot ray to find object to pick
                if (Physics.Raycast(ray, out hit, 2.0f))
                {
                    // Check if object is pickable
                    var pickable = hit.transform.GetComponent<PickableItem>();

                    // If object has PickableItem class
                    if (pickable)
                    {
                        // Pick up
                        PickItem(pickable);
                    }
                }
            }
        }
    }

    private void PickItem(PickableItem item)
    {
        // Assigne reference
        pickedItem = item;

        // Dissable rigidbody and reset velocities
        item.Rb.isKinematic = true;
        item.Rb.velocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;

        // Set slot as parent
        item.transform.SetParent(slot);

        // Reset position and rotation
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }

    private void DropItem(PickableItem item)
    {
        // Remove reference
        pickedItem = null;

        // Remove parent
        item.transform.SetParent(null);

        // Enable rigidbody
        item.Rb.isKinematic = false;

        // Add force to trow item a little bit
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }
}
