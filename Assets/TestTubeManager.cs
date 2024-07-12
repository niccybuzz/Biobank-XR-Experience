using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubeManager : MonoBehaviour
{
    public InstructionsPanelManager instructionsManager;
    public bool lidOn = true;
    public bool bloodSplit = false;
    public GameObject wholeBlood;
    public GameObject splitBlood;

    // Start is called before the first frame update
    public void SplitBlood()
    {
        bloodSplit = true;
        wholeBlood.SetActive(false);
        splitBlood.SetActive(true);
    }
    
    public void PickUpBloodTube(bool isHeld)
    {
        if (bloodSplit)
        {
            instructionsManager.PickUpSplitBlood(isHeld);
        }
    }

    public void RemoveLid()
    {

        lidOn = false;
    }

    public void PutLidOn()
    {

        lidOn = true;
    }

}
