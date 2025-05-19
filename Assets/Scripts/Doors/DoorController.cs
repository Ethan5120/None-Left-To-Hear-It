using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorController : MonoBehaviour, I_Interactable
{

    [SerializeField] PlayerSO playerData;
    [SerializeField] GM_ScriptableObject managerData;
    [SerializeField] int neededKey;
    public SceneField sceneToLoad;
    [SerializeField] Animator animator;
    [SerializeField] string AnimationName;
    [SerializeField] GameObject changeCamera;
    [SerializeField] GameEvent setDestination;
    [SerializeField] GameEvent saveData;

    [Header("ChangeSettings")]
    [SerializeField] Vector3 positionToSpawn;
    [SerializeField] Quaternion rotationToSpawn;

    [Header("UI Data")]
    [SerializeField] GameEvent turnOnPanel;
    [SerializeField][TextArea(3,5)] string TextToDisplay;


    public void Interact()
    {
        Debug.Log("Interact");
        //SetSpawn();
        StartAnim();
    }


    public void StartAnim()
    {
        if(playerData.PlayerKeys[neededKey] == true && changeCamera != null)
        {
            managerData.gameTime = 0;
            SetSpawn();
            changeCamera.SetActive(true);
            setDestination.Raise(this, sceneToLoad);
            saveData.Raise(this, null);
            animator?.Play(AnimationName);
            
        }
        else
        {
            turnOnPanel.Raise(this, TextToDisplay);
        }
    }

    void SetSpawn()
    {
        PlayerController.newPlayerSpawn = positionToSpawn;
        PlayerController.newPlayerRotation = rotationToSpawn;
    }

}
