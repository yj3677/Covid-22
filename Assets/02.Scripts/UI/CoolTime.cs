using UnityEngine;
using UnityEngine.UI;

public class CoolTime : MonoBehaviour
{
    public PlayerMove playerMove;
    [Header("---Run---")]
    public Text text_RunCoolTime; //쿨타임 텍스트
    public Image btn_Run;   //쿨타임 이미지
    public float run_Cooltime = 4; //쿨타임
    public float run_Current;      //진행되고 있는 쿨타임 시간
    [Header("---ChangeWeapon---")]
    public Text text_WeaponCoolTime;
    void Start()
    { 
    }
    void Update()
    {
        if (!playerMove.isRunning) //달리는 중이 아니라면
        {
            //ResetCoolTime();
            EndCoolTime();
            btn_Run.fillAmount = 1;
            return;
        }
        CheckCoolTime();
    }
    public void RunBtn()
    {
        if (playerMove.isRunning)
        {
            return;
        }
        if (!playerMove.isRunning)
        {
            ResetCoolTime();
        }
        
    }

    private void CheckCoolTime()
    {
        run_Current -= Time.deltaTime;
        if (run_Current < run_Cooltime) //쿨타임 시간이 안됐으면
        {
            SetFillAmount(run_Current);  //함수 실행
            text_RunCoolTime.text = run_Current.ToString("F0");
            text_RunCoolTime.gameObject.SetActive(true); //텍스트 지우기
        }
        else if (!playerMove.isRunning)
        {
            EndCoolTime();
        }
    }

    private void SetFillAmount(float value)
    {
        btn_Run.fillAmount = value / run_Cooltime;  //value값에 따라 쿨타임UI fillAmount 변경
    }

    private void EndCoolTime()
    {
        ResetCoolTime();
        SetFillAmount(run_Cooltime);  //쿨타임UI fillAmount 초기값으로 변경
        text_RunCoolTime.gameObject.SetActive(false); //텍스트 지우기
        

    }
    private void ResetCoolTime()
    { //시간 초기화 
        run_Current = 4;
        SetFillAmount(4);
    }

}
