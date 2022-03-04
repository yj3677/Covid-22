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
    [Header("---Running---")]
    public int stamina;
    public bool isRunning = false; //달리기중
    [Header("---Crouch---")]
    float recoveryTime = 0;
    public bool isCrouch = false;

    

    private Camera cam;
    public GameObject player; //이동 대상
    private NavMeshAgent agent;
    private Animator playerAnim;

    public Button runBtn;
    public GameObject clickEffect;  //클릭 지점
    

    
    private bool isMove=false;
    private Vector3 destination;

    private void Awake()
    {
        cam = Camera.main;
        playerAnim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;


    }
    private void FixedUpdate()
    {
        Stamina();
        
    }

    void Update()
    {
        InputMouse();
        LookMoveDirection();
        Recovery();
        
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
                    isCrouch = false;
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

    public void Crouch()
    {
        if (!isRunning)
        {
            isCrouch = !isCrouch;

            if (isCrouch)
            {
                //앉기 애니매이션 넣기    


            }
            else
            {
                //일어서기 애니매이션 넣기
            }
        }
      
       
    }
    void Stamina()
    {
        if (stamina<=0)
        {
            stamina = 0;
        }
        if(stamina>=100)
        {
            stamina = 100;
        }
    }
    public void Recovery() //1프레임에 스테미너 1회복
    {
        
        //crouch()넣기
        if (stamina>=100) //스테미너 100이상 리턴
        {
            recoveryTime = 0;
            return;
        }
        
        else if(isCrouch&&!isMove) //앉은 상태에서만 회복
        {
            recoveryTime += Time.fixedDeltaTime;
            if (recoveryTime > 3)
            {
                Debug.Log(recoveryTime);
                stamina += 1;
                recoveryTime = 0;
            }
           
        }
        
        
    }
    
    public void RunOn() //버튼 누르면 달리기 , 스테미나 감소
    {
        if (!isRunning && stamina!=0 && !isCrouch)
        {
            stamina -= 4;
            agent.speed = 9;
            isRunning = true;
            Invoke("RunOff", 4);
        }
        
    }
    void RunOff()
    {
        float originSpeed = agent.speed;
        if (agent.speed == 9)
        { 
            agent.speed =  5;
            isRunning = false;
        }
    }
    
}
