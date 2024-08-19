using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FFPEBlock : MonoBehaviour
{
    private Sprite image;
    public TextMeshProUGUI debugText;
    public ImageGallery _imageGallery;

    public Sprite Image { get => image; set => image = value; }

    private void Start()
    {
        _imageGallery.LoadImages();

        Image = _imageGallery.GetRandomImage();
    }

    private void AttachRandomImage()
    {
        Sprite randomImage = _imageGallery.GetRandomImage();
        Image = randomImage;
    }


}
