using UnityEngine;

public class SoakSlice : MonoBehaviour
{
    public AudioSource plop;
    private void OnTriggerEnter(Collider other)
    {
        if (!plop.isPlaying)
        {
            plop.Play();
        }

        if (other.CompareTag("Slice"))
        {
            other.GetComponent<Slice>().SoakSlice();
        }
    }
}
