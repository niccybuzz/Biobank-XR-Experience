using UnityEngine;

/*
 * Manages the state and animations of the pipette
 */
public class PipetteManager : MonoBehaviour
{
    //status bools
    private bool isPressed = false;
    private bool isHeld = false;
    private bool isFull = false;

    [SerializeField]
    private bool challengeModeEnabled;

    //variables for speed and distance of plunger moving speed
    [SerializeField]
    private GameObject plunger;
    [SerializeField]
    private float moveDistance = 0.1f; // Distance the plunger moves down
    [SerializeField]
    private float moveSpeed = 1.0f; // Speed of the plunger movement
    
    private Vector3 plunger_InitialPosition;

    [SerializeField]
    private InstructionsPanelManager previousStep;
    [SerializeField]
    private InstructionsPanelManager instructionManager;

    public bool IsPressed { get => isPressed; set => isPressed = value; }
    public bool IsHeld { get => isHeld; set => isHeld = value; }
    public bool IsFull { get => isFull; set => isFull = value; }

    private void Start()
    {
        plunger_InitialPosition = plunger.transform.localPosition;
    }

    public void OnGrab()
    {
        isHeld = true;
        if (!challengeModeEnabled)
        {
            if (previousStep.StepComplete)
            {
                instructionManager.NextPanel(1f);
            }
        }
    }

    void Update()
    {
        // Check for VR controller trigger press
        if ((OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three)) && IsHeld)
        {
            IsPressed = true;
        }
        else if (OVRInput.GetUp(OVRInput.Button.One) || OVRInput.GetUp(OVRInput.Button.Three))
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
