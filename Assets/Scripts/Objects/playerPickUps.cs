using UnityEngine;
using System.Collections.Generic;

public class playerPickUps : MonoBehaviour, I_Interactable
{
    [SerializeField] PlayerSO playerData;
    [SerializeField] PlayerController player;
    [SerializeField] ItemManager itemManager;
    public enum objectType
    {
        Key,
        Pills,
        Ammo,
        Weapon
    }

    public objectType objectSelected = objectType.Key;
    [SerializeField]int keyIndex;
    [SerializeField]int pillsAmmount;
    [SerializeField]int ammoAmmount;

    [SerializeField] bool isAmbushTrigger;
    [SerializeField] List<EnemyAI> enemies = new List<EnemyAI>();

    [Header("UI Data")]
    [SerializeField] GameEvent turnOnPanel;
    [TextArea(3,5)] public string TextToDisplay;


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
                    turnOnPanel.Raise(this, TextToDisplay);
                    break;
                }

            case objectType.Pills:
                {
                    playerData.playerPills += pillsAmmount;
                    gameObject.SetActive(false);
                    turnOnPanel.Raise(this, TextToDisplay);
                    break;
                }

            case objectType.Ammo:
                {
                    playerData.playerAmmo += ammoAmmount;
                    gameObject.SetActive(false);
                    turnOnPanel.Raise(this, TextToDisplay);
                    break;
                }

            case objectType.Weapon:
                {
                    playerData.hasGun = true;
                    player.ActivateGun();
                    gameObject.SetActive(false);
                    turnOnPanel.Raise(this, TextToDisplay);
                    break;
                }
        }
        if(isAmbushTrigger)
        {
            foreach(EnemyAI enemy in enemies)
            {
                enemy.startAmbush = true;
            }
        }
    }

}
