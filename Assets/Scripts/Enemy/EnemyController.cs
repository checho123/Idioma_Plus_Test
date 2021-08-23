using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    [Header("Variables The Enemy")]
    [SerializeField]
    private Enemy enemy;
    // Navegation AI
    private NavMeshAgent agent;
    // Player Controller
    private GameObject player;
    // Attack Enemy
    [SerializeField]
    private GameObject proyectil;
    // Layer Mask 
    [SerializeField]
    private LayerMask whatIsPlayer, whatIsGrounded;
    // Animator Controller
    [SerializeField]
    private Animator animEnemy;

    private void Start()
    {
        // AI Enemy
        agent = GetComponent<NavMeshAgent>();
        // Player Referent
        player = GameObject.FindGameObjectWithTag("Player");
        // Enemy
        animEnemy = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        enemy.playerInSinghtRange = Physics.CheckSphere(transform.position, enemy.singhtAttack, whatIsPlayer);
        enemy.playerInAttackRange = Physics.CheckSphere(transform.position, enemy.singhtAttack, whatIsPlayer);

        if (!enemy.playerInSinghtRange && !enemy.playerInAttackRange){ PatronLing(); }
        if (enemy.playerInSinghtRange && !enemy.playerInAttackRange){ ChangePlayer();}
        if (enemy.playerInSinghtRange && enemy.playerInAttackRange){ AttackPlayer(); }
    }

    private void PatronLing()
    {
        if (!enemy.walkpointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(enemy.walkPoint);
        }

        Vector3 distanceToWallPoint = transform.position - enemy.walkPoint;
        // Walkpoint reached
        if (distanceToWallPoint.magnitude < 0.5f)
        {
            
            enemy.walkpointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float ramdoZ = Random.Range(-enemy.walkpointRange, enemy.walkpointRange);
        float ramdoX = Random.Range(-enemy.walkpointRange, enemy.walkpointRange);

        enemy.walkPoint = new Vector3(transform.position.x + ramdoX, transform.position.y,  transform.position.z + ramdoZ);

        if (Physics.Raycast(enemy.walkPoint, -transform.up, 2f, whatIsGrounded))
            enemy.walkpointSet = true;
    }

    private void ChangePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void AttackPlayer()
    {
        // Walking Direction Player
        if (!enemy.stopDistance)
        {
            agent.SetDestination(player.transform.position);
            transform.LookAt(player.transform);
        }

        if (!enemy.alreadyAtack)
        {
            // Attack code here
            AttackEnemy();
            enemy.alreadyAtack = true;
            Invoke(nameof(ResetAttack), enemy.timeBetweenAttacks);
        }
    }

    private void AttackEnemy()
    {
        agent.SetDestination(player.transform.position);

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (enemy.distancePlayer >= distance)
        {
            animEnemy.SetTrigger("Attack");
            enemy.stopDistance = true;
            agent.SetDestination(transform.position);
            Instantiate(proyectil, transform.position, Quaternion.identity);
        }
    }

    private void ResetAttack()
    {
        enemy.alreadyAtack = false;
        enemy.stopDistance = false;
    }

    public void TakeDamage(int damage)
    {
        enemy.health -= damage;
        if (enemy.health <= 0)
        {
            Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, enemy.attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, enemy.singhtAttack);
    }
}
