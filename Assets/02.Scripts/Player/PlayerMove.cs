using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [Header("---Running---")]
    public bool isRunning = false; //달리기중
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private PlayerInput moveJoystick;
    [SerializeField]
    private PlayerInput camJoystick;
    

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
        
    }
    private void Update()
    {
        Move();
    }


    public void Move()
    {
        if (playerState.isDead == true)
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


    //public void LookAround(Vector3 inputDirection) //카메라 방향
    //{
    //    Vector2 mouseDelta = inputDirection;
    //    Vector3 camAngle = cam.rotation.eulerAngles;

    //    float x = camAngle.x - mouseDelta.y;
    //    if (x < 180)
    //    {
    //        x = Mathf.Clamp(x, -70, -1);  //위쪽 방향 제한
    //    }
    //    else
    //    {
    //        x = Mathf.Clamp(x, 359, 359); //아래쪽 방향 제한
    //    }
    //    cam.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);  //카메라 회전
    //}

    public void RunOn() //버튼 누르면 달리기 , 스테미나 감소
    {
        if (playerState.isDead)
        {
            return;
        }
        if (!(isRunning) && playerState.stamina != 0 && !(playerState.isCrouch))
        {
            playerState.stamina -= 4;
            navMesh.speed = 9;
            
            isRunning = true;
            Invoke("RunOff", 4);

        }
        
    }
    void RunOff()
    {
        float originSpeed = navMesh.speed;
        if (navMesh.speed == 9)
        {
            navMesh.speed = 5;
            playerState.playerAnim.SetBool("IsRun", false);
            isRunning = false;
        }
    }
}