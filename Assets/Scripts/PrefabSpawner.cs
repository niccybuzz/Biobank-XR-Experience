using Meta.XR.MRUtilityKit;
using System.Collections.Generic;
using UnityEngine;

/**
 * Used to spawn a specified prefab on the largest table in the user's room
 */
public class WorkstationSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject prefabToCreate;

    [SerializeField]
    RoomValidator validator;

    [SerializeField]
    Camera mainCamera;

    private MRUKRoom room;

    // Find the largest table from the room, then spawn a prefab on it
    public void SpawnWorkstation()
    {
        room = MRUK.Instance.GetCurrentRoom();
        List<MRUKAnchor> tables = validator.GetAllTables(room);
        MRUKAnchor largestTable = validator.GetLargestAnchorFromList(tables);
        SpawnObjectOnAnchor(largestTable, prefabToCreate);
    }

    // Spawns a prefab in the center of a specified anchor
    private void SpawnObjectOnAnchor(MRUKAnchor anchor, GameObject prefab)
    {
        Vector3 anchorCenter = anchor.transform.position;
        Vector3 spawnPosition = new Vector3(anchorCenter.x, anchorCenter.y, anchorCenter.z);

        // Get table's rotation
        Quaternion anchorRotation = anchor.transform.rotation;
        Vector3 anchorRotationEuler = anchorRotation.eulerAngles;

        // Set the rotation to align with the table's rotation
        Quaternion spawnRotation = Quaternion.Euler(anchorRotationEuler.x + 90, anchorRotationEuler.y, anchorRotationEuler.z);

        Instantiate(prefab, spawnPosition, spawnRotation);
    }

}
