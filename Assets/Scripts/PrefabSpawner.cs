using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkstationSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToCreate;

    [SerializeField]
    private float prefabSpawnClearance = 0.1f;

    private GameObject spawnedWorkstation;
    public RoomValidator validator;
    public Camera mainCamera;
    private MRUKRoom room;

    private void Start()
    {
    }
    private void Update()
    {
        if (spawnedWorkstation != null)
        {
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                spawnedWorkstation.transform.Rotate(Vector3.up, 90f);
            }
        }

    }

    public void SpawnWallArt()
    {

    }

    public void SpawnWorkstation()
    {
        room = MRUK.Instance.GetCurrentRoom();
        List<MRUKAnchor> tables = validator.GetAllTables(room);
        MRUKAnchor largestTable = validator.GetLargestAnchorFromList(tables);
        SpawnObjectOnTable2(largestTable);
    }

/*    private void SpawnObjectOnTable(MRUKAnchor table)
    {
        Vector2 tableSurface = table.PlaneRect.Value.size;
        Vector3 tableCenter = table.transform.position;
        Vector3 spawnPosition = new Vector3(tableCenter.x, tableCenter.y + prefabSpawnClearance, tableCenter.z);
        Quaternion spawnRotation = Quaternion.LookRotation(Vector3.right);

        GameObject spawnedObject = Instantiate(prefabToCreate, spawnPosition, spawnRotation);

        // Calculate direction to player
        Vector3 directionToPlayer = (mainCamera.transform.position - spawnedObject.transform.position).normalized;

        // Calculate current forward direction of the prefab
        Vector3 prefabForward = spawnedObject.transform.forward;

        //Check if the prefab is facing away from the player (within a tolerance)
        if (Vector3.Dot(directionToPlayer, prefabForward) < 0f) // Dot product < 0 means they are facing away
        {
            // Rotate the prefab 180 degrees around the y-axis
            Debug.LogWarning("Prefab rotated to face player");
          spawnedObject.transform.Rotate(Vector3.up, 180f);
        }

        spawnedWorkstation = spawnedObject;
    }*/

    private void SpawnObjectOnTable2(MRUKAnchor table)
    {
        Vector3 tableCenter = table.transform.position;
        Vector3 spawnPosition = new Vector3(tableCenter.x, tableCenter.y + prefabSpawnClearance, tableCenter.z);

        // Get table's rotation
        Quaternion tableRotation = table.transform.rotation;
        Vector3 tableRotationEuler = tableRotation.eulerAngles;

        // Set the rotation to align with the table's rotation
        Quaternion spawnRotation = Quaternion.Euler(tableRotationEuler.x + 90, tableRotationEuler.y, tableRotationEuler.z);

        GameObject spawnedObject = Instantiate(prefabToCreate, spawnPosition, spawnRotation);

        spawnedWorkstation = spawnedObject;
    }

}
