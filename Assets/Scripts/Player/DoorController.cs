using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorController : MonoBehaviour, IInteractable
{

    [SerializeField] PlayerSO playerData;
    [SerializeField] int neededKey;
    public Scene sceneToLoad;
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
            Time.timeScale = 0f;
            changeCamera.SetActive(true);
            setDestination.Raise(this, sceneToLoad);
            saveData.Raise(this, null);
            animator?.Play(AnimationName);
            
        }
        else
        {
            Debug.Log("This Door is Close");
        }
    }
    void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        animator.Update(deltaTime / 2);
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
