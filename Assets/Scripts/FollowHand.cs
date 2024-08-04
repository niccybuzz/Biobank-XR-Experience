using UnityEngine;

public class FollowHandOculus : MonoBehaviour
{

    public OVRHand hand;
    public Transform handTransform;
    public float positionLerpSpeed = 5f;
    public float rotationLerpSpeed = 5f;

    void Start()
    {

    }

    void Update()
    {
        if (hand != null && hand.IsTracked)
        {
            handTransform.position = hand.transform.position;
   
            transform.position = Vector3.Lerp(transform.position, handTransform.position, Time.deltaTime * positionLerpSpeed);

        }
    }
}

