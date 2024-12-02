using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] PlayerSO playerData;
    void Awake()
    {
        playerData.isInHub = false;
    }

}
