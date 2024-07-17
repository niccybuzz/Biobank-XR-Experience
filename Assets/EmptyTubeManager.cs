using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTubeManager : MonoBehaviour
{

    private int lastBlendShapeIndex;
    public float animationDuration = 2f; // Duration for the blend shape to transition from 100% to 0%
    private float animationTimer = 0f;
    private bool isAnimating = false;
    private bool animationCompleted = false;
    public bool lidOn = true;

    private SkinnedMeshRenderer skinnedMeshRenderer;
    public PipetteManager pipette;
    //private bool tipInTriggerZone = false;
    public InstructionsPanelManager2 instructionsManager;

    // Start is called before the first frame update
    void Start()
    {
        //Skinned mesh rendered contains the "blend frames" used to animate object
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("No SkinnedMeshRenderer found on the GameObject.");
            return;
        }

        //Only using the final "blend shape" frame here, since it contains all the animation data we need
        lastBlendShapeIndex = skinnedMeshRenderer.sharedMesh.blendShapeCount - 1;

        if (lastBlendShapeIndex < 0)
        {
            Debug.LogError("No blend shapes found on the SkinnedMeshRenderer.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tip"))
        {
            if (pipette.isPressed &&!lidOn &&pipette.isFull)
            {
                Debug.LogWarning("Pipette should be dispensing now");
                StartDispensePlasma();
            }
        }

    }
    private void StartDispensePlasma()
    {
        animationCompleted = false;
        Debug.LogWarning("Should be dispensing");
        isAnimating = true;
        animationTimer = animationDuration;
        pipette.isFull = false;
        skinnedMeshRenderer.enabled = true;
        instructionsManager.NextPanel(1f);
    }

    // Update is called once per frame
    void Update()
    {
 
        if (animationCompleted) return;

        if (isAnimating)
        {
            AnimateDispensePlasma();
        }
    }

    //Smoothly transitions from 100% weight to 0% on the final "blend shapes" frame
    private void AnimateDispensePlasma()
    {

        /* Time.deltaTime represents the amount of time that has passed since the last frame was rendered, varying based on framerate
         * When the animation starts, animationTimer is initialized to the total duration of the animation (e.g., 2 seconds).
         * Each frame, Time.deltaTime is subtracted from animationTimer. This means that with each passing frame, animationTimer gets closer to 0.
         * By decrementing animationTimer with Time.deltaTime, the animation progresses smoothly over time, regardless of frame rate fluctuations.
         * When animationTimer reaches 0, the animation is considered complete, and isAnimating is set to false to stop further updates.
         */
        animationTimer -= Time.deltaTime;

        /*
         * Calculates the blend shape weight based on the animation timer
         * Mathf.Clamp01 ensures that the value is clamped between 0.0 and 1.0.
         * This is useful for ensuring that any small floating-point errors do not cause the value to exceed the expected range.
         */

        float weight = Mathf.Clamp01(1.0f - animationTimer / animationDuration) * 100f;
        skinnedMeshRenderer.SetBlendShapeWeight(lastBlendShapeIndex, weight);

        // Stop the animation when the timer reaches zero
        if (animationTimer <= 0f)
        {
            isAnimating = false;
            animationCompleted = true;
        }
    }
/*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tip"))
        {
            tipInTriggerZone = true;
            Debug.LogWarning("Tip entered plasma");
        }
    }*/

/*    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tip"))
        {
            tipInTriggerZone = false;
            Debug.LogWarning("Tip left plasma");
        }
    }*/

    public void RemoveLid()
    {

        lidOn = false;
        Debug.LogWarning("Lid removed");
    }

    public void PutLidOn()
    {

        lidOn = true;
        Debug.LogWarning("Lid put on ");
    }
}
