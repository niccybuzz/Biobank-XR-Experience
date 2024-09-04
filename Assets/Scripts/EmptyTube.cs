using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTubeManager : TestTube
{
    public InstructionsPanelManager dispensePlasmaInstructions;
    public bool challengeModeEnabled;
    public ChallengeMode challengeMode;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tip"))
        {
            if (pipette.IsPressed &&!lidOn &&pipette.IsFull)
            {
                Debug.LogWarning("Pipette should be dispensing now");
                StartDispensePlasma();
                PlayPlasmaSound(dispenseSound);
            }
        }

    }
    protected virtual void StartDispensePlasma()
    {
        animationCompleted = false;
        isAnimating = true;
        animationTimer = animationDuration;
        pipette.IsFull = false;
        skinnedMeshRenderer.enabled = true;
        dispenseSound.Play();
        if (!challengeModeEnabled)
        {
            dispensePlasmaInstructions.NextPanel(1f);
            dispensePlasmaInstructions.ShowFacePanels(1f);
            dispensePlasmaInstructions.CompleteStage(1f);
        }
        else if (challengeMode != null)
        {
            challengeMode.AddPoint();
        }
    }

    public void RemoveLid()
    {
        lidOn = false;
    }

    public void PutLidOn()
    {

        lidOn = true;
    }
}
