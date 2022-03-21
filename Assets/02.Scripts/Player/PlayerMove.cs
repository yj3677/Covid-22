using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{ 
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private InputTest moveJoystick;
    [SerializeField]
    private InputTest camJoystick;
    public int speed;

    private bool isMove;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

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
        Vector2 moveInput = new Vector2(moveJoystick.horizontal, moveJoystick.vertical);
        isMove = moveInput.magnitude != 0;
        anim.SetBool("IsWalk", isMove);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cam.forward.x * (-1), 0, cam.forward.z * (-1)).normalized;
            Vector3 lookRight = new Vector3(cam.right.x * (-1), 0, cam.right.z * (-1)).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;  //이동 방향

            player.forward = moveDir;  //방향 바라보기
            transform.localPosition += moveDir * Time.deltaTime * speed;
        }
    }


    public void LookAround(Vector3 inputDirection) //카메라 방향
    {
        Vector2 mouseDelta = inputDirection;
        Vector3 camAngle = cam.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;
        if (x < 180)
        {
            x = Mathf.Clamp(x, -70, -1);  //위쪽 방향 제한
        }
        else
        {
            x = Mathf.Clamp(x, 359, 359); //아래쪽 방향 제한
        }
        cam.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);  //카메라 회전
    }


}