using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{

    [SerializeField] PlayerSO playerData;
    [SerializeField] int neededKey;
    [SerializeField] int sceneToLoadIndex;
    [SerializeField] Animator animator;
    [SerializeField] string AnimationName;
    [SerializeField] GameObject changeCamera;
    [SerializeField] GameEvent setDestination;
    [SerializeField] GameEvent saveData;

    [Header("HubSettings")]
    [SerializeField] Vector3 positionToSpawn;
    [SerializeField] Quaternion rotationToSpawn;


    public void Interact()
    {
        Debug.Log("Interact");
        SetSpawn();
        StartAnim();
    }


    public void StartAnim()
    {
        if(playerData.PlayerKeys[neededKey] == true && changeCamera != null)
        {
            changeCamera.SetActive(true);
            setDestination.Raise(this, sceneToLoadIndex);
            saveData.Raise(this, null);
            animator?.Play(AnimationName);
        }
        else
        {
            Debug.Log("This Door is Close");
        }
    }

    void SetSpawn()
    {
        if(playerData.isInHub)
        {
            playerData.spawnPosition = positionToSpawn;
            playerData.spawnRotation = rotationToSpawn;
        }
    }

}
