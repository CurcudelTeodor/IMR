using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public Transform player; // Reference to the player (Main Camera in this case)
    public Animator animator;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            player = Camera.main.transform;
        }
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
            Debug.Log("Banana");
            animator.SetBool("isIdle", true);
            animator.ResetTrigger("Run Forward");
        }
    }
}