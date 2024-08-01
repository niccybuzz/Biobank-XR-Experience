using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPanelToHead : MonoBehaviour
{

    [SerializeField]
    float panelDepth = 0.5f;
    [SerializeField]
    Transform targetCamera;
    public float positionLerpSpeed = 5f;
    public float rotationLerpSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 headPosition = Camera.main.transform.position;

        transform.position = Vector3.Lerp(transform.position, headPosition + panelDepth * Camera.main.transform.forward, Time.deltaTime * positionLerpSpeed);

        // Calculate the direction from the UI panel to the camera
        Vector3 lookDirection = targetCamera.position - transform.position;

        // Rotate the UI panel to face the camera
        transform.rotation = Quaternion.LookRotation(-lookDirection);
    }
}

