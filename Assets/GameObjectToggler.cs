using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction.Input;
using Unity.VisualScripting;
using UnityEngine;

public class GloveEquipper : MonoBehaviour
{
    private GameObject[] defaultHandVisuals;
    [SerializeField]
    Material gloveMaterial;

    //Finds the detault hand skins and replaces them with a blue latex glove material
    public void SwitchHandVisuals()
    {
        defaultHandVisuals = GameObject.FindGameObjectsWithTag("HandVisual");

        if (defaultHandVisuals != null )
        {
            //Turn off the default skins
            foreach (var item in defaultHandVisuals)
            {
                item.GetComponent<SkinnedMeshRenderer>().material = gloveMaterial;
            }
        } 
        else
        {
            Debug.Log("Hand Visuals not found");
        }
    }
}

