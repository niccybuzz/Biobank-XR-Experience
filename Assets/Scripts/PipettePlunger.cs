
using UnityEngine;

public class PipettePlunger : MonoBehaviour
{
    public float moveDistance = 0.1f; // Distance the plunger moves down
    public float moveSpeed = 1.0f; // Speed of the plunger movement
    public GameObject plunger;
    private Vector3 initialPosition;
    private bool isPressed = false;
    private bool isHeld = false;
    public BloodVolumeModifier blood;
    public TipCollisionDetector tip;

    private void Start()
    {
        initialPosition = plunger.transform.localPosition;

    }
    public void OnGrab()
    {
        
        isHeld = true;
    }
    public void OnDrop()
    {

        isHeld = false;
    }

    void Update()
    {
        // Check for VR controller trigger press
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && isHeld)
        {
            isPressed = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && isHeld)
        {
            isPressed = false;
        }

        // Move the plunger down if the trigger is pressed
        if (isPressed && isHeld)
        {
            MovePlungerDown();
        }
        else
        {
            ResetPlunger();
        }
    }

    void MovePlungerDown()
    {
        Vector3 targetPosition = initialPosition - new Vector3(0, moveDistance, 0);
        plunger.transform.localPosition = Vector3.Lerp(plunger.transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

        if (tip.isInContactWithBlood)
        {
            Debug.Log("Animation should begin now");
            blood.StartAnimation();
        }
        else
        {
            Debug.Log("Error detecting collision");
        }
    }

    void ResetPlunger()
    {
        plunger.transform.localPosition = Vector3.Lerp(plunger.transform.localPosition, initialPosition, moveSpeed * Time.deltaTime);
    }
}
