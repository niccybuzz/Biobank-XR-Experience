using UnityEngine;

/*
 * Moves the invisible floor of the water bath up and down, simulating floating
 */
public class FloatingObject : MonoBehaviour
{
    public float amplitude = 0.5f; // The height of the floating effect
    public float frequency = 1f; // The speed of the floating effect
    public float rotationAmplitude = 5f; // The rotation effect's amplitude
    public float rotationFrequency = 0.5f; // The speed of the rotation effect

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // Store the initial position 
        initialPosition = transform.position;
    }

    void Update()
    {
        // Update the floating position
        Vector3 newPosition = initialPosition;
        newPosition.y += Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = newPosition;
    }
}
