using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
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


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            InTriggerRange = true;
        }
        else
        {
            InTriggerRange = false;
        }
    }


    public void StartAnim(Component sender, object data)
    {
        if(sender is PlayerController && InTriggerRange == true)
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
        else
        {
            return;
        }

    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }
}
