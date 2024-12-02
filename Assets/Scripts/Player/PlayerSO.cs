using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SceneData/PlayerData")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] public int playerHP;
    [SerializeField] public int playerPills;
    [SerializeField] public int playerAmmo;
    [SerializeField] public List<bool> PlayerKeys = new List<bool>();

}
