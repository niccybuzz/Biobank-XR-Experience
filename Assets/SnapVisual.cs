using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapVisual : MonoBehaviour
{
    public GameObject snapVisual;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TestTube"))
        {
            snapVisual.SetActive(true);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        snapVisual.SetActive(false);
    }
}
