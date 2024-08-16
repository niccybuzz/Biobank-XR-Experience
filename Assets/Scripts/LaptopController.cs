using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the laptop in the Tissue Analysis scene
/// References various fields to text boxes and images on the laptop screen, and takes its data from the instance of Medical Database
/// </summary>
public class LaptopController : MonoBehaviour
{
    private MedicalDatabase _medicalDatabase;
    private List<Illness> _illnesses;

    private int _currentIndex = 0;

    public TextMeshProUGUI illnessNameTextBox;
    public TextMeshProUGUI symptomsTextBox;
    public TextMeshProUGUI treatmentsTextBox;
    public Image displayImage;

    public PopupMessage popupMessage;
    public AudioSource correctChoiceSound;
    public AudioSource wrongChoiceSound;
    public InstructionsPanelManager instructionsPanelManager;

    public Image tabletImage;
    void Start()
    {
        _medicalDatabase = GameObject.Find("Medical Database").GetComponent<MedicalDatabase>();
        _illnesses = _medicalDatabase.IllnessList;
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

    // Populates the laptop screen with information about the illness at the current index
    void DisplayPage(int index)
    {
        string symptoms = GetSymptomsTreatments(_illnesses[index].Symptoms);
        string treatments = GetSymptomsTreatments(_illnesses[index].Treatments);

        illnessNameTextBox.text = _illnesses[index].Name;
        displayImage.sprite = _illnesses[index].Sprite;
        symptomsTextBox.text = symptoms;
        treatmentsTextBox.text = treatments;
    }

    /*
     * This is required because the symptoms and treatments contained in the Medical Database are in a List
     * Use this to append them all to a singke string to be displayed on the laptop screen
     */
    string GetSymptomsTreatments(List<string> illnesses)
    {
        string result = "";

        foreach (var item in illnesses)
        {
            result += item + "\n";
        }
        return result;
    }

    public virtual void SelectButton()
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
            instructionsPanelManager.NextPanel(1f);
            instructionsPanelManager.ShowFacePanels(1f);
            instructionsPanelManager.CompleteStage(1f);

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