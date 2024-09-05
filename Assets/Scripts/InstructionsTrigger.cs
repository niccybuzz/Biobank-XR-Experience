using UnityEngine;

/*
 * This is used to communicate between objects in the scene view (cabinet) with objects in prefab view (instructions panels)
 * Required because the usual click and drag operations are not available between the 2 different views 
 */

public class InstructionsTrigger : MonoBehaviour
{
    public void OnGrab()
    {
        //FridgeRadar is contained within the tissue analysis workstation and contains an IPM instance
        GameObject fridgeRadar = GameObject.Find("FridgeRadar");
        if (fridgeRadar != null) {
            InstructionsPanelManager panelManager = fridgeRadar.GetComponent<InstructionsPanelManager>();
            if (panelManager != null)
            {
                panelManager.NextPanel(1f);
            }

    }
        
    }
}
