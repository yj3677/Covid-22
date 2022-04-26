using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName; //아이템의 이름.
    [TextArea] //여러줄 사용가능
    public string itemExplain; //아이템의 설명.
    public ItemType itemType; //아이템의 유형
    public Sprite itemImage; //아이템의 이미지.
    public GameObject itemPrefab; //아이템의 프리팹
    public int valueEffect; //아이템 사용시 수치(값)
    public int number; //아이템 개수
    public int usenum; //아이템 사용 시 줄어들 개수

    public string weaponType; //무기 유형
    public enum ItemType
    {
        Equipment, Hp, Stamina, Food, Key, Vaccine
    }
}
