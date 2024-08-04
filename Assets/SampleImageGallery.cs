
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageGallery : MonoBehaviour
{
    public string imageFolder = "Images/ParaffinBlockImages"; // Path inside Resources folder
    public Image displayImage; 
    private List<Sprite> images;
    private int currentIndex = 0;

    void Start()
    {
        LoadImages();
        if (images.Count > 0)
        {
            DisplayImage(0);
        }
    }

    void LoadImages()
    {
        // Load all sprites from the specified folder in Resources
        Sprite[] loadedImages = Resources.LoadAll<Sprite>(imageFolder);
        images = new List<Sprite>(loadedImages);
    }

    public void NextImage()
    {
        if (images.Count == 0) return;

        currentIndex = (currentIndex + 1) % images.Count;
        DisplayImage(currentIndex);
    }

    public void PreviousImage()
    {
        if (images.Count == 0) return;

        currentIndex = (currentIndex - 1 + images.Count) % images.Count;
        DisplayImage(currentIndex);
    }

    void DisplayImage(int index)
    {
        displayImage.sprite = images[index];
    }
}
