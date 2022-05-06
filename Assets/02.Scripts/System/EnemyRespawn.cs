using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    //적 캐릭터가 출현할 위치를 담을 배열
    public Transform[] points;
    //적 캐릭터 프리팹을 저장할 변수
    public GameObject enemy;
    //생성된 적 프리팹을 담을 부모
    public GameObject enemies;
    //적 캐릭터를 생성할 주기
    public float createTime = 2;
    //적 캐릭터의 최대 생성 개수
    public int maxEnemy = 10;

    PlayerState playerState;
    void Start()
    {
        playerState = FindObjectOfType<PlayerState>();
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if (points.Length > 0)
        {
            StartCoroutine(EnemySpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator EnemySpawn()
    {
        
        {   //게임오버 상태가 아니라면 생성 주기마다 적을 리스폰
            while (!GameManager.instance.isGameOver || !playerState.isDead )
            {
                int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;
                //현재 적 숫자가 최대 적 숫자보다 적다면 리스폰
                if (enemyCount < maxEnemy)
                {
                    yield return new WaitForSeconds(createTime);
                    //랜덤한 위치에 적을 리스폰
                    int idx = Random.Range(1, points.Length);
                    GameObject enemiesInfo = Instantiate(enemy, points[idx].position, points[idx].rotation);
                    enemiesInfo.transform.parent = enemies.transform; //생성된 적을 enemies object의 자식으로 생성되게 한다
                }
                else yield return null;
            }
        }
    }
}
