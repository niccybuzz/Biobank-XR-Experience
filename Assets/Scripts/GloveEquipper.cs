using UnityEngine;

/**
 * Changes the user's hands to blue when glove box is pressed
 */
public class GloveEquipper : MonoBehaviour
{
    private GameObject[] _defaultHandVisuals;

    [SerializeField]
    Material gloveMaterial;

    //Finds the detault hand skins and replaces them with a blue latex glove material
    public void SwitchHandVisuals()
    {
        _defaultHandVisuals = GameObject.FindGameObjectsWithTag("HandVisual");

        if (_defaultHandVisuals != null )
        {
            //Turn off the default skins
            foreach (var item in _defaultHandVisuals)
            {
                item.GetComponent<SkinnedMeshRenderer>().material = gloveMaterial;
            }
        } 
        else
        {
            Debug.Log("Hand Visuals not found");
        }
    }
}

