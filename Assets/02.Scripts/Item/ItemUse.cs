using UnityEngine;
using UnityEngine.EventSystems;


public class ItemUse : MonoBehaviour,IPointerClickHandler
{
    public WeaponManager weaponManger;
    private PlayerShooter playerShooter;
    private PlayerState playerState;
    private Slot slot;
    private Item item;



    public bool isDoorOpen = false; //보스방 활성화
    public ItemMix itemMix;
    public GameObject doorEffect; //다음 스테이지로 이동할 포탈 
    private void Start()
    {
        itemMix = FindObjectOfType<ItemMix>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }
    public void ItemHP()
    {
     
        playerState.currentHp += item.valueEffect;
        if (playerState.currentHp > playerState.maxHealth)
            playerState.currentHp = playerState.maxHealth;
        Debug.Log(item.itemName + "을 사용했습니다.");
    
    }
    public void ItemStamina()
    {
        if (item.itemType == Item.ItemType.Stamina)
        {
            playerState.currentStamina += item.valueEffect;
            if (playerState.currentStamina > playerState.maxStamina)
                playerState.currentStamina = playerState.maxStamina;
            Debug.Log(item.itemName + "을 사용했습니다.");
        }
    }
    public void ItemFood()
    {
        if (item.itemType == Item.ItemType.Food)
        {
            playerState.currentHungry += item.valueEffect;
            if (playerState.currentHungry > playerState.maxHungry)
                playerState.currentHungry = playerState.maxHungry;
            Debug.Log(item.itemName + "을 사용했습니다.");
        }
    }
    public void ItemBullet()
    {
        if (item.itemType == Item.ItemType.Bullet && weaponManger.currentWeaponType == "Gun")
        {
            playerShooter = FindObjectOfType<PlayerShooter>();
            playerShooter.remainBullet += item.number;
            Debug.Log(item.itemName + "을 사용했습니다.");
        }
    }

    void MixItemUsed()
    { //Vaccine 아이템 사용 시 조합창이 뜨고 조합창에 백신과 조합서를 올리고 조합버튼을 누르면 치료제가 생성되게 한다
        if (item != null) //아이템 유무 확인
        {
            if (item.itemType == Item.ItemType.Equipment) //클릭한게 장비 아이템인지
            {
                StartCoroutine(weaponManger.ChangeWeaponCoroutine(item.weaponType));
            }
            else if (item.itemType == Item.ItemType.Key)
            {
                isDoorOpen = true;
                doorEffect.SetActive(true);
                //문 열린 텍스트 활성화하기
            }
            //HP아이템을 사용하고, maxHealth보다 적다면 회복
            else if (item.itemType == Item.ItemType.Hp)
            {
                playerState.currentHp += item.valueEffect;
                if (playerState.currentHp > playerState.maxHealth)
                    playerState.currentHp = playerState.maxHealth;
                Debug.Log(item.itemName + "을 사용했습니다.");
                //MinusSlotCount(-item.number);
            }
            //Stamina아이템을 사용
            else if (item.itemType == Item.ItemType.Stamina)
            {
                playerState.currentStamina += item.valueEffect;
                if (playerState.currentStamina > playerState.maxStamina)
                    playerState.currentStamina = playerState.maxStamina;
                Debug.Log(item.itemName + "을 사용했습니다.");
                //MinusSlotCount(-item.number);
            }
            //Food아이템을 사용
            else if (item.itemType == Item.ItemType.Food)
            {
                playerState.currentHungry += item.valueEffect;
                if (playerState.currentHungry > playerState.maxHungry)
                    playerState.currentHungry = playerState.maxHungry;
                Debug.Log(item.itemName + "을 사용했습니다.");
                //MinusSlotCount(-item.number);
            }
            //총을 든 상태에서만 Bullet아이템을 사용
            else if (item.itemType == Item.ItemType.Bullet && weaponManger.currentWeaponType == "Gun")
            {
                playerShooter = FindObjectOfType<PlayerShooter>();
                playerShooter.remainBullet += item.number;
                Debug.Log(item.itemName + "을 사용했습니다.");
                //MinusSlotCount(-item.number);
            }
            else if (item.itemType == Item.ItemType.Vaccine && !itemMix.isVaccine)
            {
                itemMix.isVaccine = true;  //수정
                //MinusSlotCount(-item.number);
            }
            else if (item.itemType == Item.ItemType.Prescription && !itemMix.isPrescription)
            {
                itemMix.isPrescription = true; //수정
                //MinusSlotCount(-item.number);
            }
            else if (item.itemType == Item.ItemType.Medicine)
            {
                //vaccineAttack.DoVaccineAttack();
                ////적 캐릭터 사망 횟수를 누적시키는 함수 호출
                UIManager.instance.KillCount();
            }
        }
    }


}
