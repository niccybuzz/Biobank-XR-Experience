using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{

    public Image slideImage;
    private Microtome _microtome;
    void Start()
    {
        _microtome = GameObject.Find("Microtome").GetComponent<Microtome>();
        if (_microtome == null )
        {
            Debug.LogWarning("Microtome Not Found");
            return;
        }
        GameObject sampleBlock = _microtome.MostRecentBlockAttached;
        if ( sampleBlock == null )
        {
            Debug.LogWarning("Can't get most recent block attached");
        }
        Sprite blockImage = sampleBlock.GetComponentInChildren<Image>().sprite;
        if (blockImage != null)
        {
        }
        
        slideImage.sprite = blockImage;
    }

}
