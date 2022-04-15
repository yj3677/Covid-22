using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private float startHp;  //시작체력
    [SerializeField]
    private float currentHp; //현재체력

    private GameObject healEffect; //치료이펙트 추가하기

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 2.2f, 0);
    [Header("---Item---")]
    public GameObject hpItem; //체력 아이템
    public GameObject vaccineItem;//(총알)백신 아이템
    public GameObject staminaItem; //총 아이템

    private PlayerShooter bulletDamage;
    private AttackCtrl mleeDamage;
    private Canvas uiCanvas;
    public Image hpBarImage;

    void Start()
    {
        currentHp = startHp;
        healEffect = Resources.Load<GameObject>("BloodSplat_FX");
        SetHpBar();
        mleeDamage = FindObjectOfType<AttackCtrl>();
    }

    public void SetHpBar()
    {
        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var hpbar = hpBar.GetComponent<EnemyHpBar>();
        hpbar.targetTr = this.gameObject.transform;
        hpbar.offset = hpBarOffset;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag== "VaccineBullet")
        {
            bulletDamage = FindObjectOfType<PlayerShooter>();
            Debug.Log("백신맞고 치유");
            ShowBloodEffect();
            currentHp -= bulletDamage.gunData.damage;
            hpBarImage.fillAmount = currentHp / startHp;
            //체력이 0이 되면 에너미 상태를 DIE로 전환
            if (currentHp <= 0)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                //아이템 드롭하는 함수 호출
                ItemDrop();
                //적 캐릭터 사망 횟수를 누적시키는 함수 호출
                GameManager.instance.IncKillCount();
                //Capsule Collider 컴포넌트 비활성화
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        else if(collider.gameObject.tag == "Weapon")
        {
            
            Debug.Log("방망이 맞음");
            currentHp -= mleeDamage.damage;
            hpBarImage.fillAmount = currentHp / startHp;
            if (currentHp <= 0)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                //아이템 드롭하는 함수 호출
                ItemDrop();
                //적 캐릭터 사망 횟수를 누적시키는 함수 호출
                GameManager.instance.IncKillCount();
                //Capsule Collider 컴포넌트 비활성화
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        
    }

    private void ItemDrop()
    {  //랜덤으로 아이템 드랍
        int ran = Random.Range(0, 10);
        if (ran < 5)
        {
            Debug.Log("Not Item");
        }
        else if (ran < 8)
        {  //HP
            Instantiate(hpItem, transform.position, Quaternion.identity);
        }
        else if (ran < 9)
        {  //Vaccine
            Instantiate(vaccineItem, transform.position, Quaternion.identity);
        }
        else if (ran < 10)
        {  //Gun
            Instantiate(staminaItem, transform.position, Quaternion.identity);
        }

    }

    private void ShowBloodEffect()
    {
        Vector3 pos = transform.position;
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward,pos);

        GameObject blood = Instantiate<GameObject>(healEffect, pos, rot);
        Destroy(blood, 1);
    }
}
