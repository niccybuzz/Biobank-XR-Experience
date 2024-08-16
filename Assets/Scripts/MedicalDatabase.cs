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
            new Illness("Alzheimer's", new List<string> {"Memory Loss", "Confusion" }, new List<string>{"Donepezil", "Cognitive Therapy" }, images[0]),
            new Illness("Cardiovascular Disease", new List<string>{ "Chest Pain", "Shortness of Breath" }, new List<string> {"Statins", "Beta-blockers"}, images[1]),
            new Illness("HIV/AIDS", new List<string>{"Fatigue", "Recurrent Infections" }, new List<string>{"Antiretroviral Therapy (ART)"}, images[2]),
            new Illness("Rheumatoid Arthritis", new List<string>{"Joint Pain", "Swelling" }, new List<string>{"DMARDs", "Biologics" }, images[3]),
            new Illness("Type 1 Diabetes", new List<string> {"Frequent Urination", "Extreme Thirst" }, new List<string>{"Insulin Therapy" } , images[4]),
            new Illness("Asthma", new List<string>  {"Wheezing", "Shortness of Breath" }, new List<string> {"Inhaled Corticosteroids", "Bronchodilators" }, images[5]),
            new Illness("Schizophrenia", new List<string>{"Hallucinations", "Delusions" }, new List<string>{"Antipsychotic Medication", "Psychotherapy"},  images[6]),
            new Illness("Osteoporosis", new List<string>{"Bone Fractures", "Loss of height" }, new List<string>{"Biphosphonates", "Calcium and Vit D Supplements"}, images[7]),
            new Illness("COPD", new List<string> {"Chronic Cough", "Difficulty Breathing" }, new List<string>{"Bronchiodilators", "Inhaled Steroids" }, images[8])
        };
    }
}
