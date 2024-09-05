using UnityEngine;

// Experimental script to make an object face the player at the start of the scene
public class FacePlayerOnStartVR : MonoBehaviour
{
    // Reference to the player's CenterEyeAnchor (the main camera for the VR headset)
    private Transform centerEyeAnchor;

    void Start()
    {
        centerEyeAnchor = GameObject.Find("CenterEyeAnchor").transform;
    }

    private void Update()
    {
        if (centerEyeAnchor != null)
        {
            // Calculate the direction from this GameObject to the player's CenterEyeAnchor
            Vector3 directionToPlayer = centerEyeAnchor.position - transform.position;
            
            // Remove the vertical component so that the GameObject only rotates on the y-axis
            directionToPlayer.y = 0;
            
            // Calculate the rotation needed to face the player's CenterEyeAnchor
            Quaternion rotationToPlayer = Quaternion.LookRotation(directionToPlayer);
            
            // Apply the rotation
            transform.rotation = rotationToPlayer;
        }
        else
        {
            Debug.LogError("CenterEyeAnchor is not assigned.");
        }
    }
}
