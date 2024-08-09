using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsTrigger : MonoBehaviour
{
    public void OnGrab()
    {
        GameObject fridgeRadar = GameObject.Find("FridgeRadar");
        if (fridgeRadar != null) {
            InstructionsPanelManager2 panelManager = fridgeRadar.GetComponent<InstructionsPanelManager2>();
            panelManager.NextPanel(1f);

    }
        
    }
}
