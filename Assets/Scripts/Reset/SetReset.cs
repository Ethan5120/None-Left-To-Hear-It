using UnityEngine;

public class SetReset : MonoBehaviour
{
    [SerializeField] GM_ScriptableObject managerData;
    [SerializeField] PlayerSO playerData;
    [SerializeField] pickUp_SO[] ItemsLists;
    [SerializeField] EnemyData[] enemies;
    void Awake()
    {
        ResetPlayer();
        ResetItems();
        ResetEnemies();
        managerData.isHubDoorOpen = false;
        managerData.isEnergyOn = false;
    }


    void ResetPlayer()
    {
        PlayerController.newPlayerSpawn = new Vector3(-16.25f, 0.029f, 26);
        PlayerController.newPlayerRotation = Quaternion.Euler(0, 90, 0);
        playerData.playerAmmo = 0;
        playerData.playerPills = 0;
        playerData.playerHP = 3;
        playerData.hasGun = false;
        for (int i = 0; i < playerData.PlayerKeys.Count; i++)
        {
            playerData.PlayerKeys[i] = false;
        }
    }
    void ResetItems()
    {
        for (int j = 0; j < ItemsLists.Length; j++)
        {
            ItemsLists[j].Reset();
        }
    }
    void ResetEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].firstLoad = true;
        }
    }
}
