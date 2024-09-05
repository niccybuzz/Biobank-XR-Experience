using UnityEngine;

/*
 * Child class of test tube
 * Monitors for the presence of the pipette in it's trigger zone and triggers plasma animation if button is pressed
 */
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
                StartDispensePlasma();
                PlayPlasmaSound(dispenseSound);
            }
        }

    }
    protected void StartDispensePlasma()
    {
        animationCompleted = false;
        isAnimating = true; // Start animation
        animationTimer = animationDuration;
        pipette.IsFull = false; // Ensures the pipette can't dispense 2 in a row
        plasmaSkinnedMeshRenderer.enabled = true; // enable the visuals to see the plasma

        // Complete the stage in regular mode, or add a point in challenge mode
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

}
