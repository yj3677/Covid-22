using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public enum State
    {
        PATROL, TRACE, ATTACK, DIE
    }
    public State state = State.PATROL;
    public Transform playerTr;
    private Transform bossTr;
    public Animator animator;
    private PlayerState playerState;

    public float attackDist = 5;
    public float traceDis = 10;
    public bool isDie = false;

    private float offset;
    private float walkSpeed;

    private WaitForSeconds waitForSeconds;
    public BossAttack bossAttack;
    private BossMove bossMove;
    private void Awake()
    {
        bossMove = GetComponent<BossMove>();
        //bossAttack = GetComponent<BossAttack>();
        bossTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        playerState = FindObjectOfType<PlayerState>();
        waitForSeconds = new WaitForSeconds(0.3f);
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTr = player.GetComponent<Transform>();
        }
    }
    void Start()
    {
        animator.SetFloat("Offset", Random.Range(0, 1));
        animator.SetFloat("WalkSpeed", Random.Range(1, 1.2f));
        StartCoroutine(CheckState());
        StartCoroutine(Action());
        PlayerState.OnPlayerDie += this.OnPlayerDie;
    }

    private void OnDisable()
    {
        PlayerState.OnPlayerDie -= this.OnPlayerDie;
    }
 
    IEnumerator Action()
    {
        //적 캐릭터가 사망할 때까지 무한루프
        while (!isDie)
        {
            yield return waitForSeconds;
            //상태에 따라 분기처리
            switch (state)
            {
                case State.PATROL:
                    //공격정지
                    bossAttack.isFire = false;
                    bossMove.patrolling = true;
                    animator.SetBool("IsWalk", true);
                    break;
                case State.TRACE:
                    bossAttack.isFire = false;
                    bossMove.traceTarget = playerTr.position;  //플레이어 추적
                    animator.SetBool("IsWalk", true);
                    break;
                case State.ATTACK:
                    bossMove.StopEnemy();
                    animator.SetBool("IsWalk", false);
                    if (bossAttack.isFire == false)
                    {
                        bossAttack.isFire = true;
                    }
                    break;
                case State.DIE:
                    this.gameObject.tag = "Untagged"; //적을 재생성 시키기 위해
                    isDie = true;
                    bossAttack.isFire = false;
                    bossMove.StopEnemy();
                    //Invoke("EnemyRemove", 0.6f);
                    animator.SetTrigger("Die");
                    GetComponent<NavMeshAgent>().enabled = false;

                    break;
            }
        }
    }
    IEnumerator CheckState()
    {
        while (!isDie)
        {
            if (state == State.DIE)
            {
                yield break;
            }

            float dist = Vector3.Distance(playerTr.position, bossTr.position);

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
            bossMove.agent.isStopped = true;
            bossAttack.isFire = false;
            animator.SetBool("IsWalk", false);
            StopAllCoroutines();
        }
    }
}
