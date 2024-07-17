
using UnityEditor.XR;
using UnityEngine;

public class PipetteManager : MonoBehaviour
{
    public float moveDistance = 0.1f; // Distance the plunger moves down
    public float moveSpeed = 1.0f; // Speed of the plunger movement
    public GameObject plunger;
    private Vector3 plunger_InitialPosition;
    public bool isPressed = false;
    public bool isHeld = false;
    public bool isFull = false;
    public InstructionsPanelManager2 previousStep;
    public InstructionsPanelManager2 instructionManager;
    public ParticleSystem plasmaLiquidParticules;

    private void Start()
    {
        plunger_InitialPosition = plunger.transform.localPosition;

    }
    public void OnGrab()
    {
        
        isHeld = true;
        if (previousStep.GetStepComplete())
        {
            instructionManager.NextPanel(1f);
        }
        
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
        Vector3 targetPosition = plunger_InitialPosition - new Vector3(0, moveDistance, 0);
        plunger.transform.localPosition = Vector3.Lerp(plunger.transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

    }

    void ResetPlunger()
    {
        plunger.transform.localPosition = Vector3.Lerp(plunger.transform.localPosition, plunger_InitialPosition, moveSpeed * Time.deltaTime);
    }
}
