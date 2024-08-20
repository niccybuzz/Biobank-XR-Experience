using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsTrigger : MonoBehaviour
{
    public void OnGrab()
    {
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
