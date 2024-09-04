using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Produces a list of illnesses, containing a sprite from the ImageGallery and symptoms/treatments produced by the IllnessRepository class

public class MedicalDatabase : ImageGallery
{
    private List<Illness> _illnessList;

    public List<Illness> IllnessList { get => _illnessList; set => _illnessList = value; }

    // Start is called before the first frame update
    void Start()
    {
        LoadImages();
        _illnessList = IllnessRepository.GetAllIllnesses(Images);
        if (IllnessList != null)
        {
        }
        else
        {
            Debug.LogWarning("No list found");
        }
    }
}
//Represents an Illness
public class Illness
{
    private string name;
    private List<string> symptoms;
    private List<string> treatments;
    private Sprite sprite;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public string Name { get => name; set => name = value; }
    public List<string> Symptoms { get => symptoms; set => symptoms = value; }
    public List<string> Treatments { get => treatments; set => treatments = value; }

    public Illness(string name, List<string> symptoms, List<string> treatments, Sprite sprite)
    {
        Name = name;
        Symptoms = symptoms;
        Treatments = treatments;
        Sprite = sprite;
    }
}


//Creates a list of illnesses, coupling each one with a static image from the image gallery
public class IllnessRepository
{
    public static List<Illness> GetAllIllnesses(List<Sprite> images)
    {
        if (images == null )
        {
            Debug.LogWarning("The images list must contain at least 9 elements.");
        }
        return new List<Illness>
        {
            new Illness("Breast Cancer", new List<string> {"Lumps/Thickened Tissue", "Breast Shape/Size Changes" }, new List<string>{"Tamoxifen", "Trastuzumab"}, images[0]),
            new Illness("Lung Cancer", new List<string>{ "Persistent Cough", "Shortness of Breath" }, new List<string> {"Cisplatin", "Erlotinib"}, images[1]),
            new Illness("Colorectal Cancer", new List<string>{"Bowek Movement Changes", "Bloody Stools" }, new List<string>{"Oxa[liplatin", "5-Fluorouracil"}, images[2]),
            new Illness("Ovarian Cancer", new List<string>{"Abdominal Bloating or Swelling", "Pelvin Pain" }, new List<string>{"Carboplatin", "Olaparib" }, images[3]),
        };
    }
}
