using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is used to swap from the Face Panels, to the instructions panels, at the start of activities
 * It is required because of the awkward process of communicating between objects in Prefab View
 * and objects in Scene View
 */
public class ActivateInstructions : MonoBehaviour
{
    private GameObject[] instructionsPanels;
    //the hand that hovers over the glove box needs to be enabled too
    private GameObject boxHoveringHand;

    public void ActivateInstructionsPanel()
    {
        //Find the relevant objects in the scene
        instructionsPanels = GameObject.FindGameObjectsWithTag("Instructions");
        boxHoveringHand = GameObject.FindGameObjectWithTag("GloveBoxHand");

        /*
         * Enable the canvas
         * It's done this way because the GameObject itself has to be active to be able to GameObject.Find them
         * This way they can be kept invisible until required
         */
        foreach (var panel in instructionsPanels)
        {
            panel.GetComponent<Canvas>().enabled = true;
        }

        if (boxHoveringHand)
        {
            boxHoveringHand.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }
}
