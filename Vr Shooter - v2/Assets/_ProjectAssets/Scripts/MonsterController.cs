using EZCameraShake;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // Import the Universal Render Pipeline namespace

public class MonsterController : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private PlayerController playerController;
    public HealthBar healthBar;
    public Camera playerCamera;

    public int maxHealth = 50;
    public int monsterDamage;
    private int currentHealth;
    private bool isAttacking = false;

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.2f;

    private Vector3 originalCameraPosition;

    public float intensity = 0;
    public Volume volume; // Reference to the Volume component
    
    private float weight;
    public ParticleSystem deathParticles;

    public AudioClip[] deathSounds; // Array to hold death sounds
    private AudioSource audioSource; // Reference to AudioSource component

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

        // Get the Vignette volume component
        
        weight = volume.weight;
        // Add AudioSource component and assign it to the variable
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public IEnumerator TakeDamageEffect()
    {
        Debug.Log("Taking damage...");
        weight = 1f;

        yield return new WaitForSeconds(0.1f);

        while (weight > 0)
        {
            weight -= 0.01f;
            if (weight < 0) weight = 0;

            volume.weight = weight;

            yield return new WaitForSeconds(0.1f);
        }
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
           // StartCoroutine(TakeDamageEffect());
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
                Debug.Log("Attacking...");
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
        StartCoroutine(TakeDamageEffect());

        /*
        if (takeDamageScript != null)
        {
            Debug.Log("Triggering vignette effect");
            yield return StartCoroutine(takeDamageScript.TakeDamageEffect());
        }*/

        yield return new WaitForSeconds((float)0.8);
        playerController.TakeDamage(monsterDamage);

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
        Debug.Log("Monster took " + damage);
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

        // Play a random death sound from the array
        if (deathSounds.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, deathSounds.Length);
            audioSource.PlayOneShot(deathSounds[randomIndex]);
        }

        StartCoroutine(PauseAndDestroy(1.5f));
    }

    IEnumerator PauseAndDestroy(float duration)
    {
        yield return new WaitForSeconds(duration);
        Instantiate(deathParticles, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
