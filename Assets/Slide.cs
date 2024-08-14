using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{

    private Sprite slideImage;
    private Microtome _microtome;
    public TextMeshProUGUI debugText;

    public Sprite SlideImage { get => slideImage; set => slideImage = value; }

    void OnEnable()
    {
        _microtome = GameObject.Find("Microtome").GetComponent<Microtome>();
        if (_microtome == null)
        {
            debugText.text = "Can't find microtome";
        }
        else
        {
            SlideImage = GetImageFromBlock();
            debugText.text = SlideImage.name;
        }
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
