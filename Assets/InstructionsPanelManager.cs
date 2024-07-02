using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class InstructionsPanelManager : MonoBehaviour
{
    //booleans for checking progress
    public bool isWearingGloves = false;
    public bool centrifugeIsOpen = false;
    public bool twoTubesInCentrifuge = false;
    public bool centrifugeButtonPressed = false;

    //Centrifuge controller
    public CentrifugeController centrifugeController;

    //Panel groups
    public GameObject firstInstructionsPanel;
    public GameObject secondInstructionsPanel;
    private GameObject thirdInstructionsPanel;
    private GameObject[] mainUIPanels;


    //Borders;
    public GameObject putOnGlovesBorder;
    public GameObject openCentrifugeBorder;
    public GameObject placeTubesInCentrifugeBorder;
    public GameObject closeCentrifugeInstructionBorder;
    public GameObject centrifugeButtonPressedBorder;

    //Audio
    public AudioSource UIAudio;
    public AudioSource stageCompleteAudio;
    public bool audioHasPlayed = false;

    public void Start()
    {
        mainUIPanels = GameObject.FindGameObjectsWithTag("MainUIPanel");
    }

    // Update is called once per frame. This is required for things such as the centrifuge hinge angle which is frequently updated
    void Update()
    {
        centrifugeIsOpen = centrifugeController.centrifugeIsOpen;
        if (centrifugeIsOpen)
        {
            openCentrifugeBorder.SetActive(true);
            closeCentrifugeInstructionBorder.SetActive(false);
        }
        else
        {
            closeCentrifugeInstructionBorder.SetActive(true);
        }


    }

    //This is called once every time one of the booleans is updated, instead of putting in Update and checking every frame.
    public void CheckProgress()
    {
        //Checking if gloves are on to activate first border
        if (isWearingGloves)
        {
            putOnGlovesBorder.SetActive(true);
            UIAudio.Play();
        }

        //Checking if there are 2 tubes in the centrifuge slot. This variable is modified by the "centrifuge socket controller" class
        if (twoTubesInCentrifuge)
        {
            placeTubesInCentrifugeBorder.SetActive(true);
            UIAudio.Play();
        }

        //Opening the second set of 3 instructions panels after a short delay
        if (isWearingGloves && centrifugeIsOpen && twoTubesInCentrifuge)
        {
            StartCoroutine(OpenSecondInstructionsPanel());
            UIAudio.Play();
            audioHasPlayed = false;
        }

        //Activating the 5th final if conditions are met
        if (centrifugeButtonPressed && twoTubesInCentrifuge)
        {
            centrifugeButtonPressedBorder.SetActive(true);
            UIAudio.Play();
        }

        //
        if (centrifugeButtonPressed && !centrifugeIsOpen && twoTubesInCentrifuge)
        {
            StartCoroutine(OpenThirdInstructionsPanel());
        }
    }

    //Plays a bleep sound the first time the centrifuge has been opened
    public void PlayUIAudioOneTime()
    {
        if (!audioHasPlayed)
        {
            UIAudio.Play();
            audioHasPlayed = true;
        }
    }

    IEnumerator OpenSecondInstructionsPanel()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(2);

        // Deactivate firstInstructionsPanel and activate secondInstructionsPanel
        firstInstructionsPanel.SetActive(false);
        secondInstructionsPanel.SetActive(true);
    }


    IEnumerator OpenThirdInstructionsPanel()
    {

        // Wait for 1 second
        yield return new WaitForSeconds(8);

        // Deactivate firstInstructionsPanel and activate secondInstructionsPanel
        secondInstructionsPanel.SetActive(false);

        if (mainUIPanels != null)
        {
            foreach (var gameobj in mainUIPanels)
            {
                gameobj.SetActive(true);
            }
        } else
        {
            Debug.LogWarning("Can't find panel");
        }
        

        stageCompleteAudio.Play();
    }
}
