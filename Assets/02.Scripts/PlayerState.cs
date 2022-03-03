using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerState : MonoBehaviour
{
    GameObject player;

    private void Awake()
    {
        player = GameObject.Find("CharacterMove");
        player.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
 
}
