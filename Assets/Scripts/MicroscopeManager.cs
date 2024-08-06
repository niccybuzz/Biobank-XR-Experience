using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeManager : MonoBehaviour
{
    public Image tabletImageLocation;
    public Canvas tabletScreen;
    public SnapInteractable platform;
    public GameObject GetSlideOnPlatform()
    {

        GameObject blockOnPlatform = null;

        foreach (var selectingInterator in platform.SelectingInteractors)
        {
            //Getting the parent gameobject (i.e. the actual slice)
            Transform slice = selectingInterator.gameObject.transform.parent;
            blockOnPlatform = slice.gameObject;
        }

        return blockOnPlatform;
    }
    public void DisplayCellImage()
    {
        GameObject slideOnPlatformn = GetSlideOnPlatform();
        Sprite slideImage = slideOnPlatformn.GetComponentInChildren<Image>().sprite;

        //setting the laptop screen image to the sample image and changing tabletScreen colour to white for visibility
        tabletScreen.gameObject.SetActive(true);
        tabletImageLocation.sprite = slideImage;
    }
    

    public void RemoveBlockFromPlatform()
    {
        tabletScreen.gameObject.SetActive(false);
        tabletImageLocation.sprite = null;
    }
}
