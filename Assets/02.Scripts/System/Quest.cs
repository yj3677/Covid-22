using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 일정 조건이 되면 퀘스트 함수가 실행
/// </summary>

public class Quest : MonoBehaviour
{
    public Transform[] points; //아이템이 생성될 위치를 담은 변수
    public GameObject RareBullet; //생성될 총알아이템
    public GameObject BossRoomKey; //생성될 열쇠아이템

    void Start()
    {
        points = GameObject.Find("RareItemSpawnGroup").GetComponentsInChildren<Transform>();
    }

    
    void Update()
    {
       StartCoroutine(NoticeQuest());
    }
    IEnumerator NoticeQuest()
    {
        if (UIManager.instance.killCount==5) //킬 카운트가 5가 되면 알림창 실행
        {
            UIManager.instance.notice1.SetActive(true);
            yield return new WaitForSeconds(15);
        }
        //30마리 치료 퀘스트를 완료하면 랜덤 위치에 아이템이 생성
        else if (UIManager.instance.killCount == 30) 
        {
            UIManager.instance.notice2.SetActive(true);
            RareItemSpawn();
            yield return new WaitForSeconds(15);
        }
       
    }
    //보스를 죽일 수 있는 아이템이 일정 장소에서 랜덤하게 드롭
    private void RareItemSpawn()
    {
            //랜덤 위치에 아이템 스폰
            int idx = Random.Range(1, points.Length);
            Instantiate(RareBullet, points[idx].position, points[idx].rotation);
            Instantiate(BossRoomKey, points[idx].position, points[idx].rotation);
    }

}
