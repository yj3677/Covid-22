using UnityEngine;


public class ItemMix : MonoBehaviour
{
    Item item;
    public bool isVaccine;
    public bool isPrescription;
    public bool isItemUse; //아이템 사용 여부

    public PlayerState playerState;
    public GameObject mixTool; //아이템 사용 시 열릴 조합창
    public GameObject medicine; //조합 시 생성될 치료제
    void Start()
    {

    }

    void Update()
    {
        if (isVaccine&&isPrescription)
        {
            mixTool.SetActive(true);
        }

    }

    //public void MixItemUsed()
    //{ //Vaccine 아이템 사용 시 조합창이 뜨고 조합창에 백신과 조합서를 올리고 조합버튼을 누르면 치료제가 생성되게 한다
    //    if (item.itemType == Item.ItemType.Vaccine)
    //    {
    //        isVaccine = true;
    //    }
    //    else if (item.itemType == Item.ItemType.Prescription)
    //    {
    //        isPrescription = true;
    //    }



    //}
    public void MixTool()
    {
        Debug.Log("테스트");
        Instantiate(medicine, playerState.transform.position, Quaternion.identity);
        isItemUse = true;
        isVaccine = false;
        isPrescription = false;
        mixTool.SetActive(false);
        Invoke("MixItemUsed",2);
        
    }
    void MixItemUsed()
    {
        if (isItemUse)
        {
            isItemUse = false;
        }
    }
}
