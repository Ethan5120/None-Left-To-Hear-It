using UnityEngine;
using System.Collections.Generic;

public class playerPickUps : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerSO playerData;
    [SerializeField] pickUp_SO memory;
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


    void  Start()
    {
        if(memory == null)
        {
            memory = ScriptableObject.CreateInstance<pickUp_SO>();
        }
        else
        {
            if(memory.hasBeenCollected)
            {
                gameObject.SetActive(false);
            }
        } 
    }


    public void Interact()
    {
        Debug.Log("Interact");
        switch (objectSelected)
        {
            case objectType.Key:
            {
                playerData.PlayerKeys[keyIndex] = true;
                memory.hasBeenCollected = true;
                gameObject.SetActive(false);
                turnOnPanel.Raise(this, $"Key Number {keyIndex + 1}");
                break;
            }

            case objectType.Pills:
            {
                playerData.playerPills += pillsAmmount;
                memory.hasBeenCollected = true;
                gameObject.SetActive(false);
                turnOnPanel.Raise(this, $"{pillsAmmount}x Pill(s)");
                break;
            }

            case objectType.Ammo:
            {
                playerData.playerAmmo += ammoAmmount;
                memory.hasBeenCollected = true;
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
