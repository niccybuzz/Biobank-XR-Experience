using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubeManager : MonoBehaviour
{
    public InstructionsPanelManager2 instructionsManager;
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
