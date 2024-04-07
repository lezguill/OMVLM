
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;
    public Animator anim;
    public LayerMask whatIsGround, whatIsPlayer;
    public float walkingSpeed = 2f;
    public float runningSpeed = 4f;
    public int attackDamage = 20;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public AttackCollision attackCollision;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private bool enemyCounted=false;
    private Animator playerState;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (anim.GetBool("isAlive"))
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer) && playerState.GetBool("isAlive");
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer) && playerState.GetBool("isAlive");

            if (!playerInSightRange && !playerInAttackRange && !anim.GetBool("isAttacking"))
            {
                agent.speed = walkingSpeed;
                Patroling();
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
            }
            else if (playerInSightRange && !playerInAttackRange && !anim.GetBool("isAttacking"))
            {
                agent.speed = runningSpeed;
                ChasePlayer();
                anim.SetBool("isRunning", true);
            }
            else if (playerInAttackRange && playerInSightRange && !anim.GetBool("isAttacking"))
            {
                AttackPlayer();
            }
        }
        else
        {
            if (enemyCounted == false)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacterController>().numberOfEnemiesKilled += 1;
                anim.SetTrigger("DeathAction");
                enemyCounted=true;
            }
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            transform.LookAt(player.position);
            transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

            anim.SetTrigger("Attack");
            anim.SetBool("isAttacking", true);
            alreadyAttacked = true;
            Invoke(nameof(Attack), timeBetweenAttacks * 0.6f);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            
        }
    }
    private void ResetAttack()
    {

        alreadyAttacked = false;
        anim.SetBool("isAttacking", false);
    }
    private void Attack()
    {
        if (attackCollision.touchingPlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
    public void Die()
    {
        
        anim.SetBool("isAlive", false);
        
    }

        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
