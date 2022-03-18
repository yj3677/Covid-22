using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    NavMeshAgent enemy = null;
    LayerMask isPlayer;
    [SerializeField] Transform[] WayPoints = null;
    int count = 0;

    public bool playerInSightRange;
    public float sightRange;

    //target
    Transform playertarget = null;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        InvokeRepeating("MoveToNextWayPoint", 0, 2);
       
    }


    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);  //시야범위
        Debug.Log(playerInSightRange);
        if (playertarget !=null)
        {
            enemy.SetDestination(playertarget.position);
        }
    }
    public void SetTarget(Transform target)
    {
        CancelInvoke();
        target = playertarget;
    }
    public void RemoveTarget()
    {
        playertarget = null;
        InvokeRepeating("MoveToNextWayPoint", 0, 2);
    }

    void MoveToNextWayPoint()
    {
        if (playertarget==null)
        {
            if (enemy.velocity == Vector3.zero)
            {
                enemy.SetDestination(WayPoints[count++].position);
                if (count >= WayPoints.Length)
                {
                    count = 0;
                }
            }
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        SetTarget(collision.transform);
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        RemoveTarget();
    //    }
    //}
}
