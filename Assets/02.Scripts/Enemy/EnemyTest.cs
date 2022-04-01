using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    Rigidbody rigid;
    BoxCollider boxCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {  
        Debug.Log(maxHealth);
        if (collision.gameObject.tag=="Weapon")
        {
                AttackCtrl attackCtrl = GetComponent<AttackCtrl>();
                curHealth -= attackCtrl.damage;
                Debug.Log("Melee :" + curHealth);
        }

    }
}
