using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int damage;
    private Animator anim;
    private Transform playerTr;
    private Transform enemyTr;
    private PlayerState playerState;
    public bool isFire = false;
    private float nextFire = 0;
    private readonly float fireRate = 0.1f;
    private readonly float damping = 10;
    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttack();
    }
    //isFire면 애너미가 공격
    void EnemyAttack()
    {
        if (playerState.isDead) //플레이어 죽었을 때 공격 안함
        {
            return;
        }
        if (isFire)
        {
            //공격 후, time+딜레이+랜덤 딜레이 저장
            if (Time.time >= nextFire)
            {
                Attack();
                nextFire = Time.time + fireRate + Random.Range(1, 5f);
            }
            //플레이어를 바라보게 함. 시간에 따라 점진적으로 회전 시킴.
            Quaternion rot = Quaternion.LookRotation(playerTr.position - transform.position);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }

    }

    private void Attack()
    {
        throw new System.NotImplementedException();
    }
}
