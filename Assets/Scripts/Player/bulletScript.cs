using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    public float bulletLife = 5f;
    public float currentLife;
    
     void Update()
    {
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;// * GM.GameTime; <--- Future Pause
        Destroy(gameObject, bulletLife);
    }


    void OnCollisionEnter(Collision collision) 
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().TakeDamage(1);
        }
        
        Destroy(gameObject);
    }
}
