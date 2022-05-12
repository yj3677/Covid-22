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
    private ActionCtrl actionCtrl;
    private PlayerMove playermove;
    private PlayerInput playerInput;
    public Animator playerAnim;
    private EnemyFire enemyfire;
    private BossAttack bossFire;
    private EnemyMove enemyMove;
    private EnemyAI enemyAI;
    public int enemyDamage; //공격 받았을 때 받아올 에너미데미지 
    [SerializeField]
    private GameObject playerDeadTr;


    //앉기
    [Header("---Crouch---")]
    float recoveryTime = 0;
    public bool isCrouch = false;
    //체력
    public int maxHealth; //최대 체력
    public int health;
    public int currentHp;

    //스테미너
    public int maxStamina; //최대 스테미너
    public int stamina;
    public int currentStamina;

    //배고픔
    public int maxHungry; //최대 포만감
    public int hungry;
    public int currentHungry;
    private float hungryDecreaseTime=2; 
    private float currentHungryDecreaseTime;

   
    //배열로 구현하기
    public Image Image_gauges0;
    public Image Image_gauges1;
    public Image Image_gauges2;

    

    //플레이어 죽음
    public bool isDead=false;
    //델리게이트를 이용해 모든 적 AI스크립트를 검색해 OnPlayerDie 함수를 실행시킴.
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;
   
    private void Awake()
    {
        actionCtrl = FindObjectOfType<ActionCtrl>();
        playerInput = FindObjectOfType<PlayerInput>();
        playermove = FindObjectOfType<PlayerMove>();
        //enemyMove = FindObjectOfType<EnemyMove>();
        //enemyAI = FindObjectOfType<EnemyAI>();
        playerAnim = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHp = health;
        currentStamina = stamina;
        currentHungry = hungry;
    }

    private void FixedUpdate()
    {
        Stamina();       
    }
    void Update()
    {
        Recovery();
        Hungry();
        GaugeUpdate(); 
    }

    void Health()
    {
        Debug.Log("체력함수실행");
        if (isDead)
        {
            return;
        }
        if (currentHp>0)
        {
            Debug.Log("체력--");
            currentHp -= enemyDamage;
            if (currentHp <= 0)
            {
                isDead = true;
                Die();
            }
        } 
    }
    void Hungry()
    {
        if (isDead)
        {
            return;
        }
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
        {
            isDead = true;
            Die();
            Debug.Log("배고픔 수치가 0이 되었습니다.");
        }        
    }
    private void GaugeUpdate()
    {
        Image_gauges0.fillAmount = (float)currentHp / health;
        Image_gauges1.fillAmount = (float)stamina / currentStamina;
        Image_gauges2.fillAmount = (float)currentHungry/hungry;    
    }
    public void Crouch()
    {
        if (isDead)
        {
            return;
        }
        if (!(playermove.isRunning)||!(playermove.isMove))
        {
            isCrouch = !isCrouch;

            if (isCrouch)
            {
                playerAnim.SetBool("IsCrouch", isCrouch);
                playermove.navMesh.speed = 0;
            }
            else
            {
                playerAnim.SetBool("IsCrouch", false);
                playermove.navMesh.speed = 5;
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
        if (isDead)
        {
            return;
        }
        //crouch()넣기
        if (stamina >= 100) //스테미너 100이상 리턴
        {
            recoveryTime = 0;
            return;
        }
        else if (isCrouch && !playermove.isMove) //앉은 상태에서만 회복
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
    public void Die()
    {
        if (!isDead)
        {
            return;
        }
        else 
        {
            //GameManager.instance.isGameOver = true;
            Debug.Log("Player Die");
            playerAnim.SetTrigger("IsDead");
            //죽었을때 높이 조절 
            playerAnim.applyRootMotion = true;
            isDead = true;
            actionCtrl.DisAppearInfo();
            playermove.navMesh.speed = 0; //속도0
            playerInput.inputDirection = Vector2.zero;
            playermove.isMove = false;
            OnPlayerDie();
        }
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (isDead)
        {
            return;
        }
        //애너미에게 바이러스 공격 당했을 때
        if (other.gameObject.tag == "Virus")
        {
            Debug.Log(currentHp);
            enemyfire = FindObjectOfType<EnemyFire>();
            Health();
        }
    }
}
