using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 공격 버튼을 눌렀을 때 공격 실행
/// </summary>
public class AttackCtrl : MonoBehaviour
{
    [SerializeField]
    private float damage;
    public float rate;
    float fireDelay;
    bool isFireReady;

    GameObject equipWeapon;
    PlayerState playerState;
    public Animator playerAnim;
    public BoxCollider meleeArea; //근접공격범위
    public TrailRenderer trailEffect;

    private void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();
    }
    public void WeaponUse()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing"); 
    }

    void Attack()
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
