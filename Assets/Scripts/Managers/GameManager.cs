using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GM_ScriptableObject GM;
    [SerializeField] ItemManager items;
    
    [Header("PlayerSettings")]
    [SerializeField] PlayerSO playerData;
    [SerializeField] GameObject player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        if (playerData != null)
        {
            player.transform.SetPositionAndRotation(playerData.spawnPosition, playerData.spawnRotation);
        }
        else
        {
            Debug.LogError("NecesesaryDataMissing");
        }
    }

    void Start()
    { 
        GM.gameTime = 1;
        items.InitializeItems(); 
    }
}
