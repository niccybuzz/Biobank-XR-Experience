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
    public TextMeshProUGUI debugText;

    public void DisplayCellImage()
    {
        GameObject slideOnPlatform = GetSlideOnPlatform();
        if (slideOnPlatform != null)
        {
            Sprite image = slideOnPlatform.GetComponent<Slide>().SlideImage;
            tabletImageLocation.sprite = image;
        }
        else
        {
            debugText.text = "Can't find slide on platform\n";
        }
        //setting the laptop screen image to the sample image and changing tabletScreen colour to white for visibility
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

    private void UpdateDebugText(GameObject slide)
    {
    }

    public void RemoveBlockFromPlatform()
    {
        debugText.text = "Block removed from platform";
        tabletScreen.gameObject.SetActive(false);
        tabletImageLocation.sprite = null;
    }
}
