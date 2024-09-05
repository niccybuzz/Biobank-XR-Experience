using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class CentrifugeController : MonoBehaviour
{
    //Variables for spinning the centrifuge
    [SerializeField]
    float spinDuration = 10.0f; // Total duration of the spinning motion
    [SerializeField]
    float maxSpinSpeed = 3600.0f; // Maximum speed of rotation in degrees per second
    [SerializeField]
    float accelerationTime = 1.0f; // Time to accelerate to max speed in seconds


    //Variables for getting various parts of the centrifuge including lid, sockets, etc
    [SerializeField]
    Transform centralPiece;
    [SerializeField]
    GameObject lid;
    [SerializeField]
    HandGrabInteractable centrifugeHandle;
    [SerializeField]
    PokeInteractable centrifugeButton;

    // Variables for interacting with blood tubes in the sockets
    private List<GameObject> _bloodTubes;
    private CentrifugeSocketController[] _bloodTubeSockets;
    private int _numberOfTubesInSockets = 0;

    //Instructions panel interactors
    public InstructionsPanelManager openCentrifugePanel;
    public InstructionsPanelManager tubesInCentrifugePanel;
    public InstructionsPanelManager closeCentrifugePanel;
    public InstructionsPanelManager pressCentrifugeButtonPanel;

    // Sound clip that plays whilst spinning
    [SerializeField]
    AudioSource centrifugeAudio;

    // Variables to control functionality of centrifuge in different states
    private bool _isSpinning = false;

    //determined whether instructions panel related code should be run
    [SerializeField]
    bool challengeModeEnabled = false;

    public CentrifugeSocketController[] BloodTubeSockets { get => _bloodTubeSockets; set => _bloodTubeSockets = value; }

    //Upon scene initialiation, collects a list of all the snap interactable components in the children, i.e the tube sockets
    public void Start()
    {
        BloodTubeSockets = GetComponentsInChildren<CentrifugeSocketController>(true);
    }

    /*
     * When the centrifuge button is pressed and it starts spinning, it triggers the animation through the coroutine "Spin for duration".
     * It then replaces the whole blood GameObjects with the split blood GameObjects
     * In regular mode, triggers the next instructions panel as long as there is at least 2 tubes in the sockets
     * */
    public void SpinCentrifuge()
    {
        if (!_isSpinning)
        {
            StartCoroutine(SpinForDuration());
            SplitBloodInAllInteractingTubes(BloodTubeSockets);
            centrifugeAudio.Play();
            if (_numberOfTubesInSockets >= 2 && !challengeModeEnabled)
            {
                pressCentrifugeButtonPanel.NextPanel(spinDuration);
                pressCentrifugeButtonPanel.CompleteStage(spinDuration);
            }

        }
    }

    // Called by a trigger collider on the lid
    // Button cannot be pressed whilst centrifuge is open
    public void OpenCentrifuge()
    {
        centrifugeButton.enabled = false;
        if (!challengeModeEnabled)
        {
            openCentrifugePanel.NextPanel(1);
        }
    }

    public void CloseCentrifuge()
    {
        centrifugeButton.enabled = true;
        if (_numberOfTubesInSockets >= 2 && !challengeModeEnabled)
        {
            closeCentrifugePanel.NextPanel(1);
        }
    }

    // Calls SplitBlood() for each tube currently attached to a socket
    private void SplitBloodInAllInteractingTubes(CentrifugeSocketController[] bloodTubeSockets)
    {
        _bloodTubes = GetAllBloodTubesInCentrifuge(bloodTubeSockets);

        foreach (GameObject tube in _bloodTubes)
        {
            tube.GetComponent<TestTubeManager>().SplitBlood();
        }
    }

    /*
     * Used to get a list of all GameObjects (tubes) which are currently attached to the centrifuge sockets
     * Works by finding the Snap Interactor component in each tube then retrieving it's parent GameObject then pushing to a list
     */
    public List<GameObject> GetAllBloodTubesInCentrifuge(CentrifugeSocketController[] bloodTubeSockets)
    {
        List<GameObject> bloodTubes = new();

        foreach (CentrifugeSocketController socket in bloodTubeSockets)
        {
            GameObject tube = socket.GetObjectOnPlatform();
            if (tube != null)
            {
                bloodTubes.Add(tube);
            }
       
        }
        return bloodTubes;
    }

    // Called each time a tube is placed in a socket
    public void IncrementTubesInSockets()
    {
        _numberOfTubesInSockets++;
        if (_numberOfTubesInSockets >= 2 && !challengeModeEnabled)
        {
            tubesInCentrifugePanel.NextPanel(1);
        }
    }

    // Called whenever a tube is removed from a socket
    public void DecrementTubesInSockets()
    {
        _numberOfTubesInSockets--;
    }

    /*
     * A coroutine which animates the sample holder in the centrifuge to spin for a specified amount of time
     * Starts off slowly, and accelerates to maximum speed before deccelerating again
     */
    private IEnumerator SpinForDuration()
    {
        _isSpinning = true;
        centrifugeHandle.enabled = false;

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

        //reenable the centrifuge button and end spinning
        centrifugeHandle.enabled = true;
        _isSpinning = false;
    }
}

