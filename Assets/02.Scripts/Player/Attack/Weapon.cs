using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //무기
    GameObject nearObject;
    public GameObject weapon;
    public bool hasWeapon;

    PlayerState playerState;

    private void Awake()
    {
        playerState = FindObjectOfType<PlayerState>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = other.gameObject;
            Debug.Log(nearObject.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            nearObject = null;
        }
    }
   
}
