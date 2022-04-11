using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataInfo
{
    [System.Serializable]
    public class GameData
    {
        public int killCount = 0;
        public float hp = 120;
        public float damage = 25;
        public float speed = 6;
        public List<Item> equipItem = new List<Item>();
    }
    [System.Serializable]
    public class Item
    {
        public enum ItemType { HP,SPEED,GRENADE,DAMAGE} //아이템 종류선언
        public enum ItemCalc { INC_VALUE,PERCENT}       //계산방식 선언
        public ItemType itemType;                       //아이템의 종류 
        public ItemCalc itemCalc;                       //아이템 적용 시 계산 방식                
        public string name;                             //아이템 이름 
        public string desc;                             //아이템 소개               
        public float value;                             //계산 값              
    }
}
