using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public bool thirdPersonActive;
    [SerializeField] CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] string playerCameraName;
    [SerializeField] List<CinemachineVirtualCamera> staticCameras = new List<CinemachineVirtualCamera>();
    [SerializeField] GameObject cameraVolume;
    [SerializeField] int currentCamera;

    void Awake()
    {
    thirdPersonCamera = GameObject.Find(playerCameraName).GetComponent<CinemachineVirtualCamera>();
    }

    
    public void TriggerThirdPerson()
    {
        if(!thirdPersonActive)
        {
            thirdPersonCamera.Priority = 30;
            cameraVolume.SetActive(false);
            thirdPersonActive = true;
        }
        else
        {
            thirdPersonCamera.Priority = 0;
            cameraVolume.SetActive(true);
            thirdPersonActive = false;
        }
    }

    public void ChangeStaticCamera(int cameraToChange)
    {
        if(currentCamera != cameraToChange)
        {
            staticCameras[currentCamera].Priority = 0;
            staticCameras[cameraToChange].Priority = 20;
            currentCamera = cameraToChange;
        }
    }
}
