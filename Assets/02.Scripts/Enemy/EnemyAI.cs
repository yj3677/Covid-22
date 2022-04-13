using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 공격거리, 쫓아가는 거리, 죽었는지 유무 판단
/// </summary>
public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL, TRACE, ATTACK, DIE
    }
    public State state = State.PATROL;
    private Transform playerTr;
    private Transform enemyTr;
    public Animator animator;
    private PlayerState playerState;

    public float attackDist = 5;
    public float traceDis = 10;
    public bool isDie = false;

    private float offset;
    private float walkSpeed;

    private WaitForSeconds waitForSeconds;
    private EnemyMove enemyMove;
    private EnemyFire enemyFire;


    private void Awake()
    {
        enemyMove = GetComponent<EnemyMove>();
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemyFire = GetComponent<EnemyFire>();
        playerState = FindObjectOfType<PlayerState>();
        waitForSeconds = new WaitForSeconds(0.3f);
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player !=null)
        {
            playerTr = player.GetComponent<Transform>();
        }
        animator.SetFloat("Offset", Random.Range(0, 1));
        animator.SetFloat("WalkSpeed", Random.Range(1, 1.2f));
    }
    private void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
        PlayerState.OnPlayerDie += this.OnPlayerDie;
    }
    private void OnDisable()
    {
        PlayerState.OnPlayerDie -= this.OnPlayerDie;
    }
    private void Start()
    {
        
    }

    IEnumerator Action()
    {
        //적 캐릭터가 사망할 때까지 무한루프
        while (!isDie)
        {
            yield return waitForSeconds;
            //상태에 따라 분기처리
            switch(state)
            {
                case State.PATROL:
                    //공격정지
                    enemyFire.isFire = false;
                    enemyMove.patrolling = true;
                    animator.SetBool("IsWalk", true);
                    break;
                case State.TRACE:
                    enemyFire.isFire = false;
                    enemyMove.traceTarget = playerTr.position;  //플레이어 추적
                    animator.SetBool("IsWalk", true);
                    break;
                case State.ATTACK:
                    enemyMove.StopEnemy();
                    animator.SetBool("IsWalk", false);
                    if (enemyFire.isFire == false)
                    {
                        enemyFire.isFire = true;
                    }
                    break;
                case State.DIE:
                    this.gameObject.tag = "Untagged"; //적을 재생성 시키기 위해
                    isDie = true;
                    enemyFire.isFire = false;
                    enemyMove.StopEnemy();
                    //Invoke("EnemyRemove", 0.6f);
                    animator.SetTrigger("Die");
                    GetComponent<NavMeshAgent>().enabled = false;
                    
                    break;
            }
        }
    }
    IEnumerator CheckState()
    {
        while(!isDie)
        {
            if (state == State.DIE)
            {
                yield break;
            }
            
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);

            if (dist <= attackDist)
            {
                state = State.ATTACK;
            }
            else if (dist <= traceDis)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            yield return waitForSeconds;
            
            
        }
    }
    public void OnPlayerDie()
    {
        //플레이어가 죽었을 때 살아있는 적들 중지
        Debug.Log("실행 확인");
        if (!isDie)
        {
            enemyMove.agent.isStopped = true;
            enemyFire.isFire = false;
            animator.SetBool("IsWalk", false);
            StopAllCoroutines();
        }
    }
    public void EnemyRemove()
    {
        //죽은 적 비활성화 시키기
        //아이템 리스폰 결정됐을 때 수정하기
        //this.gameObject.SetActive(false);
    }
   
}
