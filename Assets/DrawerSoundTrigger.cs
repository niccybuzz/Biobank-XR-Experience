using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerSoundTrigger : MonoBehaviour
{
    public AudioSource openSound;
    public AudioSource closeSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drawer") && !openSound.isPlaying)
        {
            openSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Drawer") && !closeSound.isPlaying)
        {
            closeSound.Play();
        }
    }
}
