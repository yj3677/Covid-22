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
    private PlayerMove playermove;
    private PlayerInput playerInput;
    private Animator playerAnim;
    private EnemyFire enemyfire;
    private GameObject playerDeadTr;

    //앉기
    [Header("---Crouch---")]
    float recoveryTime = 0;
    public bool isCrouch = false;
    //체력
    public int health = 100;
    [SerializeField]
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
    public Image Image_gauges0;
    public Image Image_gauges1;
    public Image Image_gauges2;
    public Image Image_gauges3;
    public Image Image_gauges4;
    


    public bool isDead=false;
   
    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        playermove = FindObjectOfType<PlayerMove>();
        enemyfire = FindObjectOfType<EnemyFire>();
        playerAnim = GetComponentInChildren<Animator>();
        playerDeadTr = transform.GetChild(0).gameObject;
    }
    private void Start()
    {
        currentHp = health;
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

    void Health()
    {
        if (isDead)
        {
            return;
        }
        if (currentHp>0)
        {
            currentHp -= enemyfire.damage;
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
    void Thirsty()
    {
        if (isDead)
        {
            return;
        }
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
        {
            isDead = true;
            Die();
            Debug.Log("목마름 수치가 0이 되었습니다.");
        }
            
    }
    void Tired()
    {
        if (isDead)
        {
            return;
        }
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
        {
            isDead = true;
            Die();
            Debug.Log("피곤함 수치가 0이 되었습니다.");
        }
            
    }
    private void GaugeUpdate()
    {
        Image_gauges0.fillAmount = (float)currentHp / health;
        Image_gauges1.fillAmount = (float)stamina / currentSt;
        Image_gauges2.fillAmount = (float)currentHungry/hungry;
        Image_gauges3.fillAmount = (float)currentThirsty / thirsty;
        Image_gauges4.fillAmount = (float)currentTired / tired;
        

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
            playerAnim.SetTrigger("IsDead");
            transform.position = new Vector3(transform.position.x, -0.9f, transform.position.z);
            isDead = true;
            playermove.navMesh.speed = 0; //속도0
            //죽었을때 높이 조절 
            playerInput.inputDirection = Vector2.zero;
            playermove.isMove = false;
            
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
            Health();
        }
    }

}
