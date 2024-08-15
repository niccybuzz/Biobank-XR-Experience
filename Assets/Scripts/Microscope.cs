using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeManager : MonoBehaviour
{
    public Image tabletImageLocation;
    public Canvas tabletScreen;
    public SnapInteractable platform;

    public void DisplayCellImage()
    {
        GameObject slideOnPlatform = GetSlideOnPlatform();
        if (slideOnPlatform != null)
        {
            Sprite image = slideOnPlatform.GetComponent<Slice>().SliceImage;
            tabletImageLocation.sprite = image;
        }

        tabletScreen.gameObject.SetActive(true);
    }

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

    public void RemoveBlockFromPlatform()
    {
        tabletScreen.gameObject.SetActive(false);
        tabletImageLocation.sprite = null;
    }
}
