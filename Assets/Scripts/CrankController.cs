using UnityEngine;

public class CrankController : MonoBehaviour
{
    public Transform sampleBlockSlicer;  // Reference to the slicer object
    public float amplitude = 1.0f; // Maximum distance the slicer moves up and down
    public float frequency = 1.0f; // Controls the speed of the movement

    private float initialSlicerY; // The initial Y position of the slicer
    private float initialCrankRotation;

    void Start()
    {
        // Store the initial position of the slicer
        initialSlicerY = sampleBlockSlicer.localPosition.y;
        // Store the initial rotation of the crank
        initialCrankRotation = transform.localEulerAngles.z;
    }

    void Update()
    {
        // Get the current rotation of the crank
        float currentCrankRotation = transform.localEulerAngles.z;

        // Calculate the total rotation from the initial position
        float totalRotation = currentCrankRotation - initialCrankRotation;

        // Handle the wrapping of the rotation from 360 to 0
        if (totalRotation < 0)
        {
            totalRotation += 360;
        }

        // Convert the rotation to radians and apply the sine function
        float radianRotation = totalRotation * Mathf.Deg2Rad * frequency;
        float sineValue = Mathf.Sin(radianRotation);

        // Calculate the new Y position of the slicer
        float newYPosition = initialSlicerY + sineValue * amplitude;

        // Apply the new position to the slicer
        Vector3 newSlicerPosition = sampleBlockSlicer.localPosition;
        newSlicerPosition.y = newYPosition;
        sampleBlockSlicer.localPosition = newSlicerPosition;
    }
}


