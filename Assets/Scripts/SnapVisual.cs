using UnityEngine;

/*
 * Triggers a visual "outline" of the object to be snapped on the snappable platform
 */
public class SnapVisual : MonoBehaviour
{
    public string triggeringObjectFilter; // String used to filter which objects can trigger the snap visuals
    public GameObject snapVisual;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggeringObjectFilter))
        {
            snapVisual.SetActive(true);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        snapVisual.SetActive(false);
    }
}
