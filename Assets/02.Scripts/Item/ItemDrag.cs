using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour ,IDragHandler, IBeginDragHandler,IEndDragHandler
{
    private Transform itemTr;  //먹을 아이템 위치
    private Transform inventoryTr;  //아이템 놓을 위치

    private Transform getSlotListTr;
    private CanvasGroup canvasGroup;

    public static GameObject draggingItem = null; 

  
    void Start()
    {
        inventoryTr = GameObject.Find("PlayerInventory").GetComponent<Transform>();
        itemTr = GetComponent<Transform>();
        getSlotListTr = GameObject.Find("GetItemSlot").GetComponent<Transform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(inventoryTr);
        draggingItem = this.gameObject;

        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        itemTr.position = Input.mousePosition; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggingItem = null;
        canvasGroup.blocksRaycasts = true;
        if (itemTr.parent==inventoryTr)
        {
            itemTr.SetParent(getSlotListTr.transform);
        }
    }
}
