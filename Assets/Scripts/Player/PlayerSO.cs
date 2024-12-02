using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SceneData/PlayerData")]
public class PlayerSO : ScriptableObject
{
    public int playerHP;
    public int playerPills;
    public int playerAmmo;
    public List<bool> PlayerKeys = new List<bool>();

    [Header("HUB Data")]
    public bool isInHub;
    public bool isFirstLoad;
    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
}
