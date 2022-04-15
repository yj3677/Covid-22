using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 근접 공격
/// 어택 버튼을 눌렀을 때 어택 실행(백신)
/// 한번 공격하면 백신 아이템-1
/// </summary>
public class AttackCtrl : MonoBehaviour
{
    public int damage;
    public float rate;
    [SerializeField]
    private float attackDelay;
    public bool isFireReady;
    public bool isReadyAttack;

    Vector3 SpawnWeapon;


    public GameObject equipWeapon; //백신무기
    PlayerState playerState;
    Animator playerAnim;
    public BoxCollider meleeArea; //근접공격범위
    public TrailRenderer trailEffect;

    private void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        playerState = FindObjectOfType<PlayerState>();
    }
    private void OnEnable()
    {
        //weapon 생성 위치
        SpawnWeapon = new Vector3(0.113f, 0.053f, -0.081f);
    }
    private void Start()
    {
        transform.localPosition = SpawnWeapon;
    }
    private void Update()
    {
        attackDelay += Time.deltaTime;
    }

    public void WeaponUse()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing"); 
    }

    public void Attack()
    {
        if (equipWeapon==null || isReadyAttack || playerState.isDead) //무기X OR 중복공격 방지
        {
            return;
        }
        Debug.Log(attackDelay);
        if(rate < attackDelay)
        {
            isFireReady = true;
        }  
        //공격준비 완료, 앉아 있는 상태가 아니라면
        if (isFireReady && !playerState.isCrouch)
        {
            isReadyAttack = true;
            Debug.Log("Attack");
            WeaponUse();
            playerAnim.SetTrigger("doSwing"); //*플레이어 공격 애니메이션 넣기
            attackDelay = 0;
            Invoke("isFireReadyDelay", 0.9f);
        }
    }

    void isFireReadyDelay()
    {
        isFireReady = false;
        isReadyAttack = false;
    }

    IEnumerator Swing()
    {   
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.5f);
        meleeArea.enabled = false;
        
        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
}
