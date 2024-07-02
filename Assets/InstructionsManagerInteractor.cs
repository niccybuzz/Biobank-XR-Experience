using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManagerInteractor : MonoBehaviour
{
    public InstructionsPanelManager instructionsManager;

    public void PressButton()
    {
        instructionsManager.centrifugeButtonPressed = true;
        instructionsManager.CheckProgress();
    }

    public void PutOnGloves()
    {
        instructionsManager.isWearingGloves = true;
        instructionsManager.CheckProgress();
    }

}
