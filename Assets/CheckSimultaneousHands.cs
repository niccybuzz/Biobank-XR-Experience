using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSimultaneousHands : MonoBehaviour
{
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckStatus()
    {
        bool isEnabled = OVRPlugin.IsMultimodalHandsControllersSupported();
        if (isEnabled)
        {
            Debug.LogWarning("Should be enabled");
        }
        else
        {
            Debug.LogWarning("Not enabled");
        }
    }
}
