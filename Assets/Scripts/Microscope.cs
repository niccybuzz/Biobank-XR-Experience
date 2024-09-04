using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Microscope : SnappablePlatform {
    public Image tabletImageLocation;
    public Canvas tabletScreen;
    private GameObject slideOnPlatform;

    public InstructionsPanelManager instructionsPanelManager;
    public InstructionsPanelManager prevStep;

    public void DisplayCellImage()
    {
        if (instructionsPanelManager != null)
        {

            instructionsPanelManager.NextPanelCheckPrevious(1f, prevStep);
        }
        slideOnPlatform = GetObjectOnPlatform();
        if (slideOnPlatform != null)
        {
            Sprite image = slideOnPlatform.GetComponent<MicroscopeSlide>().SlideImage;
            tabletImageLocation.sprite = image;
        }

        tabletScreen.gameObject.SetActive(true);
    }

    public void TurnOffTabletScreen()
    {
        tabletScreen.gameObject.SetActive(false);
        tabletImageLocation.sprite = null;
    }

    public void DeleteSlideOnPlatform()
    {
        slideOnPlatform.gameObject.SetActive(false);    
    }
}
