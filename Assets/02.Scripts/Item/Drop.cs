using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DataInfo;

public class Drop : MonoBehaviour //IDropHandler
{
    
    public GameObject slotParent;
    //슬롯
    private Slot[] slots;
    private void Start()
    {
        slots = GetComponentsInChildren<Slot>();
    }
    public void AcquireItem(Item item, int count=1)
    {
        if (Item.ItemType.Equipment!=item.itemType) //장비 아이템이 아닐 경우에만
        {
            //이미 아이템이 있다면 개수만 증가
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item !=null) //기본값이 비어있어서
                {
                    if (slots[i].item.itemName == item.itemName)
                    {
                        slots[i].SetSlotCount(count);
                        return;
                    }
                }
            }
        }
        //아이템이 없다면 빈자리를 찾아 넣기
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item,count);
                return;
            }
        }
    }


    //아이템 드래그 했을 때 옮겨지는 현상 
    //public void OnDrop(PointerEventData eventData)
    //{
    //    if (transform.childCount == 0)
    //    {
    //        ItemDrag.draggingItem.transform.SetParent(this.transform);
    //        //슬롯에 추가된 아이템을 GameData에 추가하기 위해 AddItem 호출
    //        Item item = ItemDrag.draggingItem.GetComponent<ItemInfo>().itemData;
            
    //    }
    //}

 
}
