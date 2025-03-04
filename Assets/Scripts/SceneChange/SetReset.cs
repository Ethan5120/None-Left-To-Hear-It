using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReset : MonoBehaviour
{
     [SerializeField] PlayerSO playerData;
    void Awake()
    {
        playerData.isFirstLoad = true;
    }
}
