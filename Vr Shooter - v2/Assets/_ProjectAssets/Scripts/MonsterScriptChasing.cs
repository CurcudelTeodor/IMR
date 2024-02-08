using UnityEngine;
using System.Collections;

public class MonsterChasingScript : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float moveSpeed = 5f;  // Adjust this value to control the speed of the chasing
    //private bool hasStartedMoving = false;

    private Transform playerTransform;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerTransform = Camera.main.transform; // Assuming you want to move towards the main camera

        // Set the initial animation state
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // Move towards the player
            animator.SetTrigger("Run Forward"); // daca scoatem asta apare si animatia de hit, altfel nu mereu
            // animator.SetTrigger("Attack 01");

            // Look at the player
            transform.LookAt(playerTransform);

            // Move towards the player
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

            // You can add additional behavior here if needed
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Set the trigger named "TakeDamage" on the animator
        animator.SetTrigger("Take Damage");

        // Add any additional behavior upon taking damage (e.g., play a hit animation, show health bar, etc.)

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        StartCoroutine(PauseAndDestroy(2f));
    }

    IEnumerator PauseAndDestroy(float duration)
    {
        yield return new WaitForSeconds(duration);

        // This will be executed after waiting for the specified duration
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
            Debug.Log("Current health: " + currentHealth);
            Destroy(other.gameObject);
        }
    }
}
