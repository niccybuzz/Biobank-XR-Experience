using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class WallArtReplacer : MonoBehaviour
{
    public GameObject prefabToDisplay;
    private MRUKAnchor keyWall;
    private MRUKRoom room;
    // Start is called before the first frame update
    void Start()
    {
        room = MRUK.Instance.GetCurrentRoom();
        Vector2 wallScale;
        keyWall = room.GetKeyWall(out wallScale, 0.1f);
    }

    public void SpawnWallArt()
    {
        Transform keyWallTransform = keyWall.transform;
        Instantiate(prefabToDisplay, keyWallTransform.position, keyWallTransform.rotation);
    }
}
