using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInstructions : MonoBehaviour
{
    private GameObject[] instructionsPanels;
    private GameObject hoveringHand;
    public void ActivateInstructionsPanel()
    {

        instructionsPanels = GameObject.FindGameObjectsWithTag("Instructions");
        hoveringHand = GameObject.FindGameObjectWithTag("GloveBoxHand");

        foreach (var panel in instructionsPanels)
        {
            panel.GetComponent<Canvas>().enabled = true;
        }
        hoveringHand.GetComponent<SkinnedMeshRenderer>().enabled = true;

    }
}
