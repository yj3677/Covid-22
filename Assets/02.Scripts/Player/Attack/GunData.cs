using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    public enum WeaponType { Gun,Mlee}
    public WeaponType weaponType;
    public string gunName = "Gun1";
    public int damage; //대미지
    public float reloadTime; //장전시간
    public int reloadBulletCount; //총알 재장전 개수
    public int maxBullet;  //최대 소유 가능 총알 개수
    public int currBullet; //현재 탄창에 있는 총알 개수
    public int remainBullet; //현재 소유하고 있는 총알 개수
}
