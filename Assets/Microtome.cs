using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microtome : MonoBehaviour
{
    public SnapInteractable platform;
    private GameObject mostRecentBlockAttached;

    public GameObject MostRecentBlockAttached { get => mostRecentBlockAttached; set => mostRecentBlockAttached = value; }

    public void OnEnable()
    {
        mostRecentBlockAttached = GetBlockOnPlatform();
    }

    public GameObject GetBlockOnPlatform()
    {
        GameObject blockOnPlatform = null;

        foreach (var selectingInterator in platform.SelectingInteractors)
        {
            //Getting the parent gameobject (i.e. the actual slice)
            Transform slice = selectingInterator.gameObject.transform.parent;
            blockOnPlatform = slice.gameObject;
        }

        return blockOnPlatform;
    }
    
}
