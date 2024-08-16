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
    public bool LidOn { get => lidOn; set => lidOn = value; }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tip"))
        {
            if (pipette.IsPressed && !lidOn && !pipette.IsFull)
            {
                StartDrawPlasma();
            }
        }

    }

    private void StartDrawPlasma()
    {
        isAnimating = true;
        animationTimer = animationDuration;
        pipette.IsFull = true;
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

    public void RemoveLid()
    {

        lidOn = false;
        if (bloodSplit && !challengeModeEnabled)
        {
            removeLidInstructions.NextPanel(1f);
        }
    }

    public void PutLidOn()
    {

        lidOn = true;
    }

}
