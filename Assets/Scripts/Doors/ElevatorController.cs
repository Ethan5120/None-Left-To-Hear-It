using UnityEngine;



public class ElevatorController : MonoBehaviour, I_Interactable
{

    [SerializeField] PlayerSO playerData;
    [SerializeField] GM_ScriptableObject managerData;
    public SceneField sceneToLoad;
    [SerializeField] Animator animator;
    [SerializeField] string AnimationName;
    [SerializeField] GameObject changeCamera;
    [SerializeField] GameEvent setDestination;
    [SerializeField] GameEvent saveData;

    [SerializeField] GameObject LightOn;

    [Header("ChangeSettings")]
    [SerializeField] Vector3 positionToSpawn;
    [SerializeField] Quaternion rotationToSpawn;

    [Header("UI Data")]
    [SerializeField] GameEvent turnOnPanel;
    [SerializeField][TextArea(3,5)] string TextToDisplay;

    void Start()
    {
        if (!managerData.isEnergyOn)
        {
            LightOn.SetActive(false);
        }
    }

    public void Interact()
    {
        Debug.Log("Interact");
        //SetSpawn();
        StartAnim();
    }


    public void StartAnim()
    {
        if(managerData.isEnergyOn && changeCamera != null)
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
        playerData.spawnPosition = positionToSpawn;
        playerData.spawnRotation = rotationToSpawn;
    }

}
