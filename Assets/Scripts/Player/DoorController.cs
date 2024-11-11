using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour, IInteractable
{

    [SerializeField] PlayerSO playerData;
    [SerializeField] int neededKey;
    [SerializeField] int sceneToLoadIndex;
    [SerializeField] bool InTriggerRange;
    Animator animator;
    [SerializeField] Animator animatorCamera;
    [SerializeField] string AnimationName;
    [SerializeField] string AnimationCameraName;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Interact()
    {
        StartAnim();
    }


    public void StartAnim()
    {
        if(playerData.PlayerKeys[neededKey] == true)
        {
            animator?.Play(AnimationName);
            animatorCamera?.Play(AnimationCameraName);
        }
        else
        {
            Debug.Log("This Door is Close");
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }


}
