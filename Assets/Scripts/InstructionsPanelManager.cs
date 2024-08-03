using Oculus.Interaction;
using Oculus.VoiceSDK.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsPanelManager : MonoBehaviour
{
    //booleans for checking progress
    public bool isWearingGloves = false;
    // public bool centrifugeIsOpen = false;
    public bool twoTubesInCentrifuge = false;
    public bool centrifugeButtonPressed = false;
    public bool pipettePickedUp = false;
    public bool splitBloodTubePickedUp = false;
    public bool plasmaDrawn = false;
    public bool stageOneComplete = false;

    //Centrifuge controller
    public CentrifugeController centrifugeController;

    //Panel groups
    public GameObject firstThreeWallPanels;
    public GameObject secondThreeWallPanels;
    public GameObject pipetteInstructionsWallPanel;
    public GameObject dispensePlasmaWallPanel;
    private GameObject[] FacePanels;


    //Borders;
    public GameObject putOnGlovesBorder;
    public GameObject openCentrifugeBorder;
    public GameObject placeTubesInCentrifugeBorder;
    public GameObject closeCentrifugeInstructionBorder;
    public GameObject centrifugeButtonPressedBorder;
    public GameObject pipettePickedUpBorder;
    public GameObject splitBloodPickedUpBorder;
    public GameObject plasmaDrawnBorder;

    //Audio
    public AudioSource UIAudio;
    public AudioSource stageCompleteAudio;
    private bool pipetteAudioHasPlayed = false;
    private bool bloodTubeAudioHasPlayed = false;

    public void Start()
    {
        FacePanels = GameObject.FindGameObjectsWithTag("MainUIPanel");

    }

    // Update is called once per frame. This is required for things such as the centrifuge hinge angle which is frequently updated
    void Update()
    {
/*        centrifugeIsOpen = centrifugeController.centrifugeIsOpen;
        if (centrifugeIsOpen)
        {
            openCentrifugeBorder.SetActive(true);
            closeCentrifugeInstructionBorder.SetActive(false);
        }
        else
        {
            openCentrifugeBorder.SetActive(false);
            closeCentrifugeInstructionBorder.SetActive(true);
        }*/
    }
    //Checking if there are 2 tubes in the centrifuge slot. This variable is modified by the "centrifuge socket controller" class
    public void UpdateTubesInSockets(int numTubes)
    {
        //Opening the second set of 3 instructions panels after a short delay
        if (numTubes == 2 && isWearingGloves &&!stageOneComplete)
        {
            placeTubesInCentrifugeBorder.SetActive(true);
            UIAudio.Play();
            StartCoroutine(ShowSecondInstructionsWallPanel());
        }

        else if (numTubes == 2 && !isWearingGloves)
        {
            UIAudio.Play();
            placeTubesInCentrifugeBorder.SetActive(true);
        }

    }

    public void PickUpPipette(bool isHeld)
    {
        if (isHeld && stageOneComplete)
        {
            pipettePickedUpBorder.SetActive(true);
            if (!pipetteAudioHasPlayed)
            {
                UIAudio.Play();
                pipetteAudioHasPlayed = true;
            }
        } else
        {
            pipettePickedUpBorder.SetActive(false);
        }
    }

    public void PickUpSplitBlood(bool isHeld)
    {
        if (isHeld && stageOneComplete)
        {
            splitBloodPickedUpBorder.SetActive(true);
            if (!bloodTubeAudioHasPlayed)
            {
                UIAudio.Play();
                bloodTubeAudioHasPlayed = true;
            }
        }
        else
        {
            splitBloodPickedUpBorder.SetActive(false);
        }
    }

    public void DrawPlasma()
    {
        plasmaDrawnBorder.SetActive(true);
        StartCoroutine(ShowDispensePlasmaInstructions());
    }



    public void PutOnGloves()
    {

            isWearingGloves = true;
            putOnGlovesBorder.SetActive(true);
            UIAudio.Play();

    }

/*    public void PressCentrifugeButton()
    {
        //Activating the 5th final if conditions are met
        if (centrifugeController.numberOfTubesInSockets >= 2 && !stageOneComplete)
        {
            centrifugeButtonPressedBorder.SetActive(true);
            UIAudio.Play();
            StartCoroutine(ShowPipetteInstructionsFacePanel());
            stageOneComplete = true;
        }
    }*/

    IEnumerator ShowSecondInstructionsWallPanel()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1);

        // Deactivate firstInstructionsPanel and activate secondInstructionsPanel
        firstThreeWallPanels.SetActive(false);
        secondThreeWallPanels.SetActive(true);
    }


    IEnumerator ShowPipetteInstructionsFacePanel()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(10);

        // Deactivate wall panel and activate face panels
        secondThreeWallPanels.SetActive(false);

        if (FacePanels != null)
        {
            foreach (var gameobj in FacePanels)
            {
                gameobj.SetActive(true);
                
            }
        }

        else
        {
            Debug.LogWarning("Can't find panel");
        }


        stageCompleteAudio.Play();
    }

    IEnumerator ShowDispensePlasmaInstructions()
    {
        // Wait for 1 second
        yield return new WaitForSeconds(1);

        // Deactivate wall panel and activate face panels
        pipetteInstructionsWallPanel.SetActive(false);
        dispensePlasmaWallPanel.SetActive(true);
    }
}
