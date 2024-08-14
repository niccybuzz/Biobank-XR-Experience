using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeLaptop : LaptopController
{
    public ChallengeMode challengeMode;
    public Microtome microtome;
    public ParticleSystem smoke1;
    public ParticleSystem smoke2;

    public override void SelectButton()

    {
        string message;
        bool matches;
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
            ClearBlocksAndSlides();

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
        popupMessage.ShowMessage(message, matches);
    }

    private void ClearBlocksAndSlides()
    {
        GameObject[] slides = GameObject.FindGameObjectsWithTag("Slice");
        foreach (GameObject slide in slides)
        {
            slide.SetActive(false);
        }
        microtome.ClearBlocksOnPlatform();
        microtome.DetachBlock();
        smoke1.Play();
        smoke2.Play();
    }
}
