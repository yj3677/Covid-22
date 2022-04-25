using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 무기 타입, 이름에 따라 무기 교체
/// </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private AnimatorOverrideController[] overrideControllers;
 

    public static Transform currentWeapon;  //다른 스크립트에서 접근이 용이하게
    //무기 중복 교체 실행 방지
    public static bool isChangeWeapon=false;  //무기 교체중인지


    [SerializeField]
    private float changeWeaponDelayTime; //무기 교체 딜레이
    [SerializeField]
    private float changeWeaponEndDelayTime;  //무기 교체 후 딜레이
    [SerializeField]
    public string currentWeaponType; //현재 무기의 타입.

    public GameObject weaponGun; //총
    public GameObject gunButton; //총 버튼
    public GameObject Weaponmlee; //근접무기
    public GameObject mleeButton; //근접무기 버튼



    private AttackCtrl attackCtrl; //근접 공격
    private PlayerShooter shootCtrl; //원거리 공격
    private PlayerState playerState;
    private PlayerMove playermove;

    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        attackCtrl = FindObjectOfType<AttackCtrl>();
        shootCtrl = FindObjectOfType<PlayerShooter>();
        playermove = FindObjectOfType<PlayerMove>();
        currentWeaponType = "Melee";
        SetAnimations(overrideControllers[0]);
    }

    void Update()
    {
        
    }
    //무기 교체 버튼을 눌렀을 때 무기 교체 실행
    public void WeaponChangeButton()
    {
        //if (GameManager.instance.isGameOver) //게임 오버 상태, 앉아 있는 상태, 달리는 상태가 아니라면
        //{
        //    return;
        //}
        
            Debug.Log("Test");
            if (!isChangeWeapon) //무기 교체중이 아니라면
            {
                StartCoroutine(ChangeWeaponCoroutine(currentWeaponType));
            }
            else
            {

            }
        
 
        
    }
    void WeaponChange()
    {
        //총 버튼 눌렀을 때 번갈아 가면서 무기 변경
        //총,앉기,재장전, 총 쏘는 중이 아니라면
        //if (!playerState.isCrouch && !shootCtrl.isReload && !shootCtrl.isFireReady)
        {
            //shootCtrl.isActive = false;
            Debug.Log("c");
            //if (shootCtrl.isReload)
            //{
            //    shootCtrl.CancleReload(); //재장전 중일 때는 재장전 취소
            //}
            Debug.Log("d");
            //근접 무기를 들고 있다면
            if (currentWeaponType == "Melee")
            {
                Debug.Log("e");
                Weaponmlee.gameObject.SetActive(false);
                mleeButton.gameObject.SetActive(false);
                weaponGun.gameObject.SetActive(true);
                gunButton.gameObject.SetActive(true);
                currentWeaponType = "Gun"; //타입이 총으로 변경
                SetAnimations(overrideControllers[1]);
            }
            //총을 들고 있다면
            else if (currentWeaponType == "Gun")
            {
                Debug.Log("f");
                weaponGun.gameObject.SetActive(false);
                gunButton.gameObject.SetActive(false);
                Weaponmlee.gameObject.SetActive(true);
                mleeButton.gameObject.SetActive(true);
                currentWeaponType = "Melee";  //타입이 근접 무기로 변경
                SetAnimations(overrideControllers[0]);
            }
        }
    }
    public void SetAttackType(int value)
    {
        if (currentWeaponType == "Gun")
        {
            SetAnimations(overrideControllers[1]);
        }
        else if(currentWeaponType == "Melee")
        {
            SetAnimations(overrideControllers[0]);
        }
        
    }
    public void SetAnimations(AnimatorOverrideController overrideController)
    {
        playerState.playerAnim.runtimeAnimatorController = overrideController;
    }


    public IEnumerator ChangeWeaponCoroutine(string currentWeaponType)
    {
        isChangeWeapon = true;  //무기교체중
        playerState.playerAnim.SetTrigger("doChangeWeapon");  //무기교체 애니메이션
        yield return new WaitForSeconds(changeWeaponDelayTime); //무기교체 딜레이 
        CanclePreWeaponAction();
        WeaponChange();  //무기 교체 실행
        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        isChangeWeapon = false;
    }

    private void CanclePreWeaponAction()
    {
        switch(currentWeaponType)
        {
            case "Gun":
               // shootCtrl.CancleReload(); //재장전 중일 때는 재장전 취소
               // shootCtrl.isActive=false;
                break;
            case "Hand":
                break;
        }
    }
}
