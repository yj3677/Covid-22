using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 인벤토리에 있는 아이템 ToolTip 활성화 시키기
/// </summary>
public class SlotToolTip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;
    [SerializeField]
    private Text txtItemName;
    [SerializeField]
    private Text txtItemExplain;
    [SerializeField]
    private Text txtItemUse;

    //인벤토리에 있는 아이템 위에 손을대면 ToolTip 활성화
    public void ShowToolTip(Item item, Vector3 pos)
    {
        go_Base.SetActive(true);
        //ItemToolTip이 현재 위치의 우측 아래쪽(너비,높이의 반만큼)에 위치되게 한다
        pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0.8f,
               -go_Base.GetComponent<RectTransform>().rect.height * 0.8f, 0);
        go_Base.transform.position = pos;
        txtItemName.text = item.itemName;
        txtItemExplain.text = item.itemExplain;
        if (item.itemType==Item.ItemType.Equipment)
        {
            txtItemUse.text = " ";
        }
        else
        {
            txtItemUse.text = "클릭 - 아이템 사용";
        }
    }
    public void HideToolTip()
    { 
        go_Base.SetActive(false);  //ToolTip 비활성화
    }
}
