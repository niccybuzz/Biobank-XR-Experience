using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;
using System;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// This class is utilised before the start of an activity scene, and checks to see if the appropriate room anchors are in place
/// The result of the validation is outputted on the "Room Validation" face panel, and the user is prevented from continuing if the correct anchors have not been set up.
/// </summary>
public class RoomValidator : MonoBehaviour
{
    private MRUKRoom currentRoom;
    public Button continueButton;
    public Button mainMenuButton;
    public Button roomSetupButton;

    [SerializeField]
    private float minimumTableLength = 1f;

    [SerializeField]
    private float minimumTableWidth = 1f;

    [SerializeField]
    private TextMeshProUGUI roomValidationText;

    // Checks that the room has a valid table before starting the blood separator activity
    public void ValidateBloodSeparator()
    {
        currentRoom = MRUK.Instance.GetCurrentRoom();
        bool roomValid = false;
        string message;

        if (currentRoom != null)
        {
            bool roomHasTable = currentRoom.HasAllLabels(MRUKAnchor.SceneLabels.TABLE);
            if (!roomHasTable)
            {
                message = "No table detected in your environment.\n\n Please configure your space setup in Oculus settings to include a table at least " + minimumTableLength + " by " + minimumTableWidth + " metres.";
                roomValid = false;
            }
            else
            {
                List<MRUKAnchor> tables = GetAllAnchorsWithLabel(currentRoom, MRUKAnchor.SceneLabels.TABLE);
                MRUKAnchor largestTable = GetLargestAnchorFromList(tables);
                bool tableLargeEnough = CheckAnchorSize(largestTable, minimumTableLength, minimumTableWidth);
                if (tableLargeEnough)
                {
                    message = "A suitable table has been detected in your room! Press the button below to begin the simulation.";
                roomValid = true;
                }
                else
                {
                    message = "A table has been detected, but it is too small. Please configure your space setup in Oculus settings to include a table at least " + minimumTableLength + " by " + minimumTableWidth + " metres.";
                    roomValid = false;
                }
            }

            UpdateValidationGUIText(message, roomValid);
        }
    }

    //Basically the same as RoomValidateBloodSeparator, but also checks to see if there is a storage unit set up in scene model
    public void ValidateTissueAnalysis()
    {
        currentRoom = MRUK.Instance.GetCurrentRoom();
        bool roomValid = false;
        string message;

        if (currentRoom != null)
        {
            bool roomHasTable = currentRoom.HasAllLabels(MRUKAnchor.SceneLabels.TABLE);
            bool roomHasCabinet = currentRoom.HasAllLabels(MRUKAnchor.SceneLabels.STORAGE);
            if (!roomHasTable || !roomHasCabinet)
            {
                message = "No table or storage unit detected in your environment.\n\n Please follow the setup instructions on the main menu to begin the experiment";
                roomValid = false;
            }
            else
            {
                List<MRUKAnchor> tables = GetAllAnchorsWithLabel(currentRoom, MRUKAnchor.SceneLabels.TABLE);
                MRUKAnchor largestTable = GetLargestAnchorFromList(tables);
                bool tableLargeEnough = CheckAnchorSize(largestTable, minimumTableLength, minimumTableWidth);
                if (tableLargeEnough)
                {
                    message = "A suitable table and storage unit have been detected in your room! Press the button below to begin the simulation.";
                roomValid = true;
                }
                else
                {
                    message = "A table has been detected, but it is too small. Please ensure your table is at least " + minimumTableLength + " by " + minimumTableWidth + " metres.";
                    roomValid = false;
                }
            }

            UpdateValidationGUIText(message, roomValid);
        }
    }
    //Checks the size of the tables in the room and modifies the text in the room setup pop-up window appropriately
    private bool CheckAnchorSize(MRUKAnchor anchor, float minimumLength, float minimumHeight)
    {
        Vector2 anchorSize = anchor.PlaneRect.Value.size;

        return (anchorSize.x > minimumLength && anchorSize.y > minimumHeight);
    }


    public List<MRUKAnchor> GetAllTables(MRUKRoom room)
    {
        List<MRUKAnchor> tables = new();

        // First, collect all table anchors
        foreach (MRUKAnchor anchor in room.Anchors)
        {
            if (anchor.Label == MRUKAnchor.SceneLabels.TABLE)
            {
                tables.Add(anchor);
            }
        }
        return tables;
    }

    //Checks the current room to see if the scene model contains any anchors with a specified semantic label such as "table" or "wall_art"
    public List<MRUKAnchor> GetAllAnchorsWithLabel(MRUKRoom room, MRUKAnchor.SceneLabels label)
    {

        List<MRUKAnchor> anchors = new();

        // First, collect all table anchors
        foreach (MRUKAnchor anchor in room.Anchors)
        {
            if (anchor.Label == label)
            {
                anchors.Add(anchor);
            }
        }
        return anchors;
    }

    //Determines the area of each anchor in a list of anchors and returns the largest 
    public MRUKAnchor GetLargestAnchorFromList(List<MRUKAnchor> anchors)
    {
        MRUKAnchor largestAnchor = anchors[0];
        float largestAnchorArea = 0;

        foreach (MRUKAnchor anchor in anchors)
        {
            Vector2 anchorSize = anchor.PlaneRect.Value.size;
            float anchorArea = anchorSize.x * anchorSize.y;

            if (anchorArea > largestAnchorArea)
            {
                largestAnchor = anchor;
            }
        }
        return largestAnchor;
    }

    //Updated a specified text field depending on whether the room has a table, and is of a suitable size
    public void UpdateValidationGUIText(string message, bool roomValid)
    {
        if (!roomValid)
        {
            continueButton.enabled = false;
            mainMenuButton.enabled = true;
            roomSetupButton.enabled = true;
        }
        roomValidationText.text = message;
    }
}
