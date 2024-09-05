using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Every frame, adjust the position of this GameObject to remain in the center of the user's view
public class CenterPanelToHead : MonoBehaviour
{

    [SerializeField]
    float panelDepth = 0.5f;

    [SerializeField]
    Transform targetCamera;

    [SerializeField]
    float positionLerpSpeed = 5f;
    
    [SerializeField]
    float rotationLerpSpeed = 5f;

    void Update()
    {
        // Get the location of the camera
        Vector3 headPosition = Camera.main.transform.position;

        // Interpolate from the objects current position to the centre of the viewport
        transform.position = Vector3.Lerp(transform.position, headPosition + panelDepth * Camera.main.transform.forward, Time.deltaTime * positionLerpSpeed);

        // Calculate the direction the camera is facing
        Vector3 lookDirection = targetCamera.position - transform.position;

        // Rotate the UI panel to face the camera
        transform.rotation = Quaternion.LookRotation(-lookDirection);
    }
}

