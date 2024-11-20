using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPrefabController : MonoBehaviour
{
    [SerializeField] int destination;
    // Start is called before the first frame update
    public void ChangeScene()
    {
        SceneManager.LoadScene(destination);
    }


    public void SetScene(Component sender, object data)
    {
        if(data is int)
        {
            destination = (int) data;
        }
    }
}
