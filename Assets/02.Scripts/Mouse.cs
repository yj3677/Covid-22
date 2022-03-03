using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mouse : MonoBehaviour
{
    private Camera cam;
    public GameObject player;
    private NavMeshAgent agent;
    private Animator playerAnim;

    private bool isMove;
    private Vector3 destination;

    private void Awake()
    {
        cam = Camera.main;
        playerAnim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

    }

    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            playerAnim.SetBool("IsWalk", true);
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hit))
            {
                SetDestination(hit.point);
            }
        }
        LookMoveDirection();
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
            if (agent.velocity.magnitude == 0)
            {
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
