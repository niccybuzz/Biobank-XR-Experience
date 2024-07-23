using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTube : MonoBehaviour
{

    protected int lastBlendShapeIndex;
    public float animationDuration = 2f; // Duration for the blend shape to transition from 100% to 0%
    protected float animationTimer = 0f;
    protected bool isAnimating = false;
    protected bool animationCompleted = false;
    public bool lidOn = true;
    public GameObject plasma;

    protected SkinnedMeshRenderer skinnedMeshRenderer;
    public PipetteManager pipette;
 

    // Start is called before the first frame update
    void Start()
    {
        //Skinned mesh rendered contains the "blend frames" used to animate object
        skinnedMeshRenderer = plasma.GetComponent<SkinnedMeshRenderer>();
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

    // Update is called once per frame
    void Update()
    {
        if (animationCompleted) return;

        if (isAnimating)
        {
            Debug.LogWarning("is animating set to true");
            AnimateDispensePlasma();
        }
    }

    //Smoothly transitions from 100% weight to 0% on the final "blend shapes" frame
    private void AnimateDispensePlasma()
    {
        Debug.LogWarning("Animate dispense plasma called");
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
}
