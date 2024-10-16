using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "SceneData/PlayerData")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] public float playerHP;
    [SerializeField] public float playerAmmo;
    [SerializeField] public List<bool> PlayerKeys = new List<bool>();

}
