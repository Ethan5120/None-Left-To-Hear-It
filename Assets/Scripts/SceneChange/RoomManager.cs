using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GM_ScriptableObject managerData;
    void Awake()
    {
        managerData.isInHub = false;
    }

}
