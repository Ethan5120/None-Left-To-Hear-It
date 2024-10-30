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
        GameObject animatorObject = GameObject.Find("Base");
        CamDoor.SetActive(false);      
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "DoorUNLOCKED")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CamDoor.SetActive(true);
                puertaAnim.SetTrigger("DoorOpen");
                camaraAnim.SetTrigger("CamWalk");
                //SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
