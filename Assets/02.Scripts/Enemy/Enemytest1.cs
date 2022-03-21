using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemytest1 : MonoBehaviour
{
    //순찰 지점들을 저장하기 위한 List 타입 변수
    public List<Transform> wayPoint;
    //다음 순찰 지점 배열의 Index
    public int nextIdx;


    private NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        var group = GameObject.Find("WayPoints");
        if (group!=null)
        {
            group.GetComponentsInChildren<Transform>(wayPoint);
            wayPoint.RemoveAt(0);
        }
        MoveWayPoint();
    }
    private void Update()
    {
        //NavMeshAgent가 이동하고 있고 목적지에 도착했는지 여부 계산
        if (agent.velocity.sqrMagnitude>=0.2f*0.2f && agent.remainingDistance<=0.5f)
        {
            //다음 목적지의 배열 첨자를 계산
            nextIdx = ++nextIdx % wayPoint.Count; //random으로 바꾸기
            //다음 목적지로 이동 명령
            MoveWayPoint();
        }
    }
    private void MoveWayPoint()
    {
        if (agent.isPathStale)
        {
            return;
        }
        agent.destination = wayPoint[nextIdx].position;
        agent.isStopped = false;
        
    }
}
