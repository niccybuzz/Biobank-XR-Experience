using UnityEngine;

/*
 * Parent class for empty tube and blood tube GameObjects
 * contains the method for animating the plasma, shared by the tubes
 * The plasma in each tube's blend shape indexes are reversed, 
 * meaning the same method can be used for dispensing and drawing plasma
 */
public class TestTube : MonoBehaviour
{
    // The values marked as protected dont work properly with inheriting classes when set to private

    // Variables for controlling the animations
    protected int lastBlendShapeIndex;
    public float animationDuration = 2f; // Duration for the blend shape to transition from 100% to 0%
    protected float animationTimer = 0f; // This is increased to animationDuration once animation activated
    protected bool isAnimating = false;
    protected bool animationCompleted = false;

    public GameObject plasma;

    public bool lidOn = true;
    public bool LidOn { get => lidOn; set => lidOn = value; }

    //Water sound effects
    protected AudioSource dispenseSound;
    protected AudioSource drawUpSound;

    protected SkinnedMeshRenderer plasmaSkinnedMeshRenderer;
    public PipetteManager pipette;

    // Upon initialization, finding the objects and components necessary to do the plasma animation
    void Start()
    {
        plasmaSkinnedMeshRenderer = plasma.GetComponent<SkinnedMeshRenderer>();
        if (plasmaSkinnedMeshRenderer == null)
        {
            return;
        }

        lastBlendShapeIndex = plasmaSkinnedMeshRenderer.sharedMesh.blendShapeCount - 1;
        dispenseSound = GameObject.Find("DispensePlasmaSound").GetComponent<AudioSource>();
        drawUpSound = GameObject.Find("DrawPlasmaSound").GetComponent<AudioSource>();
    }

    void Update()
    {
        if (animationCompleted) return;

        if (isAnimating)
        {
            AnimateDispensePlasma();
        }
    }

       /*
        * Smoothly transitions from 100% weight to 0% on the final "blend shapes" frame
        * Time.deltaTime represents the amount of time that has passed since the last frame was rendered, varying based on framerate
        * When the animation starts, animationTimer is initialized to the specified duration of the animation (2 seconds).
        * Each frame, Time.deltaTime is subtracted from animationTimer. This means that with each passing frame, animationTimer gets closer to 0.
        * By decrementing animationTimer with Time.deltaTime, the animation progresses smoothly over time, regardless of frame rate fluctuations.
        * When animationTimer reaches 0, the animation is considered complete, and isAnimating is set to false to stop further updates.
        *
        * Animation is achieved by moving the "blend shape index" slides from top to bottom
        */
    private void AnimateDispensePlasma()
    {

        animationTimer -= Time.deltaTime;

        // Mathf.Clamp01 ensures that the value is clamped between 0.0 and 1.0.
        // This is useful for ensuring that any small floating-point errors do not cause the value to exceed the expected range.
        float weight = Mathf.Clamp01(1.0f - animationTimer / animationDuration) * 100f;

        plasmaSkinnedMeshRenderer.SetBlendShapeWeight(lastBlendShapeIndex, weight);

        // Stop the animation when the timer reaches zero
        if (animationTimer <= 0f)
        {
            isAnimating = false;
            animationCompleted = true;
        }
    }

    public void PutLidOn()
    {
        lidOn = true;
    }
    public virtual void RemoveLid()
    {
        lidOn = false;
    }

    public void PlayPlasmaSound(AudioSource sound)
    {
        sound.Play();
    }

}
