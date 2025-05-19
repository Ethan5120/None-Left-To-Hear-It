using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorOpens : MonoBehaviour, I_Interactable
{
    [SerializeField] GM_ScriptableObject GM;
    [SerializeField] PlayerSO playerData;
    [SerializeField] int neededKey;

    [SerializeField] Animator ac_DualDoors;
    [SerializeField] string a_openAnim;
    [SerializeField] GameObject NavMeshModifier;

    [Header("UI Data")]
    [SerializeField] GameEvent turnOnPanel;
    [SerializeField] [TextArea(3,5)]string TextToOpen;
    [SerializeField] [TextArea(3,5)]string TextToClosed;


    // Start is called before the first frame update
    void Start()
    {
        if (GM.isHubDoorOpen)
        {
            OpenDoor();
        }
    }
    public void Interact()
    {
        CheckKey();
    }


    void CheckKey()
    {
        if (playerData.PlayerKeys[neededKey] == true && !GM.isHubDoorOpen)
        {
            OpenDoor();
            GM.isHubDoorOpen = true;
            turnOnPanel.Raise(this, TextToOpen);
        }
        else if(!GM.isHubDoorOpen)
        {
            //Sound of Closed
            turnOnPanel.Raise(this, TextToClosed);
        }
    }


    void OpenDoor()
    {
        ac_DualDoors.Play(a_openAnim);
        NavMeshModifier.SetActive(false);
    }
}
