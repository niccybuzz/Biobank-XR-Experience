using System.Collections;
using UnityEngine;

/**
 * Used to handle the activation and deactivation of the instructions panels in a controlled and sequenced fashion
 */
public class InstructionsPanelManager : MonoBehaviour
{
    //Variables for the current and next panel and the border
    public GameObject currentPanel;
    private GameObject _confirmationBorder;
    public GameObject nextPanel;

    // Variables for sound effects
    private AudioSource _bleep;
    private AudioSource _stageCompleteBleep;

    private GameObject[] FacePanels;

    // Used by other scripts to check whether a certain step has been completed or not
    private bool _stepComplete = false;
    public bool StepComplete { get => _stepComplete; set => _stepComplete = value; }

    // Plays the sound effect, sets step to complete, and enables the green border
    public void NextPanel(float seconds)
    {
        if (!StepComplete)
        {
            _bleep.Play();
            StartCoroutine(ShowNextPanelCoroutine(seconds));
            StepComplete = true;
        }
    }

    // Does the same as NextPanel, but checks that a previous IPM has been completed already
    public void NextPanelCheckPrevious(float seconds, InstructionsPanelManager prevStep)
    {
        if (!StepComplete && prevStep.StepComplete)
        {
            _bleep.Play();
            StartCoroutine(ShowNextPanelCoroutine(seconds));
            StepComplete = true;
        }
    }

    // Handles the activation and deactivation of current/next panel, after a specified delay
    IEnumerator ShowNextPanelCoroutine(float seconds)
    {
        //activate the border
        _confirmationBorder.GetComponent<Canvas>().enabled = true;
        //wait a moment
        yield return new WaitForSeconds(seconds);
        //deactivate the border and swap the panels
        _confirmationBorder.GetComponent<Canvas>().enabled = false;
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }

    // Fetches references to the border, sound effects, and face panels upon scene startup
    public void Start()
    {
        _confirmationBorder = GameObject.Find("BS_InstructionsBorder");
        _bleep = GameObject.Find("UIHappyBleep").GetComponent<AudioSource>();
        _stageCompleteBleep = GameObject.Find("StageCompleteAudio").GetComponent<AudioSource>();
        FacePanels = GameObject.FindGameObjectsWithTag("MainUIPanel");
    }

    public void ShowFacePanels(float waitForSeconds)
    {
        StartCoroutine(ShowFacePanelsCoroutine(waitForSeconds));
    }

    public void CompleteStage(float seconds)
    {
        StartCoroutine(CompleteStageCoroutine(seconds));
    }

    IEnumerator ShowFacePanelsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentPanel.SetActive(false);
        if (FacePanels != null)
        {
            foreach (var gameobj in FacePanels)
            {
                gameobj.SetActive(true);

            }
        }
    }

    IEnumerator CompleteStageCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        _stageCompleteBleep.Play();
    }
}
