using System.Collections.Generic;
using UnityEngine;

public class HubManager : MonoBehaviour
{
    [SerializeField] PlayerSO playerData;
    [SerializeField] List<pickUp_SO> pickUps = new List<pickUp_SO>();
    [SerializeField] List<EnemyData> enemies = new List<EnemyData>();

    [SerializeField] GameObject player;
    void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        if(playerData != null)
        {
            if(playerData.isFirstLoad)
            {
                foreach(EnemyData enemy in enemies)
                {
                    enemy.firstLoad = true;
                }

                foreach(pickUp_SO pickUp in pickUps)
                {
                    pickUp.hasBeenCollected = false;
                }

                playerData.playerHP = 3;
                playerData.playerAmmo = 3;
                playerData.playerPills = 1;

                for(int i = 0; i < playerData.PlayerKeys.Count; i++)
                {
                    if(i == 0)
                    {
                        playerData.PlayerKeys[i] = true;
                    }
                    else
                    {
                        playerData.PlayerKeys[i] = false;
                    }
                }
                playerData.isFirstLoad = false;
            }
            else
            {
                player.transform.SetPositionAndRotation(playerData.spawnPosition, playerData.spawnRotation);
            }
            playerData.isInHub = true;
        }
    }
}
