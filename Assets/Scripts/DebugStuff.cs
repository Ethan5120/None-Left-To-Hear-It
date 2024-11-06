using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugStuff : MonoBehaviour
{
    public GameEvent onSceneChange;
    public int Scene1, Scene2;
   
    public void ChangeToTest()
    {
        onSceneChange.Raise(this, null);
        SceneManager.LoadScene(Scene1);
    }

    public void ChangeToBlank()
    {
        onSceneChange.Raise(this, null);
        SceneManager.LoadScene(Scene2);
    }
}
