using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Light _mainDirectionalLight;
    
    private async void Start()
    {
        BindObjects();
        //ShowTheLoadingScreen 
        //await InitializeObjects();

    }

    private void BindObjects()
    {
        _mainCamera = Instantiate(_mainCamera);
        _mainDirectionalLight = Instantiate(_mainDirectionalLight);
    }

    /*private async UniTask InitializeObjects()
    {

    }*/
}
