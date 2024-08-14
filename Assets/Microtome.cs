using Meta.WitAi;
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Microtome : MonoBehaviour
{
    public SnapInteractable platform;
    private GameObject mostRecentBlockAttached;
    public TextMeshProUGUI debugText;

    public GameObject MostRecentBlockAttached { get => mostRecentBlockAttached; set => mostRecentBlockAttached = value; }

    public void AttachBlock()
    {
        mostRecentBlockAttached = GetBlockOnPlatform();
        if (mostRecentBlockAttached != null)
        {
            Sprite blockImage = mostRecentBlockAttached.GetComponent<FFPEBlock>().Image;
        debugText.text = blockImage.name + " attached";
        }
    }

    public void DetachBlock()
    {
        debugText.text = mostRecentBlockAttached.name + " detached";
    }

    public GameObject GetBlockOnPlatform()
    {
        GameObject blockOnPlatform = null;

        foreach (var selectingInterator in platform.SelectingInteractors)
        {
            //Getting the parent gameobject (i.e. the actual slice)
            Transform block = selectingInterator.gameObject.transform.parent;
            blockOnPlatform = block.gameObject;
        }

        return blockOnPlatform;
    }

    public void ClearBlocksOnPlatform()
    {
        foreach (var selectingInterator in platform.SelectingInteractors)
        {
            Transform slice = selectingInterator.gameObject.transform.parent;
            slice.gameObject.SetActive(false);

        }
    }

}
