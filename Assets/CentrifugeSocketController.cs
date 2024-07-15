using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CentrifugeSocketController : MonoBehaviour
{
    public CentrifugeController controller;
    

    public void IncrementNumberOfTubes()
    {
        controller.IncrementTubesInSockets();
    }

    public void DecrementNumberOfTubes()
    {
        controller.DecrementTubesInSockets();
    }
}
