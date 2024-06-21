using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipCollisionDetector : MonoBehaviour
{
    public bool isInContactWithBlood = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Blood"))
        {
            Debug.Log("Tip in contact with blood");
            isInContactWithBlood = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Blood"))
        {
            Debug.Log("Tip in no longer in contact with blood");
            isInContactWithBlood = false;
        }
    }
}
