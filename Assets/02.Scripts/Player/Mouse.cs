using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AI;
/// <summary>
/// 플레이어 이동 감지
/// 하나의 동작 상태일 때 다른 동작 불가능
/// 스테미너를 사용한 달리기
/// 스테미너 회복
/// </summary>
public class Mouse : MonoBehaviour
{
 

    

    private Camera cam;
    public GameObject player; //이동 대상
    public NavMeshAgent agent;
    private Animator playerAnim;
    PlayerState playerState;  //플레이어 상태

    public Button runBtn;
    public GameObject clickEffect;  //클릭 지점
    

    
    public bool isMove=false;
    private Vector3 destination;

    private void Awake()
    {
        cam = Camera.main;
        playerAnim = GetComponentInChildren<Animator>();
        playerState = GetComponent<PlayerState>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
   

    void Update()
    {
        InputMouse();
        LookMoveDirection();
        



    }
    void InputMouse()
    {
        if ((Input.GetMouseButton(0)) )// && !isCrouch)
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                
                playerAnim.SetBool("IsWalk", true);
                RaycastHit hit;
                if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit) )
                {
                    SetDestination(hit.point);
                    playerState.isCrouch = false;
                }
                clickEffect.transform.position = hit.point;
                clickEffect.SetActive(true);
            }
           
        }
    }
    private void SetDestination(Vector3 dest)
    {
        agent.SetDestination(dest);
        destination = dest;
        isMove = true;
    }
    private void LookMoveDirection()
    {
        if (isMove)
        {
            if (agent.velocity.magnitude == 0)  //이동거리가 0이면
            {
                clickEffect.SetActive(false);  //클릭이펙트 끄기
                isMove = false;
                playerAnim.SetBool("IsWalk", false);
                return;
            }

            var dir = new Vector3(agent.steeringTarget.x,transform.position.y,agent.steeringTarget.z) - transform.position; //플레이어 높이보다 높은 곳 올라가기 방지
            player.transform.forward = dir;
            //transform.position += dir.normalized*Time.deltaTime*5;
        }
     
    }


}
