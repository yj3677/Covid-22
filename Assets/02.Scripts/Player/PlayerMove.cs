using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    static public PlayerMove instance; //값 공유
    [Header("---Running---")]
    public bool isRunning = false; //달리기중
    float originSpeed;
    public bool isCameraMoving;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private PlayerInput moveJoystick;
    [SerializeField]
    private PlayerInput camJoystick;

    public string currentMapName; //BossRoom스크립트에 있는 transferMapName변수값 저장
    public bool isMove;
    private PlayerShooter playerShooter;
    public NavMeshAgent navMesh;
    private PlayerState playerState;
    public PlayerInput playerInput;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        playerState = GetComponent<PlayerState>();
        playerShooter = FindObjectOfType<PlayerShooter>();
 
    }
    private void Start()
    {
        if (instance==null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            originSpeed = navMesh.speed; //플레이어 속도 초기값
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (playerState.isDead == true ||isCameraMoving)
        { 
            return;
        }
        Vector2 moveInput = new Vector2(moveJoystick.horizontal, moveJoystick.vertical);
        isMove = moveInput.magnitude != 0;

        if (playerState.isDead == false)
        {
            //Debug.Log(moveInput.magnitude);
            if (isMove)
            {
                Vector3 lookForward = new Vector3(cam.forward.x , 0, cam.forward.z ).normalized;
                Vector3 lookRight = new Vector3(cam.right.x , 0, cam.right.z ).normalized;
                Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;  //이동 방향

                player.forward = moveDir;  //방향 바라보기
                transform.localPosition += moveDir * Time.deltaTime * navMesh.speed;
                //걷기 & 달리기 애니메이션
                if (!isRunning)
                {
                    playerState.playerAnim.SetBool("IsWalk", isMove);
                    playerState.playerAnim.SetFloat("Speed", moveInput.magnitude);
                }
                else if (isRunning)
                {
                    playerState.playerAnim.SetBool("IsWalk", false);
                    playerState.playerAnim.SetBool("IsRun", true);
                }
            }
            //멈춘 상태에서는 애니메이션 정지
            else
            {
                playerState.playerAnim.SetBool("IsRun", false);
                playerState.playerAnim.SetBool("IsWalk", false);
            }
        }
    }

    public void RunOn() //버튼 누르면 달리기 , 스테미나 감소
    {
        if (playerState.isDead)
        {
            return;
        }
        //달리는 중, 스테미너=0, 앉은 상태가 아니라면
        if (!(isRunning) && playerState.stamina != 0 && !(playerState.isCrouch))
        {
            playerState.stamina -= 4;  //스테미너 4감소
            navMesh.speed = 9;    //플레이어 속도 9로 증가
            isRunning = true;   //달리는 상태로 변경
            Invoke("RunOff", 4);
        }      
    }
    void RunOff()
    { //달리는 상태에서 걷는 상태로 변경
        
        if (isRunning) //달리는 상태라면
        {
            navMesh.speed = originSpeed;
            playerState.playerAnim.SetBool("IsRun", false);
            isRunning = false;
        }
    }
}