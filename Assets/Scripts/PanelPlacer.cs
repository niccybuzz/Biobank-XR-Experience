using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPlacer : MonoBehaviour
{

    [SerializeField]
    float panelDepth = 0.5f;
    [SerializeField]
    Transform targetCamera;

    // Update is called once per frame
    void Start()
    {
        Vector3 headPosition = Camera.main.transform.position;

        transform.position = headPosition + panelDepth * Camera.main.transform.forward;

        // Calculate the direction from the UI panel to the camera
        Vector3 lookDirection = targetCamera.position - transform.position;

        // Rotate the UI panel to face the camera
        transform.rotation = Quaternion.LookRotation(-lookDirection);
    }
}
