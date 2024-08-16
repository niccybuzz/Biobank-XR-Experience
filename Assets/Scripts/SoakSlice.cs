using UnityEngine;

public class SoakSlice : MonoBehaviour
{
    public AudioSource plop;
    public InstructionsPanelManager instructionsManager;
    private void OnTriggerEnter(Collider other)
    {
        if (!plop.isPlaying)
        {
            plop.Play();
        }

        if (other.CompareTag("Slice"))
        {
            other.GetComponent<Slice>().SoakSlice();
            if (instructionsManager != null)
            {
                instructionsManager.NextPanel(1f);
            }
        }
    }
}
