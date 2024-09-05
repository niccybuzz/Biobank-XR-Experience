using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used to trigger opening and closing of the centrifuge, turning on/off hand grab interactions with the sockets
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

    //Disables any grab interactions with tubes inside the centrifuge to stop weird grabbing behaviours with tubes while it is spinning
    public void ToggleTubeGrabInteractions(bool enable)
    {
        List<GameObject> bloodTubes = centrifuge.GetAllBloodTubesInCentrifuge(centrifuge.BloodTubeSockets);
        if (bloodTubes.Count > 0)
        {
            foreach (GameObject tube in bloodTubes)
            {
                if (enable)
                {
                    //get the hand grab interactable component in each tube and enable
                    HandGrabInteractable[] handInteractions = tube.GetComponentsInChildren<HandGrabInteractable>();
                    foreach (var item in handInteractions)
                    {
                        item.enabled = true;
                    }
                }
                else
                {
                    //get the hand grab interactable component in each tube and disable
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
