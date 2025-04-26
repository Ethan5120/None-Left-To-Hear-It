using Unity.VisualScripting;
using UnityEngine;

public class FinalDoor : MonoBehaviour, I_Interactable
{

    [SerializeField] PlayerSO playerData;
    [SerializeField] AudioSource lockedDoor;
    [SerializeField] Animator animator;
    [SerializeField] string AnimationName;



    public void Interact()
    {
        if(CheckKeys())
        {
            animator.Play(AnimationName);
        }
        else
        {
            lockedDoor.Play();
        }
    }

    bool CheckKeys()
    {
        foreach (bool key in playerData.PlayerKeys)
        {
            if(key == false)
            {
                return false;
            }
            else
            {
                continue;
            }
        }
        return true;
    }


    void Update()
    {
        float deltaTime = Time.unscaledDeltaTime;
        animator.Update(deltaTime / 2);
    }

}
