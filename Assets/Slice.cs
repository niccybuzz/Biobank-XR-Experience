using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slice : MonoBehaviour
{

    private Sprite sliceImage;
    private Microtome _microtome;
    public TextMeshProUGUI debugText;
    private bool hasBeenSoaked = false;

    public Sprite SliceImage { get => sliceImage; set => sliceImage = value; }

    void OnEnable()
    {
        _microtome = GameObject.Find("Microtome").GetComponent<Microtome>();
        if (_microtome == null)
        {
            debugText.text = "Can't find microtome";
        }
        else
        {
            SliceImage = GetImageFromBlock();
            debugText.text = SliceImage.name;
        }
    }

    public void SoakSlice()
    {
        hasBeenSoaked = true;
        GetComponentInChildren<SnapInteractor>().enabled = true;
    }

    public Sprite GetImageFromBlock()
    {
        GameObject sampleBlock = _microtome.MostRecentBlockAttached;
        Sprite blockImage;
        if (sampleBlock == null)
        {
            debugText.text = ("Can't get most recent block attached");
        }

        blockImage = sampleBlock.GetComponent<FFPEBlock>().GetBlockImage();
        if (blockImage == null)
        {
            debugText.text = "Can't get the block image";
        }
        else
        {
            debugText.text = blockImage.name + ", success";
        }

        return blockImage;
    }

}
