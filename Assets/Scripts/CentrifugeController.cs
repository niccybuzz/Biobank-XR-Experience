
using Oculus.Interaction;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CentrifugeController : MonoBehaviour
{

    public float spinDuration = 5.0f; // Total duration of the spinning motion
    public float maxSpinSpeed = 3600.0f; // Maximum speed of rotation in degrees per second
    public float accelerationTime = 1.0f; // Time to accelerate to max speed in seconds

    public Transform centralPiece;
    public GameObject lid;
    private List<GameObject> bloodTubes;
    public SnapInteractable[] bloodTubeSockets;
    public PokeInteractable centrifugeButton;

    public InstructionsPanelManager instructionsPanelManager;

    public int numberOfTubesInSockets = 0;

    public AudioSource UIAudio;
    public AudioSource centrifugeAudio;

    public bool centrifugeIsOpen = false;
    private bool isSpinning = false;

    public void Start()
    {
        bloodTubeSockets = GetComponentsInChildren<SnapInteractable>(true);
    }
    //Checks every frame to see if the centrifuge is open or not. If open, the border is turned green
    public void Update()
    {
        Quaternion lidRotation = lid.transform.rotation;
        Vector3 eulerRotation = lidRotation.eulerAngles;

        if (eulerRotation.x < 275f && eulerRotation.x > 100f)
        {
            centrifugeIsOpen = false;
        }
        else
        {
            centrifugeIsOpen = true;
        }


        //disabling the spin button if the lid is open and changing the instructions border colour
        if (centrifugeIsOpen)
        {
            centrifugeButton.enabled = false;
        }
        else
        {
            centrifugeButton.enabled = true;
        }

        

        if (numberOfTubesInSockets >= 2)
        {
            instructionsPanelManager.twoTubesInCentrifuge = true;
        }
    }
    public void SpinCentrifuge()
    {
        if (!isSpinning)
        {
            StartCoroutine(SpinForDuration());
            SplitBlood(bloodTubeSockets);
            centrifugeAudio.Play();
            instructionsPanelManager.PressCentrifugeButton();
        }
    }

    private void SplitBlood(SnapInteractable[] bloodTubeSockets)
    {
        bloodTubes = GetAllBloodTubesInCentrifuge(bloodTubeSockets);

        foreach (GameObject tube in bloodTubes)
        {
            tube.GetComponent<TestTubeManager>().SplitBlood();
        }
    }

    //Iterates through each blood tube inside the centrifuge when it is spun, and changes the whole blood for split blood
    public List<GameObject> GetAllBloodTubesInCentrifuge(SnapInteractable[] bloodTubeSockets)
    {
        List<GameObject> bloodTubes = new();
        
        //iterating through each of the 6 Snap Interactables, one in each centrifuge socket
        foreach (SnapInteractable socket in bloodTubeSockets)
        {
            //We need to get the parent of each snap interactor
            foreach (var bloodTubeInteractor in socket.SelectingInteractors)
            {
                Transform bloodTubeParent = bloodTubeInteractor.gameObject.transform.parent;
                if (bloodTubeParent != null)
                {
                    bloodTubes.Add(bloodTubeParent.gameObject);
                }
            }

        }
        return bloodTubes;
    }

    private IEnumerator SpinForDuration()
    {
        isSpinning = true;

        float elapsedTime = 0f;
        float startSpeed = 0f;
        float targetSpeed = maxSpinSpeed;

        float decelerationTime = spinDuration - accelerationTime; // Time to decelerate from max speed to stop

        // Accelerate to max speed
        while (elapsedTime < accelerationTime)
        {
            float t = elapsedTime / accelerationTime;
            float currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, t);

            float rotationAmount = Time.deltaTime * currentSpeed;
            centralPiece.Rotate(Vector3.forward, rotationAmount);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Maintain max speed
        while (elapsedTime < decelerationTime)
        {
            float rotationAmount = Time.deltaTime * targetSpeed;
            centralPiece.Rotate(Vector3.forward, rotationAmount);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Decelerate to stop
        while (elapsedTime < spinDuration)
        {
            float t = (elapsedTime - decelerationTime) / (spinDuration - decelerationTime);
            float currentSpeed = Mathf.Lerp(targetSpeed, 0f, t);

            float rotationAmount = Time.deltaTime * currentSpeed;
            centralPiece.Rotate(Vector3.forward, rotationAmount);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isSpinning = false;
    }
}

