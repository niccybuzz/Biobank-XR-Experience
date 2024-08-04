using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeManager : MonoBehaviour
{
    public RawImage laptopImage;
    public SnapInteractable platform;
    public Canvas background;
    // Start is called before the first frame update
    public void DisplayCellImage()
    {
        // Iterating through the object currently interacting with the microscope platform (restricted to sample slices)
        foreach (var interactingSlice in platform.SelectingInteractors)
        {
            //Getting the parent gameobject (i.e. the actual slice)
            Transform slice = interactingSlice.gameObject.transform.parent;

            // fetching the raw image component from the slice's child objects
            RawImage sampleBlockImage = slice.GetComponentInChildren<RawImage>();

            //setting the laptop screen image to the sample image and changing background colour to white for visibility
            background.gameObject.SetActive(true);
            laptopImage.texture = sampleBlockImage.texture;


        }

    }

    public void RemoveImage()
    {
        background.gameObject.SetActive(false);
    }
}
