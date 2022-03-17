using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 일정시간 지나면 AI가 플레이어에게 관심X or 안움직임
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround, isPlayer;
    Animator anim;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    float currentIdle;


    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        AIRangeCheck();
    }
    private void Update()
    {
       AIRangeCheck();
    }

    void AIRangeCheck()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);  //시야범위
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer); //공격범위
        Debug.Log(playerInAttackRange);
        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
            
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
        


    }
    void Patroling()
    {

        if (!walkPointSet) //false면 이동할 좌표 구해라
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint); //목적지로 이동
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint; //목적지로 이동
        AiIdle();
        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
            Invoke("WalkPointReset", 10); //순찰 반복
        }

    }
    void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);  //AI랜덤이동
        
        walkPointSet = true;

    }
    void AiIdle() //10초가 되면 멈추기
    {
        currentIdle += Time.deltaTime;
        if (currentIdle >= 10)
        {
            anim.SetBool("IsWalk", false);
            agent.speed = 0;
            currentIdle = 0;
            Invoke("AiMove", 5);
        }
    }
    void AiMove()
    {
        agent.speed = 2;
    }
    void WalkPointReset()
    {
        walkPointSet = !walkPointSet;

    }
    void ChasePlayer()
    {
        agent.SetDestination(player.position); 
    }
    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        
        if (!alreadyAttacked)
        {
            anim.SetBool("IsWalk", false);
            //Attack 
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();  //공격체 생성
            rb.AddForce(transform.forward * 32, ForceMode.Impulse);
            rb.AddForce(transform.up * 8, ForceMode.Impulse);
            

            alreadyAttacked = true;
            Invoke("ResetAttack", timeBetweenAttacks);  //공격시간
        }
    }
    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Invoke("DestroyEnemy", 0.5f);
        }
    }
    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
