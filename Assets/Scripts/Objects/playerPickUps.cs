using UnityEngine;

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

                break;
            }

            case objectType.Pills:
            {
                playerData.playerPills += pillsAmmount;
                memory.hasBeenCollected = true;
                gameObject.SetActive(false);

                break;
            }

            case objectType.Ammo:
            {
                playerData.playerAmmo += ammoAmmount;
                memory.hasBeenCollected = true;
                gameObject.SetActive(false);

                break;
            }


        }
    }

}
