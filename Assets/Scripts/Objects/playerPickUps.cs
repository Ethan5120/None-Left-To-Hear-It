using UnityEngine;
using System.Collections.Generic;

public class playerPickUps : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerSO playerData;
    [SerializeField] ItemManager itemManager;
    public enum objectType 
    {
        Key,
        Pills,
        Ammo
    }

    public objectType objectSelected = objectType.Key;
    [SerializeField]int keyIndex;
    [SerializeField]int pillsAmmount;
    [SerializeField]int ammoAmmount;

    [SerializeField] bool isAmbushTrigger;
    [SerializeField] List<EnemyData> enemies = new List<EnemyData>();

    [Header("UI Data")]
    [SerializeField] GameEvent turnOnPanel;


    void  Awake()
    {
        itemManager = FindAnyObjectByType<ItemManager>();
    }


    public void Interact()
    {
        for (int i = 0; i < itemManager.items.Count; i++)
        {
            if (itemManager.items[i] == gameObject)
            {
                itemManager.itemsStates.itemActiveState[i] = true;
            }
        }

        Debug.Log("Interact");
        switch (objectSelected)
        {
            case objectType.Key:
            {
                playerData.PlayerKeys[keyIndex] = true;
                gameObject.SetActive(false);
                turnOnPanel.Raise(this, $"Key Number {keyIndex + 1}");
                break;
            }

            case objectType.Pills:
            {
                playerData.playerPills += pillsAmmount;
                gameObject.SetActive(false);
                turnOnPanel.Raise(this, $"{pillsAmmount}x Pill(s)");
                break;
            }

            case objectType.Ammo:
            {
                playerData.playerAmmo += ammoAmmount;
                gameObject.SetActive(false);
                turnOnPanel.Raise(this, $"{ammoAmmount}x Bullet(s)");
                break;
            }


        }
        if(isAmbushTrigger)
        {
            foreach(EnemyData enemy in enemies)
            {
                enemy.startAmbush = true;
            }
        }
    }

}
