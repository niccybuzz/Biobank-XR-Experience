using Oculus.Interaction;
using UnityEngine;

/*
 * Governs the behaviour of the slices produced from the sample blocks when operating the microtome
 */
public class Slice : MonoBehaviour
{
    // The png image associated with the slide, determined when the slice is produced from the sample block being cut
    private Sprite _sliceImage;
    // A reference to the microtome used to retrieve the current sample block attached
    private Microtome _microtome;

    public Sprite SliceImage { get => _sliceImage; set => _sliceImage = value; }

    // I don't know why Start() doesn't work for this, but it doesn't
    void OnEnable()
    {
        _microtome = GameObject.Find("Microtome").GetComponent<Microtome>();

        if (_microtome != null)
        {
            SliceImage = GetImageFromBlock();
        }
    }

    // Once soaked, the snap interactor is enabled, allowing the user to place it on a microscope slide
    public void SoakSlice()
    {
        GetComponentInChildren<SnapInteractor>().enabled = true;
    }

    // Retrieves the image from the block attached to the microtome. Ensures that the same image is produced for every slice cut from the same block
    public Sprite GetImageFromBlock()
    {
        GameObject sampleBlock = _microtome.MostRecentBlockAttached;
        Sprite blockImage;
        if (sampleBlock != null)
        {
            blockImage = sampleBlock.GetComponent<FFPEBlock>().Image;
            return blockImage;
        }
        else
        {
            Debug.Log("Can't find most recent block attached");
            return null;
        }

    }

}
