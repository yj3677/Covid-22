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
    [SerializeField]
    private float damage;
    public float rate;
    float fireDelay;
    bool isFireReady;

    Vector3 SpawnWeapon;


    public GameObject equipWeapon; //백신무기
    PlayerState playerState;
    public Animator playerAnim;
    public BoxCollider meleeArea; //근접공격범위
    public TrailRenderer trailEffect;

    private void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();
    }
    private void OnEnable()
    {
        //weapon 생성 위치
        SpawnWeapon = new Vector3(1.44f, -0.38f, 0.15f);
    }
    private void Start()
    {
        transform.localPosition = SpawnWeapon;
    }
 
    public void WeaponUse()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing"); 
    }

    public void Attack()
    {
        if (equipWeapon==null)
        {
            return;
        }
        fireDelay += Time.deltaTime;
        isFireReady = rate < fireDelay;

        //공격준비 완료, 앉아 있는 상태가 아니라면
        if (isFireReady && !playerState.isCrouch)
        {
            WeaponUse();
            playerAnim.SetTrigger("doSwing"); //*플레이어 공격 애니메이션 넣기
            fireDelay = 0;
        }
    }

    IEnumerator Swing()
    {
        
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;
        

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
}
