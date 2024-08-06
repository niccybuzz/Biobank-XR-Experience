using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaptopController : MonoBehaviour
{
    private IllnessGallery _illnessGallery;
    private List<Illness> _illnesses;

    private int _currentIndex = 0;
    public Image displayImage;

    public TextMeshProUGUI illnessNameTextBox;
    public TextMeshProUGUI symptomsTextBox;
    public TextMeshProUGUI treatmentsTextBox;
    public PopupMessage popupMessage;
    public AudioSource correctChoiceSound;
    public AudioSource wrongChoiceSound;

    public Image tabletImage;
    void Start()
    {
        _illnessGallery = GameObject.Find("IllnessGallery").GetComponent<IllnessGallery>();
        _illnesses = _illnessGallery.IllnessList;
    }
    public void NextPage()
    {
        if (_illnesses.Count == 0) return;

        _currentIndex = (_currentIndex + 1) % _illnesses.Count;
        DisplayPage(_currentIndex);
    }

    public void PreviousPage()
    {
        if (_illnesses.Count == 0) return;

        _currentIndex = (_currentIndex - 1 + _illnesses.Count) % _illnesses.Count;
        DisplayPage(_currentIndex);
    }

    void DisplayPage(int index)
    {
        string symptoms = GetSymptomsTreatments(_illnesses[index].Symptoms);
        string treatments = GetSymptomsTreatments(_illnesses[index].Treatments);

        illnessNameTextBox.text = _illnesses[index].Name;
        displayImage.sprite = _illnesses[index].Sprite;
        symptomsTextBox.text = symptoms;
        treatmentsTextBox.text = treatments;
    }

    string GetSymptomsTreatments(List<string> illnesses)
    {
        string result = "";

        foreach (var item in illnesses)
        {
            result += item + "\n";
        }
        return result;
    }

    public void SelectButton()
    {
        string message;
        bool matches;
        if (displayImage == null) return;
        if (tabletImage == null) return;

        if (displayImage.sprite == tabletImage.sprite)
        {
            message = "Correct!";
            matches = true;
            if (!correctChoiceSound.isPlaying)
            {
                correctChoiceSound.Play();
            }

        } else
        {
            matches = false;
            message = "Wrong!";
            if (!wrongChoiceSound.isPlaying)
            {
                wrongChoiceSound.Play();
            }
        }
        popupMessage.ShowMessage(message, matches);
    }
}