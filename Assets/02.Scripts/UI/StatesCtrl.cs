using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatesCtrl : MonoBehaviour
{
    [SerializeField]
    private int hp;
    private int currentHp;
    [SerializeField]
    private int sp;
    private int currentSp;

    [SerializeField]
    private int spIncreaseSpeed; //스테미나 증가량
    [SerializeField]
    private int spRechargeTime; //회복딜레이

    private bool spUsed; //스테미나 감소 여부
    [SerializeField] //배고픔
    private int hungry;
    private int currentHungry;
    [SerializeField] //배고픔 줄어드는 속도
    private int hungryDecreaseTime;
    private int currentHungryDecreaseTime;

    [SerializeField] //목마름
    private int thirsty;
    private int currentThirsty;
    [SerializeField] //목마름 줄어드는 속도
    private int thirstyDecreaseTime;
    private int currentThirstyDecreaseTime;

    [SerializeField] //피곤함
    private int tired;
    private int currentTired;
    [SerializeField] //피곤함 줄어드는 속도
    private int tiredDecreaseTime;
    private int currentTiredDecreaseTime;

    [SerializeField]
    private Image[] gauges;

    private const int HP = 0, SP = 1, HUNGRY = 2, THIRSTY = 3, TIRED = 4;
    void Start()
    {
        currentHp = hp;
        currentSp = sp;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentTired = tired;

    }

    
    void Update()
    {
        Hungry();
        Thirsty();
        GaugeUpdate();
    }
    private void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
            {
                currentHungryDecreaseTime++;
            }
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else
            Debug.Log("배고픔 수치가 0이 되었습니다");
    }
    private void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
            {
                currentThirstyDecreaseTime++;
            }
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else
            Debug.Log("배고픔 수치가 0이 되었습니다");
    }
    private void GaugeUpdate()
    {
        gauges[HP].fillAmount = (float)currentHp / hp;
        gauges[SP].fillAmount = (float)currentSp / sp;
        gauges[HUNGRY].fillAmount = (float)currentHungry / hungry;
        gauges[TIRED].fillAmount = (float)currentTired / tired;
        gauges[THIRSTY].fillAmount = (float)currentThirsty / thirsty;
    }
}
