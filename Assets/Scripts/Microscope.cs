using UnityEngine;
using UnityEngine.UI;

/**
 * Manages the microscope and the hovering tablet screen above
 */
public class Microscope : SnappablePlatform {
    public Image tabletImage;
    public Canvas tabletScreen;
    private GameObject _slideOnPlatform;

    public InstructionsPanelManager instructionsPanelManager;
    public InstructionsPanelManager prevStep;

    // Activates the tablet screen and displays the image from the sample
    public void DisplayCellImage()
    {
        if (instructionsPanelManager != null)
        {
            instructionsPanelManager.NextPanelCheckPrevious(1f, prevStep);
        }

        _slideOnPlatform = GetObjectOnPlatform();

        if (_slideOnPlatform != null)
        {
            Sprite image = _slideOnPlatform.GetComponent<MicroscopeSlide>().SlideImage;
            tabletImage.sprite = image;
        }
        tabletScreen.gameObject.SetActive(true);
    }

    public void TurnOffTabletScreen()
    {
        tabletScreen.gameObject.SetActive(false);
        tabletImage.sprite = null;
    }

    public void DeleteSlideOnPlatform()
    {
        _slideOnPlatform.gameObject.SetActive(false);    
    }
}
