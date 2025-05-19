using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GM_ScriptableObject GM;
    [SerializeField] ItemManager items;
    
    [Header("PlayerSettings")]
    [SerializeField] PlayerSO playerData;
    [SerializeField] GameObject player;

    void SpawnPlayer()
    {
        if (playerData != null)
        {
            player.transform.SetPositionAndRotation(playerData.spawnPosition, playerData.spawnRotation);
        }
        else
        {
            Debug.LogError("NecesesaryDataMissing");
        }
    }

    void Awake()
    {
        GM.gameTime = 1;
        if (items)
        {
            items.InitializeItems();   
        }
        
    }
}
