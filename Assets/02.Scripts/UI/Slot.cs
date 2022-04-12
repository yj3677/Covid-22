using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item; //획득한 아이템
    public int itemCount; //획득한 아이템의 개수
    public Image itemImage; //아이템의 이미지

    [SerializeField]
    private Text textCount; //텍스트UI
    [SerializeField]
    private GameObject CountImage; //아이템 획득시 텍스트 이미지 띄우기
    
    private void SetColor(float alpha)
    {
        //인벤토리에 아이템이 없으면 아이템이미지 투명하게 설정
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
    public void AddItem(Item _item, int count=1)
    {
        //아이템 획득
        item = _item;
        itemCount = count;
        itemImage.sprite = item.itemImage;

        if (item.itemType!=Item.ItemType.Equipment)
        {
            CountImage.SetActive(true);
            textCount.text = itemCount.ToString();
        }
        else
        {
            textCount.text = " ";
            CountImage.SetActive(false);
        }

        //아이템 획득 시 알파값1
        SetColor(1);

    }
    public void SetSlotCount(int count)
    {
        //아이템 개수 조정
        itemCount += count;
        textCount.text = itemCount.ToString();

        if (itemCount<=0)
        {
            ClearSlot();
        }
    }
    private void ClearSlot()
    {
        //아이템이 0개가 되면 슬롯 초기화
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);
        textCount.text = " ";
        CountImage.SetActive(false);
        
    }
}
