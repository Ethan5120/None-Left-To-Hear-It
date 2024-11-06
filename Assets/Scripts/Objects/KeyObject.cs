using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [SerializeField] PlayerSO playerData;
    [SerializeField] int keyItem;
    void OnInteract()
    {
        playerData.PlayerKeys[keyItem] = true;
    }
}
