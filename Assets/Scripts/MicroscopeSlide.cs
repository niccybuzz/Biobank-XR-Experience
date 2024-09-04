using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroscopeSlide : SnappablePlatform
{
    public InstructionsPanelManager panelManager;
    public InstructionsPanelManager previousStep;

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
        if (panelManager != null && previousStep != null)
        {
            if (previousStep.StepComplete)
            {
                panelManager.NextPanel(1f);
            }
        }
    }
    

}
