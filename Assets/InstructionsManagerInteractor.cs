using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsManagerInteractor : MonoBehaviour
{
    public InstructionsPanelManager instructionsManager;

    public void PressCentrifugeButton()
    {
        instructionsManager.PressCentrifugeButton();
    }

    public void PutOnGloves()
    {
        instructionsManager.PutOnGloves(true);
    }



}
