using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetReset : MonoBehaviour
{
    [SerializeField] GM_ScriptableObject managerData;
    void Awake()
    {
        managerData.isFirstLoad = true;
    }
}
