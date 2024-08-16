using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeSlide : SnappablePlatform
{

    private Sprite slideImage;

    public Sprite SlideImage { get => slideImage; set => slideImage = value; }

    // When a sample slice is placed on a microscope slide, gets the image from that sample slice
    public void AttachSampleSlice()
    {
        GameObject sampleSliceAttached = GetObjectOnPlatform();

        if (sampleSliceAttached != null)
        {
            Slice slice = sampleSliceAttached.GetComponent<Slice>();
            slideImage = slice.SliceImage;
        }
    }
    

}
