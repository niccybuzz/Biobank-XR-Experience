using UnityEngine;

/*
 * Activates snap interactions on slices once soaked in the water bath
 * Also plays a little plop sound effect
 */
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
