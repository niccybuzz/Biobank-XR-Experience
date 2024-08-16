using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SliceBlock : MonoBehaviour
{
    public Transform spawnLocation;
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    public List<GameObject> slices;
    public InstructionsPanelManager instructionsManager;
    private void Start()
    {
        spawnPosition = spawnLocation.transform.position;
        spawnRotation = spawnLocation.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool validSample = false;

        if (other.CompareTag("SpottySample"))
        {
            Instantiate(slices[0], spawnPosition, spawnRotation);
            validSample = true;
        }
        if (other.CompareTag("SquareSample"))
        {
            Instantiate(slices[1], spawnPosition, spawnRotation);
            validSample = true;
        }
        if (other.CompareTag("TriangleSample"))
        {
            Instantiate(slices[2], spawnPosition, spawnRotation);
            validSample = true;
        }
        
        if (validSample && instructionsManager != null)
        {
            instructionsManager.NextPanel(1f);
        }
    }
}