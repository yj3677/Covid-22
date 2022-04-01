using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private string attackTag = "Weapon";
    [SerializeField]
    private float hp = 2;

    private GameObject healEffect; //치료이펙트 추가하기

    AttackCtrl attackDamage;
    void Start()
    {
        healEffect = Resources.Load<GameObject>("BloodSplat_FX");
        attackDamage = FindObjectOfType<AttackCtrl>();
    }
 
    void Update()
    {
        
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("Test");
    //    //무기(백신)에 공격(닿았을 때)받았을 때
    //    if (collision.collider.tag== attackTag)
    //    {
    //        Debug.Log("EnemyDamage");
    //        ShowBloodEffect(collision);
    //        //Destroy(collision.gameObject);
    //        //hp감소 
    //        hp -= collision.collider.GetComponent<AttackCtrl>().damage;
    //        Debug.Log(hp);

    //        //체력이 0이 되면 에너미 상태를 DIE로 전환
    //        if (hp <= 0)
    //        {
    //            GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
    //        }
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Weapon")
        {
            Debug.Log(hp);
            hp -= attackDamage.damage;
            //체력이 0이 되면 에너미 상태를 DIE로 전환
            if (hp <= 0)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
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
