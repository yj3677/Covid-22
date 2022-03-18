using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemytest1 : MonoBehaviour
{
    [SerializeField] EnemyTest enemy = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            enemy.SetTarget(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemy.RemoveTarget();
        }
    }
}
