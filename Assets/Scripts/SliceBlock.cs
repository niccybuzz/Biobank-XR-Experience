using System.Collections.Generic;
using UnityEngine;

/*
 * Spawns a block when the crank is rotated with a block attached
 * Has a list of slices, each of which correspond to a different block colour pattern, for visual consistency
 */
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