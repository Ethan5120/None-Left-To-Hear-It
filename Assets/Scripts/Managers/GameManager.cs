using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] ItemManager items;
    void Start()
    {  
       items.InitializeItems(); 
    }


    void Update()
    {
        
    }
}
