using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CentrifugeSocketController : MonoBehaviour
{
    public CentrifugeController controller;
    public InstructionsPanelManager instructionsManager;

    public void IncrementNumberOfTubes()
    {
        controller.numberOfTubesInSockets++;
        if (controller.numberOfTubesInSockets == 2)
        {
            instructionsManager.twoTubesInCentrifuge = true;
            instructionsManager.CheckProgress();
        }
    }

    public void DecrementNumberOfTubes()
    {
        controller.numberOfTubesInSockets--;
        if (controller.numberOfTubesInSockets < 2)
        {
            instructionsManager.twoTubesInCentrifuge = false;
            instructionsManager.CheckProgress();
        }
    }
}
