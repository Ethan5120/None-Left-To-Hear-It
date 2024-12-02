using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] List<EnemyData> enemies = new List<EnemyData>();

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(EnemyData enemy in enemies)
            {
                enemy.startAmbush = true;
            }
        }
    }
}
