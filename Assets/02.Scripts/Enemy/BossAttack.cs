using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int damage;
    public bool isFire = false;
    private float nextFire = 0;
    private readonly float fireRate = 0.1f;
    private readonly float damping = 10.0f;

    public AudioSource audioEnemy;
    public AudioClip fireClip;
    public Animator anim;

    public GameObject bullet;
    public Transform firePos;
    private Transform playerTr;
    public Transform enemyTr;
    private PlayerState playerState;
    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

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
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.transform.position);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Fire");
        audioEnemy.PlayOneShot(fireClip,1.0f);
        GameObject _bullet = Instantiate(bullet, firePos.position, firePos.rotation);  //총구 위치에서 총알 생성
        Destroy(_bullet, 3.0f); //3초 뒤 제거
    }
}
