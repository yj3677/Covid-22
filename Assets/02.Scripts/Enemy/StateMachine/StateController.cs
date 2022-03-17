using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// staate->actions update->transition(decision)
/// state에 필요한 기능들. 애니메이션 콜백들.
/// 시야 체크, 찾아놓은 엄폐물 장소 중 가장 가까운 위치를 찾는 기능
/// </summary>
public class StateController : MonoBehaviour
{
    public GeneralStats generalStats;
    public string classID;

    public State currenState;
    public State remainState;

    public Transform aimTarget;
    public List<Transform> patrolWaypoints;
    public int bullets;
    [Range(0,50)]
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;
    [Range(0,25)]
    public float perceptionRadius;


    [HideInInspector] public float nearRadius;
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public int wayPointIndex;
    [HideInInspector] public int maximumBurst = 7;
    [HideInInspector] public float blindEngageTime = 30;
    [HideInInspector] public bool targetInsight;
    [HideInInspector] public bool focusSight;
    [HideInInspector] public bool reloading;
    [HideInInspector] public bool hadClearShot;
    [HideInInspector] public bool haveClearShot;
    [HideInInspector] public int coverHash=-1;

    [HideInInspector] public EnemyVariables variables;
    [HideInInspector] public Vector3 personalTarget = Vector3.zero;

    private int magBullets;
    private bool aiActive; //사망
    private static Dictionary<int, Vector3> coverSpot;
    private bool strafing;
    private bool aiming;
    private bool checkedOnLoop, blockedSight;

    [HideInInspector] public EnemyAnimation enemyAnimation;
    [HideInInspector] public CoverLookUp coverLookUp;

    public Vector3 CoverSpot
    {
        get { return coverSpot[this.GetHashCode()]; }
        set { coverSpot[this.GetHashCode()] = value; }
    }
    public void TransitionToState(State nextState, Decision decision)
    {
        if (nextState!=remainState)
        {
            currenState = nextState;
        }
    }
}
