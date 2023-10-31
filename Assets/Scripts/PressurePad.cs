using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    // detect moving box
    // when close to center
    // disable the box's rigidbody or set it kinematic
    // change color to blue

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MoveableBox")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log("Distance: " + distance);
            if (distance < 0.05f)
            {
                Rigidbody box = other.GetComponent<Rigidbody>();
                if (box != null)
                {
                    box.isKinematic = true;
                }
                else
                {
                    Debug.LogError("Could not get Rigidbody in OnTriggerStay.");
                }

                MeshRenderer pressurePlate = GetComponentInChildren<MeshRenderer>();
                if (pressurePlate != null)
                {
                    pressurePlate.material.color = Color.blue;
                }
                else
                {
                    Debug.LogError("Could not get MeshRenderer in OnTriggerStay.");
                }
                Destroy(this);
            }
        }
    }
}
