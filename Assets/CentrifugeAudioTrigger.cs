using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentrifugeAudioTrigger : MonoBehaviour
{
    public AudioSource UIAudio;
    private bool audioHasPlayed = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {

  
        if (other.CompareTag("LidTrigger"))
        {
            if (!audioHasPlayed)
            {
                UIAudio.Play();
                audioHasPlayed = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audioHasPlayed = false;
        if (other.CompareTag("LidTrigger"))
        {
            UIAudio.Play();
            audioHasPlayed = true;
        }
    }
}
