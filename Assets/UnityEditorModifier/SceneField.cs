using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SceneField
{
    [SerializeField]
    private Object _sceneAsset;

    [SerializeField]
    private string _sceneName = "";

    public string SceneName
    {
        get {return _sceneName; }
    }

    //Makes it work with the existing Unity Methods (LoadLevel/LoadScene)
    public static implicit operator string(SceneField sceneField)
    {
        return sceneField.SceneName;
    }
}
