using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInstructions : MonoBehaviour
{
    private GameObject[] instructionsPanels;
    public void ActivateInstructionsPanel()
    {

        instructionsPanels = GameObject.FindGameObjectsWithTag("Instructions");

        Debug.LogWarning("There are " + instructionsPanels.Length + " applicable gameobjects");
        foreach (var panel in instructionsPanels)
        {
            panel.GetComponent<Canvas>().enabled = true;
        }

    }
}
