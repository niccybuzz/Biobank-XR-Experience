using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using System;
using TMPro;

public class RoomValidator : MonoBehaviour
{
    private MRUKRoom currentRoom;
    private string[] propertiesToCheck = { "TABLE" };
    private bool roomHasTable = false;

    [SerializeField]
    private float minimumTableLength = 1f;

    [SerializeField]
    private float minimumTableWidth = 1f;

    [SerializeField]
    private TextMeshProUGUI roomValidationText;

    // Checks that the room has a valid table before starting the application
    public void RoomValidate()
    {
        currentRoom = MRUK.Instance.GetCurrentRoom();

        if (currentRoom != null)
        {
            roomHasTable = currentRoom.DoesRoomHave(propertiesToCheck);

            if (roomHasTable)
            {
                CheckTableSize(currentRoom);
            }
            else
            {
                roomValidationText.text = "No table detected in your environment.\n\n Please configure your space setup in Oculus settings to include a table at least " + minimumTableLength + " by " + minimumTableWidth + " metres.\n\n Alternatively, you may create a virtual table if there are no suitable tables available.";
                Debug.LogWarning("Room does not have a table");
            }
        }
    }

    //Checks the size of the tables in the room and modifies the text in the room setup pop-up window appropriately
    private void CheckTableSize(MRUKRoom room)
    {
        List<MRUKAnchor> tables = GetAllTables(room);

        // Check the size of each table in the room. If a table of minimum size requirements is found, allow the user to choose between using that table or a virtual one.
        foreach (MRUKAnchor table in tables)
        {
            Vector2 tableSize = table.PlaneRect.Value.size;
            if (tableSize.x > 1)
            {
                roomValidationText.text = "A suitable table has been detected in your room";
                return;
            }
            else
            {
                roomValidationText.text = "A table has been detected, but it is too small. Please configure your space setup in Oculus settings to include a table at least " + minimumTableLength + " by " + minimumTableWidth + " metres. Alternatively, you may create a virtual table if there are no suitable tables available.";
            }
        }
        Debug.Log("There are " + tables.Count + " tables in your room.");
    }

    public List<MRUKAnchor> GetAllTables(MRUKRoom room)
    {
        List<MRUKAnchor> tables = new();

        // First, collect all table anchors
        foreach (MRUKAnchor anchor in room.Anchors)
        {
            if (anchor.AnchorLabels.Contains("TABLE"))
            {
                tables.Add(anchor);
            }
        }

        return tables;
    }


}
