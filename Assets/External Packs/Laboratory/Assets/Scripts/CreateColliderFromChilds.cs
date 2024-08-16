using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateColliderFromChilds : MonoBehaviour
{
    /*
     * 
     * This is used as an alternative to drawing out every collider
     * The original gamemakers just decided to not do some of the collders and this just makes it easier
     * Some of the colliders may not be completely accurate but \( T_T )/
     * 
     */
    void Start()
    {
        AddColliderAroundChildren(this.gameObject);
    }

    private Collider AddColliderAroundChildren(GameObject assetModel)
    {

        var pos = assetModel.transform.localPosition;
        var rot = assetModel.transform.localRotation;
        var scale = assetModel.transform.localScale;

        // need to clear out transforms while encapsulating bounds
        assetModel.transform.localPosition = Vector3.zero;
        assetModel.transform.localRotation = Quaternion.identity;
        assetModel.transform.localScale = Vector3.one;

        // start with root object's bounds
        var bounds = new Bounds(Vector3.zero, Vector3.zero);
        if (assetModel.transform.TryGetComponent<Renderer>(out var mainRenderer))
        {
            bounds = mainRenderer.bounds;
        }

        var descendants = assetModel.GetComponentsInChildren<Transform>();
        foreach (Transform desc in descendants)
        {
            if (desc.gameObject.name == "EXCLUDE")
            {
                continue;
            }
            
            if (desc.TryGetComponent<Renderer>(out var childRenderer))
            {
                //if initialized to renderer bounds yet
                if (bounds.extents == Vector3.zero)
                    bounds = childRenderer.bounds;
                bounds.Encapsulate(childRenderer.bounds);
            }
        }

        var boxCol = assetModel.transform.parent.gameObject.AddComponent<BoxCollider>();
        boxCol.center = bounds.center - assetModel.transform.position;
        boxCol.size = bounds.size;

        // restore transforms
        assetModel.transform.localPosition = pos;
        assetModel.transform.localRotation = rot;
        assetModel.transform.localScale = scale;
        return boxCol;
    }
}
