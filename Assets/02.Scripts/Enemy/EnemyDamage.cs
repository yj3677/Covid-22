using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    private string attackTag = "Weapon";
    [SerializeField]
    private float startHp = 2;  //시작체력
    [SerializeField]
    private float currenthp; //현재체력

    private GameObject healEffect; //치료이펙트 추가하기

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);

    private Canvas uiCanavas;
    private Image hpBarImage;

    AttackCtrl attackDamage;
    void Start()
    {
        currenthp = startHp;
        healEffect = Resources.Load<GameObject>("BloodSplat_FX");
        attackDamage = FindObjectOfType<AttackCtrl>();
        SetHpBar();
    }
 
    void Update()
    {
        
    }
    void SetHpBar()
    {
        uiCanavas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanavas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var hpbar = hpBar.GetComponent<EnemyHpBar>();
        hpbar.targetTr = this.gameObject.transform;
        hpbar.offset = hpBarOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Weapon")
        {
            Debug.Log(currenthp);
            currenthp -= attackDamage.damage;
            hpBarImage.fillAmount = currenthp / startHp;
            //체력이 0이 되면 에너미 상태를 DIE로 전환
            if (currenthp <= 0)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;

                //적 캐릭터 사망 횟수를 누적시키는 함수 호출
                GameManager.instance.IncKillCount();
                //Capsule Collider 컴포넌트 비활성화
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }


    private void ShowBloodEffect(Collision collision)
    {
        Vector3 pos = collision.contacts[0].point;
        Vector3 _normal = collision.contacts[0].normal;
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);

        GameObject blood = Instantiate<GameObject>(healEffect, pos, rot);
        Destroy(blood, 1);
    }
}
