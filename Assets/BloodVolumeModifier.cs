using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodVolumeModifier : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private int lastBlendShapeIndex;
    public float animationDuration = 2f; // Duration for the blend shape to transition from 100% to 0%
    private float animationTimer = 0f;
    private bool isAnimating = false;
    private bool animationCompleted = false;


    // Getting the components required for triggering the animation at start
    void Start()
    {
        //Skinned mesh rendered contains the "blend frames" used to animate object
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
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

    /*
     * Checks every frame to see if the animation is currently playing
     */
    void Update()
    {
        if (animationCompleted) return;

        if (isAnimating)
        {
            AnimateBlendShape();
        }
    }

    //Public method that can be called to start the blood animation
    public void StartAnimation()
    {
        isAnimating = true;
        animationTimer = animationDuration; // Initialize the animation timer
    }

    //Smoothly transitions from 100% weight to 0% on the final "blend shapes" frame
    private void AnimateBlendShape()
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
        float weight = Mathf.Clamp01(animationTimer / animationDuration) * 100f;
        skinnedMeshRenderer.SetBlendShapeWeight(lastBlendShapeIndex, weight);

        // Stop the animation when the timer reaches zero
        if (animationTimer <= 0f)
        {
            isAnimating = false;
            animationCompleted = true;
        }
    }
}
