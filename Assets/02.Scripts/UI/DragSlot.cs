using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 인벤토리에서 아이템의 위치를 옮길 때 백이미지(백슬롯)가 움직이지 않도록
/// </summary>
public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public Slot dragSlot;
    [SerializeField]
    private Image imageItem; //아이템 이미지

    private void Start()
    {
        instance = this;
    }

    public void DragSetImage(Image itemImage)
    {
        imageItem.sprite = itemImage.sprite; //이미지에 스프라이트 넣어주기
        SetColor(1);
    }
    public void SetColor(float alpha)
    { //드래그 시 비어있는 이미지는 흰색의 배경이 보이게 함.(평소에는 투명 상태)
        Color color = imageItem.color;
        color.a = alpha;
        imageItem.color = color;
    }
  
}
