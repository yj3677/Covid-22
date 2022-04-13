using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 총알구현 바꿔야함
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePos;

    public int damage;
    public float attackTime;
    [SerializeField]
    private float fireDelay;
    public bool isFireReady;
    public bool isReadyAttack;

    Vector3 SpawnWeapon;

    private float reloadTime = 2; //장전시간
    [SerializeField]
    private int maxBullet = 30;  //최대 소지 총알
    [SerializeField]
    private int currBullet = 10; //현재 총알
    [SerializeField]
    private int remainBullet = 10;
    private bool isReload = false;

    public GameObject equipWeapon; //백신무기
    PlayerState playerState;
    Animator playerAnim;


    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        playerState = FindObjectOfType<PlayerState>();
    }

    private void Start()
    {
        transform.localPosition = SpawnWeapon;
    }
    private void Update()
    {
        fireDelay += Time.deltaTime;
    }

    void isFireReadyDelay()
    {
        isFireReady = false;
        isReadyAttack = false;
    }
  
    public void Fire()
    {//무기&총알X , 중복공격 방지, 사망, 재장전 중일때는 실행X
        if (equipWeapon == null || isReadyAttack || playerState.isDead ||isReload ||currBullet<=0) 
        {
            return;
        }
        if (attackTime < fireDelay)
        {
            isFireReady = true;
        }
        //공격준비 완료, 앉아 있는 상태가 아니라면
        if (isFireReady && !playerState.isCrouch &&!isReload)
        {
            playerAnim.SetTrigger("doFire"); //*플레이어 공격 애니메이션 넣기
            Instantiate(bullet, firePos.position, firePos.rotation);
            fireDelay = 0;
            Debug.Log("Fire");
            Invoke("isFireReadyDelay", 0.9f);

        }
        isReload = (--currBullet % maxBullet == 0);
        if (isReload)
        {
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        playerAnim.SetTrigger("Reload");
        yield return new WaitForSeconds(2);
        currBullet = maxBullet;
        //총알구현 바꾸기
        isReload = false;

    }
}
