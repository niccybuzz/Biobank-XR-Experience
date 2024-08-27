using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentrifugeAudioTrigger : MonoBehaviour
{
    public CentrifugeController centrifuge;


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LidTrigger"))
        {
            ToggleTubeGrabInteractions(true);
            centrifuge.OpenCentrifuge();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LidTrigger"))
        {
            ToggleTubeGrabInteractions(false);
            centrifuge.CloseCentrifuge();
        }
    }

    public void ToggleTubeGrabInteractions(bool enable) 
    {
        //Disabling any grab interactions with tubes inside the centrifuge to stop weird grabbing behaviours with tubbes
        List<GameObject> bloodTubes = centrifuge.GetAllBloodTubesInCentrifuge(centrifuge.BloodTubeSockets);
        if (bloodTubes.Count > 0)
        {
            foreach (GameObject tube in bloodTubes)
            {
                if (enable)
                {
                    HandGrabInteractable[] handInteractions = tube.GetComponentsInChildren<HandGrabInteractable>();
                    foreach (var item in handInteractions)
                    {
                        item.enabled = true;   
                    }
                } else
                {
                    HandGrabInteractable[] handInteractions = tube.GetComponentsInChildren<HandGrabInteractable>();
                    foreach (var item in handInteractions)
                    {
                        item.enabled = false;   
                    }
                }
                
            }
        } 
    }

}
