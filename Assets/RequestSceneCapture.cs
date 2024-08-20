using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestSceneCapture : MonoBehaviour
{
    // Start is called before the first frame update
    public OVRSceneManager manager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Request()
    {
        manager.RequestSceneCapture();
    }
}
