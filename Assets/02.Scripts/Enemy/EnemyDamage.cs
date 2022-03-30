using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private string bulletTag = "Bullet";
    private float hp = 2;

    private GameObject healEffect; //치료이펙트 추가하기
    void Start()
    {
        healEffect = Resources.Load<GameObject>("BloodSplat_FX");
    }

    
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag==bulletTag)
        {
            ShowBloodEffect(collision);
            Destroy(collision.gameObject);
            //플레이어에게 공격 받았을 때 hp감소 구현
            //hp -= collision.gameobject.GetComponent<AttackCtrl>().damage;
            if (hp<=0)
            {
                GetComponent< EnemyAI >().state = EnemyAI.State.DIE;
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
