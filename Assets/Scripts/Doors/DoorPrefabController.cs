using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPrefabController : MonoBehaviour
{
    public AudioSource DoorOpenSound;
    [SerializeField] SceneField sceneToLoad;
    // Start is called before the first frame update
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


    public void SetScene(Component sender, object data)
    {
        if(data is SceneField)
        {
            sceneToLoad = (SceneField) data;
        }
    }

    public void DoorSounds(){
        DoorOpenSound.Play();
    }
}
