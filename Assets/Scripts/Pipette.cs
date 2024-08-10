
using UnityEditor.XR;
using UnityEngine;

public class PipetteManager : MonoBehaviour
{
    //status bools
    private bool isPressed = false;
    private bool isHeld = false;
    private bool isFull = false;

    //variables for speed and distance of plunger moving speed
    public GameObject plunger;
    public float moveDistance = 0.1f; // Distance the plunger moves down
    public float moveSpeed = 1.0f; // Speed of the plunger movement
    
    private Vector3 plunger_InitialPosition;

    public InstructionsPanelManager2 previousStep;
    public InstructionsPanelManager2 instructionManager;

    private OVRControllerHelper leftControllerHelper;
    private OVRControllerHelper rightControllerHelper;

    public bool IsPressed { get => isPressed; set => isPressed = value; }
    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public bool IsFull { get => isFull; set => isFull = value; }

    private void Start()
    {
        plunger_InitialPosition = plunger.transform.localPosition;

    }

    void Update()
    {
        // Check for VR controller trigger press
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && IsHeld)
        {
            IsPressed = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) && IsHeld)
        {
            IsPressed = false;
        }

        // Move the plunger down if the trigger is pressed
        if (IsPressed && IsHeld)
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
        Vector3 targetPosition = plunger_InitialPosition - new Vector3(0, moveDistance, 0);
        plunger.transform.localPosition = Vector3.Lerp(plunger.transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

    }

    void ResetPlunger()
    {
        plunger.transform.localPosition = Vector3.Lerp(plunger.transform.localPosition, plunger_InitialPosition, moveSpeed * Time.deltaTime);
    }
}
