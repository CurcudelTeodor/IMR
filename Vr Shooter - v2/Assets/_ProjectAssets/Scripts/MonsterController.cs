using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public Transform player; // Reference to the player (Main Camera in this case)
    public Animator animator;
    private NavMeshAgent navMeshAgent;

    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = Camera.main.transform;
        }

        currentHealth = maxHealth;
    }

    void Update()
    {
        animator.SetBool("isIdle", false);
        animator.SetTrigger("Run Forward"); // daca scoatem asta apare si animatia de hit, altfel nu mereu
        navMeshAgent.SetDestination(player.position);

        // Project both positions onto the same plane (ignore the Y component)
        Vector3 monsterPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0f, player.position.z);


        if (Vector3.Distance(monsterPos, playerPos) <= navMeshAgent.stoppingDistance)
        {   
            animator.SetBool("isIdle", true);
            animator.ResetTrigger("Run Forward");
        }
    }

    public void TakeDamage(int damage)
    {   
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Play Take Damage animation or perform other actions
            animator.SetTrigger("Take Damage");
            Debug.Log("Monster took damage. Current health: " + currentHealth);
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        Debug.Log("Monster died");
        StartCoroutine(PauseAndDestroy(2f));
    }

    IEnumerator PauseAndDestroy(float duration)
    {
        yield return new WaitForSeconds(duration);

        // This will be executed after waiting for the specified duration
        Destroy(gameObject);
    }

    // if we don't want to use the Bullet script attached to the Bullet Object
    /*void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Bullet hit!");
            int damage = 10;
            TakeDamage(damage);
            Debug.Log("Current health: " + currentHealth);
            Destroy(other.gameObject);
        }
    }*/
}