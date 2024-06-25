using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWorkstation : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabToCreate;

    [SerializeField]
    private float prefabSpawnClearance = 0.1f;

    public RoomValidator Validator;
    private MRUKRoom room;


    public void OnSceneStart()
    {
        room = MRUK.Instance.GetCurrentRoom();
        List<MRUKAnchor> tables = Validator.GetAllTables(room);
        MRUKAnchor largestTable = GetLargestTable(tables);
        SpawnObjectOnTable(largestTable);
    }

    private MRUKAnchor GetLargestTable(List<MRUKAnchor> tables)
    {
        MRUKAnchor largestTable = tables[0];
        Vector2 largestTableSize = largestTable.PlaneRect.Value.size;
        float largestTableArea = largestTableSize.x * largestTableSize.y;

        foreach (MRUKAnchor table in tables)
        {
            Vector2 tableSize = table.PlaneRect.Value.size;
            float tableArea = tableSize.x * tableSize.y;
            if (tableArea > largestTableArea)
            {
                largestTable = table;
            }
        }
        return largestTable;
    }

    private void SpawnObjectOnTable(MRUKAnchor table)
    {
        Vector3 tableCenter = table.GetAnchorCenter();
        Vector3 spawnPosition = new Vector3(tableCenter.x, tableCenter.y + prefabSpawnClearance, tableCenter.z);

        GameObject spawnedObject = Instantiate(prefabToCreate, spawnPosition, Quaternion.identity);

        // Calculate direction to player
        Vector3 directionToPlayer = (Camera.main.transform.position - spawnedObject.transform.position).normalized;

        // Calculate current forward direction of the prefab
        Vector3 prefabForward = spawnedObject.transform.forward;

        //Check if the prefab is facing away from the player (within a tolerance)
        if (Vector3.Dot(directionToPlayer, prefabForward) < 0f) // Dot product < 0 means they are facing away
        {
            // Rotate the prefab 180 degrees around the y-axis
            spawnedObject.transform.Rotate(Vector3.up, 180f);
        }
    }

}
