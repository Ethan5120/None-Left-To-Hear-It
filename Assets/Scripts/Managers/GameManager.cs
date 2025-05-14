using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GM_ScriptableObject GM;
    [SerializeField] ItemManager items;
    void Start()
    {  
        GM.gameTime = 1;
        items.InitializeItems(); 
    }
}
