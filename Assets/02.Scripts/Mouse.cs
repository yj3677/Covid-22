using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Camera cam;
    public GameObject player;

    private bool isMove;
    private Vector3 destination;

    private void Awake()
    {
        cam = Camera.main;

    }
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition),out hit))
            {
                SetDestination(hit.point);
            }
        }
        Move();
    }
    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
    }
    private void Move()
    {
        if (isMove)
        {
            var dir = destination - transform.position;
            player.transform.forward = dir;
            transform.position += dir.normalized*Time.deltaTime*5;
        }
        if (Vector3.Distance(transform.position, destination) <= 0.1f)
        {
            isMove = false;
        }
    }
}
