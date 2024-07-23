using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTubeManager : TestTube
{
    public InstructionsPanelManager2 dispensePlasmaInstructions;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tip"))
        {
            if (pipette.IsPressed &&!lidOn &&pipette.IsFull)
            {
                Debug.LogWarning("Pipette should be dispensing now");
                StartDispensePlasma();
            }
        }

    }
    private void StartDispensePlasma()
    {
        animationCompleted = false;
        isAnimating = true;
        animationTimer = animationDuration;
        pipette.IsFull = false;
        skinnedMeshRenderer.enabled = true;
        dispensePlasmaInstructions.NextPanel(1f);
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
