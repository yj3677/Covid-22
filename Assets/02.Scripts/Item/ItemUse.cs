using UnityEngine;


public class ItemUse : MonoBehaviour
{
    public WeaponManager weaponManger;
    private PlayerShooter playerShooter;
    private PlayerState playerState;
    private Slot slot;
    private Item item;

    public GameObject mixTool; //아이템 사용 시 열릴 조합창

    public bool isDoorOpen = false; //보스방 활성화
    public GameObject doorEffect; //다음 스테이지로 이동할 포탈 
    private void Start()
    {

    }


    void MixItemUsed()
    { //Vaccine 아이템 사용 시 조합창이 뜨고 조합창에 백신과 조합서를 올리고 조합버튼을 누르면 치료제가 생성되게 한다
        if (item.itemType==Item.ItemType.Vaccine)
        {
            mixTool.SetActive(true);
            
        }
    }
    void MixTool()
    {
        if (item.itemType==Item.ItemType.Vaccine && item.itemType==Item.ItemType.Prescription)
        {

        }
    }
}
