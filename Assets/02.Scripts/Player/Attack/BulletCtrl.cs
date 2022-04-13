using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 원거리 공격
/// 어택 버튼을 눌렀을 때 어택 실행(백신)
/// 한번 공격하면 백신 아이템-1
/// </summary>
public class BulletCtrl : MonoBehaviour
{
    public float damage = 2;
    public float speed = 1000;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        //애너미에게 닿으면 총알 제거
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else
        {
            Invoke("BulletRemove", 1.3f);
        }
    }
 
    void BulletRemove()
    {
        //총알 제거
        Destroy(gameObject);
    }
}
