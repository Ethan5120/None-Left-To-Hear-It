using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] List<EnemyAI> enemies = new List<EnemyAI>();

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(EnemyAI enemy in enemies)
            {
                enemy.startAmbush = true;
            }
        }
    }
}
