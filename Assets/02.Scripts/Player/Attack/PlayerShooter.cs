using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// gun에 따라 무기 교체 구현하기
/// 
/// </summary>
public class PlayerShooter : MonoBehaviour
{
    WeaponManager weaponManager;
    public GameObject gunWeapon; //백신무기
    PlayerState playerState;
    public GunData gunData; //총 데이터
    public GameObject bullet; //백신 총알
    public Transform firePos;  //총알 발사 위치

    public float attackTime; //발사 시간
    [SerializeField]
    private float fireDelay;  //발사 딜레이
    public bool isFireReady;  //총을 쏠 준비
    public bool isReadyAttack;  //공격 여부
    public bool isReload = false;  //재장전
    public int currBullet; //현재 탄창에 있는 총알 개수
    public int remainBullet; //현재 소유하고 있는 전체 총알 개수  
    public int bulletCapacity=20; //탄창 용량

    public bool isActive=true; //무기교체 후


    Vector3 SpawnWeapon;

    private void Awake()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        playerState = FindObjectOfType<PlayerState>();
    }

    private void Start()
    {
        gunData.weaponType = GunData.WeaponType.Gun;
        remainBullet = gunData.remainBullet; //전체 탄알양 초기화
        currBullet = bulletCapacity; //현재 탄창 가득 채우기
        fireDelay = 0;
        transform.localPosition = SpawnWeapon;

    }
    private void Update()
    {
        if (isActive)
        {
            fireDelay += Time.deltaTime;
        }

    }

    void isFireReadyDelay()
    {
        isFireReady = false;
        isReadyAttack = false;
    }
  
    public void Fire()
    {//무기&총알X , 중복공격 방지, 사망, 재장전 중일때는 실행X
        if (weaponManager.currentWeaponType == "Mlee"|| isReadyAttack || playerState.isDead ||isReload || currBullet <= 0 || playerState.isCrouch||!isActive) 
        {
            Debug.Log("공격 불가");
            return;
        }
        if (attackTime < fireDelay)
        {
            isFireReady = true;
        }

        //공격준비 완료, 앉아 있는 상태가 아니라면
        if (isFireReady && !playerState.isCrouch &&!isReload &&isActive)
        {
            playerState.playerAnim.SetTrigger("doFire"); //*플레이어 공격 애니메이션 넣기
            currBullet--;
            Instantiate(bullet, firePos.position, firePos.rotation);
            fireDelay = 0;
            Debug.Log("Fire");
            Invoke("isFireReadyDelay", 0.9f);

        }
        //탄창 총알이 0이고 현재 소유한 총알 전체 개수가 0보다 클때
        if (currBullet == 0 && remainBullet>0) 
        {
            isReload = true;
        }
        //isReload = (--gunData.currBullet % gunData.maxBullet == 0);
        if (isReload)
        {
            if (isActive)
            {
                StartCoroutine(Reloading());
            }

        }
    }

    IEnumerator Reloading()
    {
        //만약 소유한 전체 총알 개수가 재장전 해야할 개수보다 많다면
        if (remainBullet>=gunData.reloadBulletCount)
        {
            currBullet = gunData.reloadBulletCount; //탄창에 재장전 개수만큼 넣기
            remainBullet -= gunData.reloadBulletCount; //소유한 총알 개수를 재장전한 만큼 빼기 
        }
        else
        {
            currBullet = remainBullet;
            remainBullet = 0;
        }
        playerState.playerAnim.SetTrigger("Reload");
        yield return new WaitForSeconds(2);
        isReload = false;
    }
    public void CancleReload()
    {
        if (isReload)
        {
            StopAllCoroutines();
            isReload = false;
        }
    }
 
}
