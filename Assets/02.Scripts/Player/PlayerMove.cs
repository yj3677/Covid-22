using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{ 
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform cam;
    public int speed;

    private bool isMove;

 
    Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();

    }
    private void Start()
    {
        
    }

    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection;
        isMove = moveInput.magnitude != 0;
        anim.SetBool("IsWalk", isMove);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cam.forward.x, 0, cam.forward.z).normalized;
            Vector3 lookRight = new Vector3(cam.right.x, 0, cam.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;  //이동 방향

            player.forward = moveDir;  //방향 바라보기
            transform.position += moveDir * Time.deltaTime * speed;
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