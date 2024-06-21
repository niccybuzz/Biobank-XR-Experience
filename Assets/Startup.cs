using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using System;

public class Startup : MonoBehaviour
{
    private MRUKRoom room;
    private string[] propertiesToCheck = { "TABLE" };
    private bool doesRoomHaveTable = false;
    public GameObject prefabToCreate;
    public float prefabSpawnClearance = 0.1f;

    // Checks that the room has a valid table before starting the application
    public void RoomValidate()
    {
        room = MRUK.Instance.GetCurrentRoom();
        doesRoomHaveTable = room.DoesRoomHave(propertiesToCheck);

        if (doesRoomHaveTable)
        {
            Debug.LogWarning("Room has a table");
            CheckTableSize();
        }
        else
        {
            Debug.LogWarning("Room does not have a table");
        }

    }

    private void CheckTableSize()
    {
        List<MRUKAnchor> tables = new List<MRUKAnchor>();

        // First, collect all table anchors
        foreach (MRUKAnchor anchor in room.Anchors)
        {
            Debug.LogWarning(anchor.GetLabelsAsEnum().ToString());

            if (anchor.AnchorLabels.Contains("TABLE"))
            {
                tables.Add(anchor);
            }
        }

        // Then, process each table
        foreach (MRUKAnchor table in tables)
        {
            Vector2 tableSize = table.PlaneRect.Value.size;
            if (tableSize.x < 1)
            {
                Debug.LogWarning("Your table is " + tableSize.x + " metres long, and is too small");
            }
            else
            {
                Debug.LogWarning("Your table is " + tableSize.x + " metres long, and is large enough");
                SpawnObjectOnTable(table);
            }
        }

        Debug.LogWarning("There are " + tables.Count + " tables in your room.");
    }

    private void SpawnObjectOnTable(MRUKAnchor table)
    {
        Vector3 tableCenter = table.GetAnchorCenter();
        Vector3 spawnPosition = new Vector3(tableCenter.x, tableCenter.y + prefabSpawnClearance, tableCenter.z);
        spawnPosition.y += prefabSpawnClearance;
        Instantiate(prefabToCreate, spawnPosition, Quaternion.identity);
    }

}
