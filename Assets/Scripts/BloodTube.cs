using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubeManager : TestTube
{
    private bool bloodSplit = false;
    public GameObject wholeBlood;
    public GameObject splitBlood;
    public InstructionsPanelManager removeLidInstructions;
    public InstructionsPanelManager drawPlasmaInstructions;
    
    public bool challengeModeEnabled = false;

    public bool BloodSplit { get => bloodSplit; set => bloodSplit = value; }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tip"))
        {
            if (pipette.IsPressed && !lidOn && !pipette.IsFull)
            {
                StartDrawPlasma();
                PlayPlasmaSound(drawUpSound);
            }
        }

    }

    private void StartDrawPlasma()
    {
        isAnimating = true;
        animationTimer = animationDuration;
        pipette.IsFull = true;
        drawUpSound.Play();
        if (!challengeModeEnabled)
        {
            drawPlasmaInstructions.NextPanel(1f);
        }

    }

    public void SplitBlood()
    {
        bloodSplit = true;
        wholeBlood.SetActive(false);
        splitBlood.SetActive(true);
    }

    public override void RemoveLid()
    {

        lidOn = false;
        if (bloodSplit && !challengeModeEnabled)
        {
            removeLidInstructions.NextPanel(1f);
        }
    }

}
