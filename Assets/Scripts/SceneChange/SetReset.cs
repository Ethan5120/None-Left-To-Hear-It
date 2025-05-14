using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReset : MonoBehaviour
{
    [SerializeField] GM_ScriptableObject managerData;
    [SerializeField] PlayerSO playerData;
    [SerializeField] ItemManager IManager;
    void Awake()
    {
        playerData.playerAmmo = 5;
        playerData.playerHP = 4;
        for(int i = 0; i < playerData.PlayerKeys.Count; i++)
        {
            playerData.PlayerKeys[i] = false;
        }

        IManager.ResetItems();
    }
}
