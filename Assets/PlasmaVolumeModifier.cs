using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaVolumeModifier : MonoBehaviour
{
    public InstructionsPanelManager instructionsManager;

    public GameObject pivotPoint;
    private Vector3 targetScale = new Vector3(1f, 1f, 0.003f);
    private Vector3 initialScale;

    private bool tipInTriggerZone;
    public PipetteManager pipette;

    public float animationDuration = 2f;
    public float animationTimer = 0f;
    private bool isAnimating = false;
    private bool animationCompleted = false;
    // Start is called before the first frame update
    void Start()
    {
        if (pivotPoint == null)
        {
            Debug.LogError("Pivot point object not assigned.");
            return;
        }
        initialScale = pivotPoint.transform.localScale;
    }

    private void Update()
    {
        if (isAnimating && !animationCompleted)
        {
            SuckPlasmaAnimation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tip"))
        {
            tipInTriggerZone = true;
            Debug.LogWarning("Tip In Trigger Zone");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (tipInTriggerZone && pipette.isPressed && !isAnimating &&!pipette.isFull)
        {
            StartAnimation();
            Debug.LogWarning("Animation to trigger");
            instructionsManager.DrawPlasma();
        }
    }

    public void StartAnimation()
    {
        isAnimating = true;
         animationTimer = 0f;
    }

    private void SuckPlasmaAnimation()
    {
        animationTimer += Time.deltaTime;
        float progress = Mathf.Clamp01(animationTimer / animationDuration);

        float logProgress = Mathf.Log10(1 + 9 * progress) / Mathf.Log10(10);

        Vector3 newScale = Vector3.Lerp(initialScale, targetScale, logProgress);

        pivotPoint.transform.localScale = new Vector3(initialScale.x, initialScale.y, newScale.z);

        if (progress >= 1)
        {
            animationCompleted = true;
        }
        pipette.isFull = true;
    }
}
