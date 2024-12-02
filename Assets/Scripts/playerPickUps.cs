using UnityEngine;

public class playerPickUps : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerSO playerData;
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




    public void Interact()
    {
        switch (objectSelected)
        {
            case objectType.Key:
            {
                playerData.PlayerKeys[keyIndex] = true;

                Destroy(gameObject);
                break;
            }

            case objectType.Pills:
            {
                playerData.playerPills += pillsAmmount;

                Destroy(gameObject);
                break;
            }

            case objectType.Ammo:
            {
                playerData.playerAmmo += ammoAmmount;

                Destroy(gameObject);
                break;
            }


        }
    }

}
