using UnityEngine;

//Not sure what this does but scared to delete it 
public class HoverIndicator : MonoBehaviour
{
    public float hoverSpeed = 1.0f;
    public float hoverHeight = 1.0f;

    private Vector3 originalPosition;
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        float newZ = originalPosition.z + Mathf.Sin( hoverSpeed * Time.time ) * hoverHeight;
        transform.localPosition = new Vector3 (originalPosition.x, originalPosition.y, newZ);
    }
}
