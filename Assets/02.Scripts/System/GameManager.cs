using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataInfo;

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

    public CanvasGroup inventoryCG;  //인벤토리 캔버스 그룹

    PlayerState playerState;

    //PlayerPrefs를 활용한 데이터 저장
    [HideInInspector] public int killCount;
    //적 캐릭터를 죽인 횟수를 표시할 텍스트 UI
    public Text killCountText;

    //DataManager를 저장할 변수
    private DataManager dataMgr;
    public GameData gameData;
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

        //DataManager를 추출
        dataMgr = GetComponent<DataManager>();
        //DataManager 초기화
        dataMgr.Initialize();


        //게임의 초기 데이터 로드
        LoadGameData();

        playerState = FindObjectOfType<PlayerState>();
    }
    void Start()
    {
        OnInventoryOpen(false);
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if (points.Length>0)
        {
            StartCoroutine(this.CreateEnemy());
        }
    }

    //앱이 종료되는 시점에 호출되는 이벤트 함수
    private void OnApplicationQuit()
    {
        //게임 종료 전 게임 데이터를 저장한다.
        SaveGameData();
    }
    public void OnInventoryOpen(bool isOpen)
    {
        //isOpen상태면 아이템 창이 열리고, 하위 기능 활성화
        inventoryCG.alpha = (isOpen) ? 1 : 0;
        inventoryCG.interactable = isOpen;
        inventoryCG.blocksRaycasts = isOpen;
    }

    void LoadGameData()
    {
        //DataManager를 통해 파일에 저장된 데이터 불러오기
        GameData data = dataMgr.Load();

        gameData.hp = data.hp;
        gameData.damage = data.damage;
        gameData.speed = data.speed;
        gameData.killCount = data.killCount;
        gameData.equipItem = data.equipItem;
        //Kill Count 키로 지정된 값을 로드한다.
        //killCount = PlayerPrefs.GetInt("KillCount", 0);
        killCountText.text = "KILL " + gameData.killCount.ToString("0000");
    }
    private void SaveGameData()
    {
        dataMgr.Save(gameData);
    }

    public void IncKillCount()
    {
        //적 캐릭터 사망시킨 횟수 누적
        ++gameData.killCount;
        killCountText.text = "KILL " + gameData.killCount.ToString("0000");
        //PlayerPrefs.SetInt("KillCount", killCount);
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
