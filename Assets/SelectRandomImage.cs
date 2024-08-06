using UnityEngine;
using UnityEngine.UI;

public class SelectRandomImage : MonoBehaviour
{
    // Start is called before the first frame update
    public ImageGallery _imageGallery;
    public Image associatedImage;
    void Start()
    {
        if(_imageGallery != null)
        {
            AttachRandomImage();
        }
    }

    private void AttachRandomImage()
    {
        Sprite randomImage = _imageGallery.GetRandomImage();
        associatedImage.sprite = randomImage;
    }

}
