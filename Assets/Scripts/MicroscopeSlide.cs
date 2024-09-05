using UnityEngine;

/* 
 * Used by the microscope slides, mainly to pass the image from the sample slice to the platform
 */
public class MicroscopeSlide : SnappablePlatform
{
    public InstructionsPanelManager panelManager;
    public InstructionsPanelManager previousStep;

    private Sprite _slideImage;

    public Sprite SlideImage { get => _slideImage; set => _slideImage = value; }


    // When a sample slice is placed on a microscope slide, gets the image from that sample slice
    public void AttachSampleSlice()
    {
        GameObject sampleSliceAttached = GetObjectOnPlatform();

        if (sampleSliceAttached != null)
        {
            Slice slice = sampleSliceAttached.GetComponent<Slice>();
            _slideImage = slice.SliceImage;
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
