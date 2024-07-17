using System.Collections;
using UnityEngine;

public class InstructionsPanelManager2 : MonoBehaviour
{
    public GameObject currentPanel;
    public GameObject confirmationBorder;
    public GameObject nextPanel;
    private AudioSource bleep;
    private AudioSource stageCompleteBleep;
    private GameObject[] FacePanels;
 
    private bool stepComplete = false;

    public bool GetStepComplete()
    {
        return stepComplete;
    }
    public void Start()
    {
        bleep = GameObject.Find("UIHappyBleep").GetComponent<AudioSource>();
        stageCompleteBleep = GameObject.Find("StageCompleteAudio").GetComponent<AudioSource>();
        FacePanels = GameObject.FindGameObjectsWithTag("MainUIPanel");
    }
    public void NextPanel(float seconds)
    {
        if (!stepComplete)
        {
            bleep.Play();
            confirmationBorder.SetActive(true);
            StartCoroutine(ShowNextPanelCoroutine(seconds));
            stepComplete = true;
        }
    }

    public void ShowFacePanels(float waitForSeconds)
    {
        StartCoroutine(ShowFacePanelsCoroutine(waitForSeconds));
    }

    public void CompleteStage(float seconds)
    {
        StartCoroutine(CompleteStageCoroutine(seconds));
    }

    IEnumerator ShowNextPanelCoroutine(float seconds) 
    {
        yield return new WaitForSeconds(seconds);
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
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
        stageCompleteBleep.Play();
    }
}
