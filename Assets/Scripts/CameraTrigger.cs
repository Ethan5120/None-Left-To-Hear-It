using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] int cameraToTrigger;
    [SerializeField] CameraChanger cameraChanger;
    void OnTriggerEnter(Collider collider)
    {
        cameraChanger.ChangeStaticCamera(cameraToTrigger);
    }
}
