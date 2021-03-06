using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMove : MonoBehaviour
{
    //순찰 지점들을 저장하기 위한 List 타입 변수
    public List<Transform> wayPoint;
    //다음 순찰 지점 배열의 Index
    public int nextIdx;

    private PlayerState playerState;
    public NavMeshAgent agent;
    private Transform enemyTr;
    private Boss bossAI;
    public BossAttack bossAttack;

    [SerializeField]
    public readonly float patrollSpeed = 1.5f;
    [SerializeField]
    private readonly float traceSpeed = 4;
    [SerializeField]
    private float damping = 1;

    private bool _patrolling;  //순찰 여부 판단
    public bool patrolling
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = patrollSpeed;
                //순찰 상태의 회전계수
                damping = 1;
                MoveWayPoint();
            }
        }
    }

    private Vector3 _traceTarget;//추적 대상의 위치를 저장하는 변수
    public Vector3 traceTarget
    {
        get { return _traceTarget; }
        set
        {
            _traceTarget = value;
            agent.speed = traceSpeed;
            //추적상태의 회전계수
            damping = 7;
            TraceTarget(_traceTarget);
        }
    }
    public float speed
    {
        get { return agent.velocity.magnitude; }

    }

    void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale)
        {
            return;
        }
        agent.destination = pos;
        agent.isStopped = false;
    }
    void OnEnable()
    {
        //agent.enabled = true;
    }
    private void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();
        //bossAttack = GetComponent<BossAttack>();
        enemyTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        //agent.enabled = false;
        bossAI = GetComponent<Boss>();
    }
    private void Start()
    {
        //agent.enabled = true;
        agent.autoBraking = false;
        //자동으로 회전하는 기능 비활성화
        agent.updateRotation = false;
        agent.speed = patrollSpeed;

        var group = GameObject.Find("WayPoints");
        if (group != null)
        {
            group.GetComponentsInChildren<Transform>(wayPoint);
            wayPoint.RemoveAt(0);

            nextIdx = Random.Range(0, wayPoint.Count);
        }
        MoveWayPoint();
    }
    private void Update()
    {
        //사망상태에는 실행X
        if (bossAI.state == Boss.State.DIE)
        {
            //죽었을때 높이 조절
            bossAI.animator.applyRootMotion = true;
            return;
        }
        //Debug.Log(agent.velocity.magnitude);
        if (agent.isStopped == false)
        {
            //애너미가 가야할 방향 벡터를 쿼터니언 타입 각도로 변환
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            //보간 함수를 사용해 점진적으로 회전시킴
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
        if (!_patrolling)
        {
            return;
        }

        //NavMeshAgent가 이동하고 있고 목적지에 도착했는지 여부 계산
        if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
        {
            //다음 목적지로 랜덤 이동
            nextIdx = Random.Range(0, wayPoint.Count);
            //다음 목적지로 이동 명령
            MoveWayPoint();
        }
    }
    //표시해둔 영역 찾아가기
    private void MoveWayPoint()
    {
        if (agent.isPathStale)
        {
            //플레이어 죽었을 때 애너미 멈추는 애니메이션 넣기
            return;
        }
        agent.destination = wayPoint[nextIdx].position;
        agent.isStopped = false;
    }
    public void StopEnemy()
    {
        //애너미가 죽었을 때 실행
        bossAttack.isFire = false; //공격 멈춤
        agent.isStopped = true;
        agent.velocity = Vector3.zero;  //정지. 속도0
        _patrolling = false;  //순찰정지
    }
}
