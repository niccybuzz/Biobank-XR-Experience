using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentrifugeAudioTrigger : MonoBehaviour
{
    public AudioSource UIAudio;
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
        List<GameObject> bloodTubes = centrifuge.GetAllBloodTubesInCentrifuge(centrifuge.bloodTubeSockets);
        if (bloodTubes.Count > 0)
        {
            foreach (GameObject blood in bloodTubes)
            {
                if (enable)
                {
                    blood.GetComponentInChildren<HandGrabInteractable>().enabled = true;
                } else
                {
                    blood.GetComponentInChildren<HandGrabInteractable>().enabled = false;
                }
                
            }
        } 
    }

}
