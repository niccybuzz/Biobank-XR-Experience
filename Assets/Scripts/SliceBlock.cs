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

    private void Start()
    {
        spawnPosition = spawnLocation.transform.position;
        spawnRotation = spawnLocation.transform.rotation;
    }
    // Start is called before the first frame update



    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning($"Object {other.name} entered trigger zone with tag: {other.tag}");

        if (other.CompareTag("SampleBlock"))
        {
            Debug.LogWarning("Block Entered Trigger Zone");

            SampleBlockManager blockManager = other.GetComponent<SampleBlockManager>();
            if (blockManager == null)
            {
                Debug.LogError("SampleBlockManager component not found on the object.");
                return;
            }

            GameObject childSlice = blockManager.associatedSlice;
            if (childSlice == null)
            {
                Debug.LogError("AssociatedSlice is not assigned in SampleBlockManager.");
                return;
            }

            Debug.Log("Instantiating associated slice.");
            Instantiate(childSlice, spawnPosition, spawnRotation);
        }
        else
        {
            Debug.LogWarning($"Object {other.name} entered trigger zone, but it is not a SampleBlock. Tag: {other.tag}");
        }
    }
}