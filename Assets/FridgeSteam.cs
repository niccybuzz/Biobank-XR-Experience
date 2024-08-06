using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeSteam : MonoBehaviour
{
    public ParticleSystem smoke;
    public AudioSource fridgeSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FridgeDoor"))
        {
            smoke.Play();
            fridgeSound.Play();
        }
    }
}
