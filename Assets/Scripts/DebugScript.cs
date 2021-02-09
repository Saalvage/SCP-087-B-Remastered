using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DebugScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject debugPanel;
    public GameObject graphy;
    public TMP_Text coords;
    public TMP_Text floor;

    [Header("Transforms")]
    public Transform playerPos;

    [Header("Bools")]
    public bool nightVision = false;
    public bool graphyEnabled = false;
    public bool debugEnabled = false;

    private void Start()
    {
        playerPos = Globals.playerObject.transform;
    }

    public void FixedUpdate()
    {
#if UNITY_EDITOR
        if(Input.GetKeyUp(KeyCode.J))
        {
            debugEnabled = !debugEnabled;
            debugPanel.SetActive(!debugPanel.activeSelf);
            coords.gameObject.SetActive(!coords.gameObject.activeSelf);
            floor.gameObject.SetActive(!floor.gameObject.activeSelf);
        }

        if(debugEnabled == true)
        {
            if(Input.GetKeyDown(KeyCode.N))
            {
                nightVision = !nightVision;
                RenderSettings.fog = !RenderSettings.fog;

                if (nightVision == true) RenderSettings.ambientLight = new Color(2,2,2,1);
                else RenderSettings.ambientLight = Color.black;
            }
            else if(Input.GetKeyDown(KeyCode.P))
            {
                graphyEnabled = !graphyEnabled;
                graphy.SetActive(!graphy.activeSelf);
            }

            coords.text = string.Format("X: {0} Y: {1} Z: {2}", Math.Round(playerPos.position.x, 2, MidpointRounding.AwayFromZero),
                Math.Round(playerPos.position.y, 2, MidpointRounding.AwayFromZero), Math.Round(playerPos.position.z, 2, MidpointRounding.AwayFromZero));

            floor.text = string.Format("Floor: {0}\nEvent: {1}", Globals.playerFloor, GetFloorAction());
        }
#endif
    }

    private string GetFloorAction()
    {
        Floor floor = Floor.floors[Globals.playerFloor];

        switch(floor.ID)
        {
            case 0:
                return "ACT_NONE";
            case 1:
                return "ACT_STEPS";
            case 2:
                return "ACT_LIGHTS";
            case 3:
                return "ACT_FLASH";
            case 4:
                return "ACT_WALK";
            case 5:
                return "ACT_RUN";
            case 6:
                return "ACT_KALLE";
            case 7:
                return "ACT_BREATH";
            case 8:
                return "ACT_PROCEED";
            case 9:
                return "ACT_TRAP";
            case 11:
                return "ACT_173";
            case 12:
                return "ACT_CELL";
            case 13:
                return "ACT_LOCK";
            case 15:
                return "ACT_RADIO2";
            case 16:
                return "ACT_RADIO3";
            case 17:
                return "ACT_RADIO4";
            case 18:
                return "ACT_TRICK1";
            case 19:
                return "ACT_TRICK2";
            case 20:
                return "ACT_ROAR";
            case 21:
                return "ACT_DARKNESS";
            case 22:
                return "ACT_BEHIND";
            default:
                return "UNKNOWN";
        }
    }
}
