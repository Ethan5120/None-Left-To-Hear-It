using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public GameObject CamDoor;
    public Animator puertaAnim, camaraAnim;

    private void Start()
    {
        CamDoor.SetActive(false);      
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "DoorUNLOCKED")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CamDoor.SetActive(true);
                
                    
                //SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
