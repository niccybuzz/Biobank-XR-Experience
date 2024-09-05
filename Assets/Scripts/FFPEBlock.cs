using TMPro;
using UnityEngine;

/**
 * Manages the image component of the FFPE Blocks
 * Loads all images at the start of scene then attaches a random one
 */
public class FFPEBlock : MonoBehaviour
{
    private Sprite _image;
    public ImageGallery imageGallery;

    public Sprite Image { get => _image; set => _image = value; }

    private void Start()
    {
        imageGallery.LoadImages();

        Image = imageGallery.GetRandomImage();
    }

    private void AttachRandomImage()
    {
        Sprite randomImage = imageGallery.GetRandomImage();
        Image = randomImage;
    }


}
