using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private PlayerController playerController;
    public HealthBar healthBar;
    public Camera playerCamera;

    public int maxHealth = 100;
    private int currentHealth;
    private bool isAttacking = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = Camera.main.transform;
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerController = playerCamera.GetComponent<PlayerController>();
    }

    void Update()
    {
        animator.SetBool("isIdle", false);
        animator.SetTrigger("Run Forward");
        navMeshAgent.SetDestination(player.position);

        Vector3 monsterPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 playerPos = new Vector3(player.position.x, 0f, player.position.z);

        if (Vector3.Distance(monsterPos, playerPos) <= navMeshAgent.stoppingDistance)
        {
            animator.SetBool("isIdle", true);

            if (!isAttacking)
            {
                Debug.Log("here");
                isAttacking = true;
                StartCoroutine(AttackPlayer());
            }

            animator.ResetTrigger("Run Forward");
        }
    }

    IEnumerator AttackPlayer()
    {
        int randomAttack = Random.Range(1, 3); // randomly choose 1 or 2

        string attackTrigger = "Attack 0" + randomAttack.ToString();
        animator.SetTrigger(attackTrigger);

        yield return new WaitForSeconds((float)0.8);
        playerController.TakeDamage(10);

        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        playerController.AddScore(damage);

        if (damage >= currentHealth)
        {   
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            playerController.AddScore(50);
            Die();
        }
        else
        {
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
        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
