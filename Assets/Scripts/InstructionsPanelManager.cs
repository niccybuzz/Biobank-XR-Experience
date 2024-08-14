using System.Collections;
using UnityEngine;

public class InstructionsPanelManager2 : MonoBehaviour
{
    public GameObject currentPanel;
    private GameObject confirmationBorder;
    public GameObject nextPanel;
    private AudioSource bleep;
    private AudioSource stageCompleteBleep;
    private GameObject[] FacePanels;
 
    private bool stepComplete = false;

    public bool StepComplete { get => stepComplete; set => stepComplete = value; }

    public void Start()
    {
        confirmationBorder = GameObject.Find("BS_InstructionsBorder");
        bleep = GameObject.Find("UIHappyBleep").GetComponent<AudioSource>();
        stageCompleteBleep = GameObject.Find("StageCompleteAudio").GetComponent<AudioSource>();
        FacePanels = GameObject.FindGameObjectsWithTag("MainUIPanel");
    }
    public void NextPanel(float seconds)
    {
        if (!StepComplete)
        {
            bleep.Play();
            confirmationBorder.GetComponent<Canvas>().enabled = true;
            StartCoroutine(ShowNextPanelCoroutine(seconds));
            StepComplete = true;
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
        confirmationBorder.GetComponent<Canvas>().enabled = false;
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
