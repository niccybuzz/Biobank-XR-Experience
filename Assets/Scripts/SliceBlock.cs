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
    public GameObject slice;
    private void Start()
    {
        spawnPosition = spawnLocation.transform.position;
        spawnRotation = spawnLocation.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("SampleBlock"))
        {
            Debug.Log("Instantiating associated slice.");
            Instantiate(slice, spawnPosition, spawnRotation);
        }
    }
}