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

    public float attackDist = 5;
    public float traceDis = 10;
    public bool isDie = false;

    private WaitForSeconds waitForSeconds;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player !=null)
        {
            playerTr = player.GetComponent<Transform>();
            enemyTr = GetComponent<Transform>();
            waitForSeconds = new WaitForSeconds(0.3f);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(CheckState());

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
            
            if (dist<=attackDist)
            {
                state = State.ATTACK;
            }
            else if (dist<=traceDis)
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
