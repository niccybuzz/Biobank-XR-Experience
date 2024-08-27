using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A parent class which can be extended by all classes which have a snap interactable component
/// Useful since many gameobjects have platforms and we need to be able to retrieve the gameObject attached
/// </summary>
public class SnappablePlatform : MonoBehaviour
{
    [SerializeField]
    SnapInteractable platform;

    public GameObject GetObjectOnPlatform()
    {
        GameObject blockOnPlatform = null;

        /* 
         * Although we are using a foreach here, only 1 selecting interactor is permitted on the platform, so only a single game object can be returned
         * This is because there is no method provided for accessing a single interactor
         */
        foreach (var selectingInterator in platform.SelectingInteractors)
        {
            /*
             * Getting the parent gameobject (i.e. the actual slice)
             * Have to use Transform instead of gameobject to navigate through parent / child relationships
             */
            Transform blockTransform = selectingInterator.gameObject.transform.parent;
            blockOnPlatform = blockTransform.gameObject;
        }

        return blockOnPlatform;
    }
}
