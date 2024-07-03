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
        int tubesInSockets = controller.numberOfTubesInSockets;
        if (tubesInSockets >= 2)
        {
            instructionsManager.UpdateTubesInSockets(tubesInSockets);
        }
        

    }

    public void DecrementNumberOfTubes()
    {
        controller.numberOfTubesInSockets--;
        int tubesInSockets = controller.numberOfTubesInSockets;
        Debug.Log(tubesInSockets);
        instructionsManager.UpdateTubesInSockets(tubesInSockets);
    }
}
