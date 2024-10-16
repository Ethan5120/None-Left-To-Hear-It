using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{

    [SerializeField] PlayerSO playerData;
    [SerializeField] int neededKey;
    [SerializeField] string SceneToLoad;
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "DoorUNLOCKED" && playerData.PlayerKeys[neededKey] == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(SceneToLoad);
            }
        }
        else
        {
            Debug.Log("This Door is locked");
        }
    }
}
