using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public bool thirdPersonActive;
    [SerializeField] CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] string playerCameraName;

    [SerializeField] GameObject cameraVolume;
    [SerializeField] CinemachineVirtualCamera currentCamera;

    void Awake()
    {
        thirdPersonCamera = GameObject.Find(playerCameraName).GetComponent<CinemachineVirtualCamera>();
    }

    
    public void TriggerThirdPerson()
    {
        if(!thirdPersonActive)
        {
            thirdPersonCamera.Priority = 30;
            if(cameraVolume != null) cameraVolume.SetActive(false);
            thirdPersonActive = true;
        }
        else
        {
            thirdPersonCamera.Priority = 0;
            if(cameraVolume != null) cameraVolume.SetActive(true);
            thirdPersonActive = false;
        }
    }

    public void ChangeStaticCamera(CinemachineVirtualCamera cameraToChange)
    {
        if(currentCamera != cameraToChange)
        {
            currentCamera.Priority = 0;
            cameraToChange.Priority = 20;
            currentCamera = cameraToChange;
        }
    }
}
