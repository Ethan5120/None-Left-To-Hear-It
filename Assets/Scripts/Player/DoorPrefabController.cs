using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPrefabController : MonoBehaviour
{
    public AudioSource DoorOpenSound;
    [SerializeField] Scene sceneToLoad;
    // Start is called before the first frame update
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad.name);
    }


    public void SetScene(Component sender, object data)
    {
        if(data is Scene)
        {
            sceneToLoad = (Scene) data;
        }
    }

    public void DoorSounds(){
        DoorOpenSound.Play();
    }
}
