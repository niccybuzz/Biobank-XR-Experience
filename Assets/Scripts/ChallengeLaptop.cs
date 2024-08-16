using TMPro;
using UnityEngine;


/*
 * An extension of the laptop class. Has unique implementation for the laptop buttons, manipulating the scoreboard and deleting slides/blocks once correctly matched
 */
public class ChallengeLaptop : LaptopController
{
    public ChallengeMode challengeMode;
    public Microtome microtome;
    public Microscope microscopeManager;
    public ParticleSystem smoke1;
    public ParticleSystem smoke2;

    // Called whenever the "Select" button is pressed on the laptop
    public override void SelectButton()

    {
        string message;
        bool matches;

        // Have to check whether there is currenly an image on both the laptop screen and the tablet screen
        if (displayImage == null) return;
        if (tabletImage == null) return;

        if (displayImage.sprite == tabletImage.sprite)
        {
            message = "Correct!";
            matches = true;
            if (!correctChoiceSound.isPlaying)
            {
                correctChoiceSound.Play();
            }
            challengeMode.AddPoint();
            ClearBlocksAndSlices();
        }
        else
        {
            matches = false;
            message = "Wrong!";
            if (!wrongChoiceSound.isPlaying)
            {
                wrongChoiceSound.Play();
            }
        }

        // The popup message is the wee bit of text that says "Correct" or "Wrong" and pops up from behind the laptop
        popupMessage.ShowMessage(message, matches);
    }

    /* 
     * Deletes all of the current slices and blocks in the scene, called after correcly matching the images.
     * Useful because stop the area getting cluttered with loads of leftover slides from previous blocks
     * */
    private void ClearBlocksAndSlices()
    {
        GameObject[] slices = GameObject.FindGameObjectsWithTag("Slice");
        foreach (GameObject slice in slices) 
        {
            slice.SetActive(false);
        }
        microscopeManager.TurnOffTabletScreen();
        microscopeManager.DeleteSlideOnPlatform();
        microtome.ClearBlocksOnPlatform();

        
        //Playing the smoke particle systems whenever they are deleted for a little visual effect
        smoke1.Play();
        smoke2.Play();
    }
}
