using UnityEngine;

public class MonsterChasingScript : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // You can add additional behavior here if needed
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Add any additional behavior upon taking damage (e.g., play a hit animation, show health bar, etc.)

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Bullet hit!");
            int damage = 50;
            TakeDamage(damage);
            Debug.Log("Currect health: " + currentHealth);
            Destroy(other.gameObject);
        }
    }
}
