using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

public class Padlock_MainScript : MonoBehaviour, I_Interactable
{
    [SerializeField] private int[] result;
    [SerializeField] private int[] solution;

    public bool isInteractingLock;

    //[SerializeField] GameEvent lockOpen;
    [SerializeField] CinemachineVirtualCamera interactCamera;
    [SerializeField] GameObject DialButtons;
    [SerializeField] GM_ScriptableObject managerData;
    [SerializeField] List<GameObject> Dials = new List<GameObject>();

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
        interactCamera.Priority = 0;
        isInteractingLock = false;
        DialButtons.SetActive(isInteractingLock);
        if(solution.Length != Dials.Count)
        {
            solution = new int[Dials.Count];
        }
    }

    private void CheckResults(GameObject dial, int number)
    {
        for(int i = 0; i < Dials.Count; i++)
        {
            if(dial == Dials[i])
            {
                result[i] = number;
            }
        }
        

        if(result.SequenceEqual(solution))
        {
            Debug.Log("Open");
            Interact();
            //lockOpen.Raise(this, null);
        }
    }

    public void Interact()
    {
        if(!isInteractingLock)
        {
            Debug.Log("interactingLock");
            isInteractingLock = true;
            DialButtons.SetActive(isInteractingLock);
            interactCamera.Priority = 80;
            managerData.gameTime = 0; 
        }
        else
        {
            isInteractingLock = false;
            DialButtons.SetActive(isInteractingLock);
            interactCamera.Priority = 0;
            managerData.gameTime = 1;
        }
    }
}
