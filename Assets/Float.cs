using UnityEngine;

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
        // Store the initial position and rotation
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // Update the floating position
        Vector3 newPosition = initialPosition;
        newPosition.y += Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = newPosition;

        // Update the floating rotation
        Quaternion newRotation = initialRotation;
        float rotationX = Mathf.Sin(Time.time * rotationFrequency) * rotationAmplitude;
        float rotationZ = Mathf.Cos(Time.time * rotationFrequency) * rotationAmplitude;
        newRotation *= Quaternion.Euler(rotationX, 0f, rotationZ);
        transform.rotation = newRotation;
    }
}
