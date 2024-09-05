using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used to increment / decrement number of tubes in sockets.
 * This info is used by the centrifuge to trigger the instructions panel manager
 */
public class CentrifugeSocketController : SnappablePlatform
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
