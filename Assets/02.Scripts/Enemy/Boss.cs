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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        while (!isDie)
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
}
