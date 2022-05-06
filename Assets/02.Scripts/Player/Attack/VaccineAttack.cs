using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 사용안함
/// 백신 아이템 사용 후 일정 시간 동안 범위 내 적을 치료(공격)
/// 만약 인벤토리가 켜져 있다면 꺼지고 공격 실행
/// </summary>
public class VaccineAttack : MonoBehaviour
{
    EnemyDamage enemyDamage;

    public float searchTimer;
    public void DoVaccineAttack()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, 10);
        if (cols.Length>0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].tag=="Enemy")
                {
                    Debug.Log(cols[i]);
                    enemyDamage = FindObjectOfType<EnemyDamage>();
                    //enemyDamage.WeaponAttack();
                }
            }
        }
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("s");
    //    if (searchTimer<0.1)
    //    {
    //        searchTimer += Time.deltaTime;
    //    }
    //    if (other.gameObject.tag=="Enemy"&&searchTimer>=0.1)
    //    {
    //        enemyDamage = FindObjectOfType<EnemyDamage>();
    //        enemyDamage.WeaponAttack();
    //        searchTimer = 0;

    //    }
    //}
}
