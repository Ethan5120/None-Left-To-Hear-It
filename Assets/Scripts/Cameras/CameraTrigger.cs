using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cameraToTrigger;
    [SerializeField] CameraManager cameraChanger;
    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            cameraChanger.ChangeStaticCamera(cameraToTrigger);
        }
    }
}
