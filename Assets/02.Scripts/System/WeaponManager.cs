using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //무기 중복 교체 실행 방지
    public static bool isChangeWeapon=false;
    [SerializeField]
    private float changeWeaponDelayTime; //무기 교체 딜레이
    [SerializeField]
    private float changeWeaponEndDelayTime;

    

    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
