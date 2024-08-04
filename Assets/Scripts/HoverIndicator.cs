using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverIndicator : MonoBehaviour
{
    public float hoverSpeed = 1.0f;
    public float hoverHeight = 1.0f;

    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float newZ = originalPosition.z + Mathf.Sin( hoverSpeed * Time.time ) * hoverHeight;
        transform.localPosition = new Vector3 (originalPosition.x, originalPosition.y, newZ);
    }
}
