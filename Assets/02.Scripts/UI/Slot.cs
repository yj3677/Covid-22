using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item; //획득한 아이템
    public int itemCount; //획득한 아이템의 개수
    public Image itemImage; //아이템의 이미지
    public bool isDoorOpen = false; //보스방 활성화
    public GameObject doorEffect; //다음 스테이지로 이동할 포탈 

    [SerializeField]
    private Text textCount; //텍스트UI
    [SerializeField]
    private GameObject countImage; //아이템 획득시 텍스트 이미지 띄우기

    PlayerState playerState;
    private WeaponManager weaponManger;
    private void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        weaponManger = FindObjectOfType<WeaponManager>();
    }

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
            countImage.SetActive(true);
            textCount.text = itemCount.ToString();
        }
        else
        {
            textCount.text = " ";
            countImage.SetActive(false);
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
        countImage.SetActive(false);
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button==PointerEventData.InputButton.Left) //우클릭시 =>한번 터치시로 바꾸기
        {
            if (item!=null) //아이템 유무 확인
            {
                if (item.itemType==Item.ItemType.Equipment) //클릭한게 장비 아이템인지
                {
                    //장착
                    StartCoroutine(weaponManger.ChangeWeaponCoroutine(item.weaponType));
                }
                else if (item.itemType==Item.ItemType.Key)
                {
                    isDoorOpen = true;
                    doorEffect.SetActive(true);
                    //문 열린 텍스트 활성화하기
                }
                //HP아이템을 사용하고, 최대HP보다 적다면
                else if(item.itemType == Item.ItemType.Hp && playerState.currentHp <= playerState.maxHealth)
                {
                    //소모
                    //회복물약 구현
                    
                    playerState.currentHp += item.num;
                    Debug.Log(item.itemName + "을 사용했습니다.");
                    SetSlotCount(-1);                    
                }
                else if (item.itemType == Item.ItemType.Hp && playerState.currentHp == playerState.maxHealth)
                {
                    return;
                }
                else if (item.itemType == Item.ItemType.Stamina)
                {
                    //소모
                    playerState.currentStamina += item.num;
                    Debug.Log(item.itemName + "을 사용했습니다.");
                    SetSlotCount(-1);
                }
                else if (item.itemType == Item.ItemType.Food)
                {
                    //소모
                    playerState.currentHungry += item.num;
                    Debug.Log(item.itemName + "을 사용했습니다.");
                    SetSlotCount(-1);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item!=null)
        {
            DragSlot.instance.dragSlot = this; //드래그에 자기자신 넣기(아이템 정보)
            DragSlot.instance.DragSetImage(itemImage); //드래그 중인 아이템 이미지가 넣어짐
            DragSlot.instance.transform.position = eventData.position;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    { //드래그가 끝나면 아이템 정보를 빼고 이미지 없애기
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
        Debug.Log("OnEndDrag 호출");
    }

    public void OnDrop(PointerEventData eventData)
    { //아이템슬롯의 자리가 서로 바뀌는 것을 구현 
        if (DragSlot.instance.dragSlot !=null)
        { //아이템이 없는 빈 슬롯을 드래그 할 때 ChangeSlot()이 호출되는 것을 방지
            ChangeSlot();
        }
        Debug.Log("OnDrop 호출");
    }

    private void ChangeSlot()
    {
        Item tempItem = item; //아이템(B)
        int tempItemCount = itemCount;
        //(A)드래그하고 있는 아이템을 (B)다른 아이템 자리에 놓기
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        if (tempItem !=null)
        {  //옮기려는 자리에 (B)다른 아이템이 있다면 아이템끼리 자리를 교환
            DragSlot.instance.dragSlot.AddItem(tempItem, tempItemCount);
        }
        else
        {  //비어있으면 그대로 넣기
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
    void ItemUse()
    {

    }
}
