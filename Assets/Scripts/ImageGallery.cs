using UnityEngine;
using System.Collections.Generic;

// Loads a list of images from a specified directory
public class ImageGallery : MonoBehaviour
{
    public string imageFolder = "Images/ParaffinBlockImages";     
    protected List<Sprite> _images;

    public List<Sprite> Images { get => _images; }

    void Start()
    {
        LoadImages();
    }

    public void LoadImages()
    {
        // Load all sprites from the specified folder in Resources
        Sprite[] loadedImages = Resources.LoadAll<Sprite>(imageFolder);
        if (loadedImages.Length > 0)
        {
            _images = new List<Sprite>(loadedImages);
        }
        else
        {
            Debug.LogWarning("No images found at the specified path: " + imageFolder);
        }
    }

    public Sprite GetRandomImage()
    {
        if (_images == null || _images.Count == 0)
        {
            Debug.LogError("No images available to select.");
            return null;
        }

        int randomImageNo = Random.Range(0, Images.Count-1);
        Sprite randomImage = Images[randomImageNo];
        return randomImage;
    }
}

