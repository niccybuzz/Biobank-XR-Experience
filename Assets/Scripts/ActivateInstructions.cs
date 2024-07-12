using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInstructions : MonoBehaviour
{
    private GameObject[] instructionsPanels;
    private GameObject boxHoveringHand;
    private GameObject pipetteHoveringHand;
    public void ActivateInstructionsPanel()
    {

        instructionsPanels = GameObject.FindGameObjectsWithTag("Instructions");
        boxHoveringHand = GameObject.FindGameObjectWithTag("GloveBoxHand");

        foreach (var panel in instructionsPanels)
        {
            panel.GetComponent<Canvas>().enabled = true;
        }
        boxHoveringHand.GetComponent<SkinnedMeshRenderer>().enabled = true;

    }

    public void ActivateSecondInstructionsPanel()
    {

        instructionsPanels = GameObject.FindGameObjectsWithTag("PipetteInstructions");
        pipetteHoveringHand = GameObject.FindGameObjectWithTag("HoveringPipetteController");

        foreach (var panel in instructionsPanels)
        {
            panel.GetComponent<Canvas>().enabled = true;
        }
        pipetteHoveringHand.GetComponent<SkinnedMeshRenderer>().enabled = true;

    }
}
