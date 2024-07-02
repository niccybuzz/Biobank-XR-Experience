using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using System;
using TMPro;
using UnityEngine.UI;

public class RoomValidator : MonoBehaviour
{
    private MRUKRoom currentRoom;
    private string[] propertiesToCheck = { "TABLE" };
    private bool tableIsLongEnough = false;
    private bool tableIsWideEnough = false;
    public Button continueButton;

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
        bool roomHasTable;

        if (currentRoom != null)
        {
            roomHasTable = currentRoom.DoesRoomHave(propertiesToCheck);
            if (roomHasTable)
            {
                List<MRUKAnchor> tables = GetAllTables(currentRoom);
                MRUKAnchor largestTable = GetLargestTable(tables);
                CheckTableSize(largestTable);
            }
            
            UpdateValidationGUIText(roomHasTable, tableIsLongEnough, tableIsWideEnough);

        }
    }

    //Checks the size of the tables in the room and modifies the text in the room setup pop-up window appropriately
    private void CheckTableSize(MRUKAnchor table)
    {
        Vector2 tableSize = table.PlaneRect.Value.size;

        if (tableSize.x > minimumTableLength)
        {
            tableIsLongEnough = true;
            Debug.LogWarning(tableSize.x);
        }
        else
        {
            tableIsLongEnough = false;
        }

        if (tableSize.y > minimumTableWidth)
        {
            tableIsWideEnough = true;
            Debug.LogWarning(tableSize.y);
        }
        else
        {
            tableIsWideEnough = false;
        }
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
        Debug.Log("There are " + tables.Count + " tables in your room.");
        return tables;
    }

    //Determines the area of each table in a list of anchors and returns the largest anchor
    public MRUKAnchor GetLargestTable(List<MRUKAnchor> tables)
    {
        MRUKAnchor largestTable = tables[0];
        float largestTableArea = 0;

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

    //Updated a specified text field depending on whether the room has a table, and is of a suitable size
    public void UpdateValidationGUIText(bool roomHasTable, bool tableLongEnough, bool tableWideEnough)
    {
        if (!roomHasTable)
        {
            roomValidationText.text = "No table detected in your environment.\n\n Please configure your space setup in Oculus settings to include a table at least " + minimumTableLength + " by " + minimumTableWidth + " metres.";
            continueButton.enabled = false;
        }
        else
        {
            if (tableLongEnough && tableWideEnough)
            {
                roomValidationText.text = "A suitable table has been detected in your room! Press the button below to begin the simulation.";
            }
            else
            {
                roomValidationText.text = "A table has been detected, but it is too small. Please configure your space setup in Oculus settings to include a table at least " + minimumTableLength + " by " + minimumTableWidth + " metres.";
                continueButton.enabled = false;
            }
        }
    }
}
