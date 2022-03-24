using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    private AudioSource _audio;
    private Animator anim;
    private Transform playerTr;
    private Transform enemyTr;

    public GameObject attackBullet;  //공격 수단

    private readonly int hashFire = Animator.StringToHash("Fire");

    private float nextFire = 0;
    private readonly float fireRate = 0.1f;
    private readonly float damping = 10;

    public bool isFire = false;
   // public AudioClip fireSfx;

    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAttack();
    }
    //isFire면 애너미가 공격
    void EnemyAttack()
    {
        if (isFire)
        {
            //공격 후, time+딜레이+랜덤 딜레이 저장
            if (Time.time>= nextFire)
            {
                VirusAttack();
                nextFire = Time.time + fireRate + Random.Range(1, 5f);
            }
        }
        //플레이어를 바라보게 함. 시간에 따라 점진적으로 회전 시킴.
        Quaternion rot = Quaternion.LookRotation(playerTr.position - transform.position);
        enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
    }

    void VirusAttack()
    {
        anim.SetTrigger(hashFire);
        //_audio.PlayOneShot(fireSfx, 1.0f); //소리넣기
        //Attack
        Rigidbody rb = Instantiate(attackBullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();  //공격체 생성
        rb.AddForce(transform.forward * 32, ForceMode.Impulse);
        rb.AddForce(transform.up * 8, ForceMode.Impulse);
    }
}
