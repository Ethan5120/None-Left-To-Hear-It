using System.Linq;
using Cinemachine;
using UnityEngine;

public class Padlock_MainScript : MonoBehaviour, I_Interactable
{
    [SerializeField] private int[] result, solution;

    public bool isInteractingLock;

    [SerializeField] GameEvent lockOpen;
    [SerializeField] CameraManager camManager;
    [SerializeField] GameObject interactCamera;
    [SerializeField] GM_ScriptableObject managerData;

    void OnEnable()
    {
        Padlock_Rotate.Rotated += CheckResults;
    }

    void OnDisable()
    {
        Padlock_Rotate.Rotated -= CheckResults;
    }

    public void Start()
    {
        interactCamera.SetActive(false);
    }

    private void CheckResults(string dialName, int number)
    {
        switch(dialName)
        {
            case "Dial1":
                result[0] = number;
                break;
            
            case "Dial2":
                result[1] = number;
                break;

            case "Dial3":
                result[2] = number;
                break;
        }

        if(result.SequenceEqual(solution))
        {
            Debug.Log("Open");
            Interact();
            lockOpen.Raise(this, null);
        }
    }

    public void Interact()
    {
        if(isInteractingLock == true)
        {
            isInteractingLock = false;
            interactCamera.SetActive(false);
            managerData.gameTime = 1;
        }
        else
        {
            Debug.Log("interactingLock");
            isInteractingLock = true;
            interactCamera.SetActive(true);
            managerData.gameTime = 0; 
        }
    }
}
