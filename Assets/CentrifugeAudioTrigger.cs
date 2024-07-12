using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentrifugeAudioTrigger : MonoBehaviour
{
    public AudioSource UIAudio;
    private bool openAudioHasPlayed = false;
    private bool closeAudioHasPlayed = false;

    public CentrifugeController centrifuge;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
 

        if (other.CompareTag("LidTrigger"))
        {
            ToggleTubeGrabInteractions(true);
            if (!openAudioHasPlayed)
            {

                UIAudio.Play();
                openAudioHasPlayed = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LidTrigger"))
        {
            ToggleTubeGrabInteractions(false);
            if (!closeAudioHasPlayed)
            {
                UIAudio.Play();
                closeAudioHasPlayed = true;
            }
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
        } else
        {
            Debug.LogWarning("Can't find blood tubes");
        }
    }

}
