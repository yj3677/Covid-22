using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //적 캐릭터가 출현할 위치를 담을 배열
    public Transform[] points;
    //적 캐릭터 프리팹을 저장할 변수
    public GameObject enemy;
    //적 캐릭터를 생성할 주기
    public float createTime = 2;
    //적 캐릭터의 최대 생성 개수
    public int maxEnemy = 10;
    //게임 종료 여부를 판단할 변수
    public bool isGameOver = false;

    PlayerState playerState;
    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if(instance !=null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        playerState = FindObjectOfType<PlayerState>();
    }
    void Start()
    {
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if (points.Length>0)
        {
            StartCoroutine(this.CreateEnemy());
        }
    }

    void Update()
    {
        
    }
    IEnumerator CreateEnemy()
    {   //게임오버 상태가 아니라면 생성 주기마다 적을 리스폰
        while(!isGameOver || !playerState.isDead)
        {
            int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
            //현재 적 숫자가 최대 적 숫자보다 적다면 리스폰
            if (enemyCount < maxEnemy)
            {
                yield return new WaitForSeconds(createTime);
                //랜덤한 위치에 적을 리스폰
                int idx = Random.Range(1, points.Length);
                Instantiate(enemy, points[idx].position, points[idx].rotation);
            }
            else yield return null;
        }
    }
}
