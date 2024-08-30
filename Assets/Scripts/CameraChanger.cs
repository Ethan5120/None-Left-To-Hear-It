using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    [SerializeField] bool thirdPersonActive;
    [SerializeField] CinemachineVirtualCamera thirdPersonCamera;
    [SerializeField] List<CinemachineVirtualCamera> staticCameras = new List<CinemachineVirtualCamera>();
    [SerializeField] int currentCamera;

    
    public void TriggerThirdPerson()
    {
        if(!thirdPersonActive)
        {
            thirdPersonCamera.Priority = 30;
            thirdPersonActive = true;
        }
        else
        {
            thirdPersonCamera.Priority = 0;
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
