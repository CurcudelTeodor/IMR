using EZCameraShake;
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

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;

    private Vector3 originalCameraPosition;

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
        // Store the original position of the camera
        originalCameraPosition = playerCamera.transform.localPosition;

        int randomAttack = Random.Range(1, 3); // randomly choose 1 or 2

        string attackTrigger = "Attack 0" + randomAttack.ToString();
        animator.SetTrigger(attackTrigger);

        // Add camera shake when attacking
        //StartCoroutine(CameraShake(shakeDuration, shakeMagnitude));
        CameraShaker.Instance.ShakeOnce(3f, 1f, 0.1f, 1f);

        yield return new WaitForSeconds((float)0.8);
        playerController.TakeDamage(10);

        isAttacking = false;
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        Debug.Log("Im shaking the camera");
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Apply the shake offset to the camera's position relative to its current position
            playerCamera.transform.localPosition += new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the camera position to the original position after the shake
        playerCamera.transform.localPosition = originalCameraPosition;
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
