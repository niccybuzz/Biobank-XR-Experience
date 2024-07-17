using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubeManager : MonoBehaviour
{
    public InstructionsPanelManager2 instructionsManager;
    private bool lidOn = true;
    private bool bloodSplit = false;
    public GameObject wholeBlood;
    public GameObject splitBlood;

    public bool BloodSplit { get => bloodSplit; set => bloodSplit = value; }
    public bool LidOn { get => lidOn; set => lidOn = value; }

    
    public void SplitBlood()
    {
        bloodSplit = true;
        wholeBlood.SetActive(false);
        splitBlood.SetActive(true);
    }

    public void RemoveLid()
    {

        lidOn = false;
        if (bloodSplit)
        {
            instructionsManager.NextPanel(1f);
        }
    }

    public void PutLidOn()
    {

        lidOn = true;
    }

}
