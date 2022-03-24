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
    private Animator animator;

    public float attackDist = 5;
    public float traceDis = 10;
    public bool isDie = false;



    private WaitForSeconds waitForSeconds;
    private EnemyMove enemyMove;
    private EnemyFire enemyFire;


    private void Awake()
    {
        enemyMove = GetComponent<EnemyMove>();
        enemyTr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        enemyFire = GetComponent<EnemyFire>();
        waitForSeconds = new WaitForSeconds(0.3f);
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player !=null)
        {
            playerTr = player.GetComponent<Transform>();
        }
    }
    private void OnEnable()
    {
        StartCoroutine(CheckState());
        StartCoroutine(Action());
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
                    enemyMove.StopEnemy();
                    
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
   
}
