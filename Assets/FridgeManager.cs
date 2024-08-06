using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door;
    void Start()
    {
        CloseDoor();
    }

    private void CloseDoor()
    {

        Vector3 doorPosition = door.transform.rotation.eulerAngles;
        
        doorPosition.z += 90;
        door.transform.rotation = Quaternion.Euler(doorPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
