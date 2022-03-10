using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 캐릭터 상태 UI
/// 체력, 스테미너, 배고픔, 목마름, 피곤함
/// </summary>

public class PlayerState : MonoBehaviour
{
    [Header("---Running---")]
    public bool isRunning = false; //달리기중

    [Header("---Crouch---")]
    float recoveryTime = 0;
    public bool isCrouch = false;
    //체력
    private int currentHp;

    //스테미너
    public int stamina=10;
    private int currentSt;

    //배고픔
    public int hungry;
    [SerializeField]
    private int currentHungry;
    private float hungryDecreaseTime=2; 
    private float currentHungryDecreaseTime;

    //목마름
    public int thirsty;
    [SerializeField]
    private float currentThirsty;
    private int thirstyDecreaseTime = 2;
    private float currentThirstyDecreaseTime;

     //피곤함
    public int tired;
    [SerializeField]
    private float currentTired;
    private int tiredDecreaseTime = 2;
    private float currentTiredDecreaseTime;
    //배열로 구현하기
    public Image Image_gauges1;
    public Image Image_gauges2;
    public Image Image_gauges3;
    public Image Image_gauges4;

    Mouse player;

    private void Awake()
    {
        player = FindObjectOfType<Mouse>();

    }
    private void Start()
    {
        currentSt = stamina;
        currentHungry = hungry;
        currentThirsty = thirsty;
        currentTired = tired;
    }

    private void FixedUpdate()
    {
        Stamina();
        
    }
    void Update()
    {
        Recovery();
        Hungry();
        Thirsty();
        Tired();
        GaugeUpdate();
    }

    void Hungry()
    {
        if (currentHungry > 0)
        {
            if (currentHungryDecreaseTime <= hungryDecreaseTime)
            {
                currentHungryDecreaseTime+=Time.deltaTime;
            }
            else
            {
                currentHungry--;
                currentHungryDecreaseTime = 0;
            }
        }
        else //사망처리하기
            Debug.Log("배고픔 수치가 0이 되었습니다.");
    }
    void Thirsty()
    {
        if (currentThirsty > 0)
        {
            if (currentThirstyDecreaseTime <= thirstyDecreaseTime)
            {
                currentThirstyDecreaseTime += Time.deltaTime;
            }
            else
            {
                currentThirsty--;
                currentThirstyDecreaseTime = 0;
            }
        }
        else //사망처리하기
            Debug.Log("목마름 수치가 0이 되었습니다.");
    }
    void Tired()
    {
        if (currentTired > 0)
        {
            if (currentTiredDecreaseTime <= tiredDecreaseTime)
            {
                currentTiredDecreaseTime += Time.deltaTime;
            }
            else
            {
                currentTired--;
                currentTiredDecreaseTime = 0;
            }
        }
        else //사망처리하기
            Debug.Log("피곤함 수치가 0이 되었습니다.");
    }
    private void GaugeUpdate()
    {
        Image_gauges1.fillAmount = (float)stamina / currentSt;
        Image_gauges2.fillAmount = (float)currentHungry/hungry;
        Image_gauges3.fillAmount = (float)currentThirsty / thirsty;
        Image_gauges4.fillAmount = (float)currentTired / tired;

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
        if (stamina <= 0)
        {
            stamina = 0;
        }
        if (stamina >= 100)
        {
            stamina = 100;
        }
    }
    public void Recovery() //1프레임에 스테미너 1회복
    {
        //crouch()넣기
        if (stamina >= 100) //스테미너 100이상 리턴
        {
            recoveryTime = 0;
            return;
        }
        else if (isCrouch && !player.isMove) //앉은 상태에서만 회복
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
        if (!isRunning && stamina != 0 && !isCrouch)
        {
            stamina -= 4;
            player.agent.speed = 9;
            isRunning = true;
            Invoke("RunOff", 4);
        }
    }
    void RunOff()
    {
        float originSpeed = player.agent.speed;
        if (player.agent.speed == 9)
        {
            player.agent.speed = 5;
            isRunning = false;
        }
    }

}
