using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public int damage;
    private Animator anim;
    private Transform playerTr;
    private Transform enemyTr;
    private PlayerState playerState;


    public GameObject attackBullet;  //공격 수단

    private AudioSource _audio;
    public MeshRenderer muzzleFlash;

    private readonly int hashFire = Animator.StringToHash("Fire");

    private float nextFire = 0;
    private readonly float fireRate = 0.1f;
    private readonly float damping = 10;

    public bool isFire = false;
   // public AudioClip fireSfx;

    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        playerTr = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyTr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        muzzleFlash.enabled = false;
    }

    
    void Update()
    {
        EnemyAttack();
    }
    //isFire면 애너미가 공격
    void EnemyAttack()
    {
        //if (playerState.isDead) //플레이어 죽었을 때 공격 안함
        //{
        //    return;
        //}
        if (isFire)
        {
            //공격 후, time+딜레이+랜덤 딜레이 저장
            if (Time.time>= nextFire)
            {
                VirusAttack();
                nextFire = Time.time + fireRate + Random.Range(1, 5f);
            }
            //플레이어를 바라보게 함. 시간에 따라 점진적으로 회전 시킴.
            Quaternion rot = Quaternion.LookRotation(playerTr.position - transform.position);
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
       
    }

    void VirusAttack()
    {
        anim.SetTrigger(hashFire);
        StartCoroutine(ShowMuzzleFlash()); //effect
        //_audio.PlayOneShot(fireSfx, 1.0f); //소리넣기
        //Attack
        Rigidbody rb = Instantiate(attackBullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();  //공격체 생성
        rb.AddForce(transform.forward * 10, ForceMode.Impulse);
        rb.AddForce(transform.up * 5, ForceMode.Impulse);
    }

    //effect
    IEnumerator ShowMuzzleFlash()
    {
        muzzleFlash.enabled = true;
        //z축 랜덤 회전
        Quaternion rot = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        muzzleFlash.transform.localRotation = rot;
        muzzleFlash.transform.localScale = Vector3.one * Random.Range(1, 2);

        Vector2 offset = new Vector2(Random.Range(0,2),Random.Range(0,2))*0.5f;
        muzzleFlash.material.SetTextureOffset("_MainTex", offset); //effect

        yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        muzzleFlash.enabled = false;
    }
}
