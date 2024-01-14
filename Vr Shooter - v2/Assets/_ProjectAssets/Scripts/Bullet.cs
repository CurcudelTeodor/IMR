using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;  // Adjust damage value as needed

    void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has the MonsterController script
        MonsterController monster = other.GetComponent<MonsterController>();

        // If it does, apply damage to the monster
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}
