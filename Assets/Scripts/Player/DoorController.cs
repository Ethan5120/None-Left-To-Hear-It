using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour, IInteractable
{

    [SerializeField] PlayerSO playerData;
    [SerializeField] int neededKey;
    [SerializeField] int sceneToLoadIndex;
    Animator animator;
    [SerializeField] string AnimationName;

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
