using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SliceBlock : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SampleBlock"))
        {
            Debug.LogWarning("Block Entered Trigger Zone");
            GameObject childSlice = other.GetComponent<SampleBlockManager>().associatedSlice;
            childSlice.SetActive(true);
        }
    }
}
