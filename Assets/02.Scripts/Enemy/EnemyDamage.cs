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
