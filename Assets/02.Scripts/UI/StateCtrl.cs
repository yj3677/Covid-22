using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateCtrl : MonoBehaviour
{ 
    //체력
    private int currentHp;
  
    //스테미너
    private int currentSt;

    //배고픔
    private int currentHungry;

    //목마름
    private int currentThirsty;


    [SerializeField] //피곤함
    private int tired;
    private int currentTired;

    public Image Image_gauges1;


    Mouse mouse;

   
    private void Awake()
    {
        mouse = FindObjectOfType<Mouse>();
    }
    private void Start()
    {
      

    }


    void Update()
    {
        GaugeUpdate();
    }
  
  
    private void GaugeUpdate()
    {  
      
    }
}
