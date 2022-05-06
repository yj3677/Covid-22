using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DataInfo;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameData gameData;
    
    public Text killCountText; //적 캐릭터를 죽인 횟수를 표시할 텍스트 UI
    public GameObject notice1; //알림창1
    public GameObject notice2; //알림창2

    public Text bulletCapacityTxt; //탄창에 남은 총알 개수를 표시할 텍스트 UI
    public Text remainBulletTxt; //전체 남은 총알 개수를 표시할 텍스트 UI
    public int killCount; //적 죽인수

    void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    public void KillCount()
    {
        //적 캐릭터 치료시킨 횟수 누적
        ++killCount;
        killCountText.text = "치료 " + killCount.ToString("0000");
        //PlayerPrefs.SetInt("KillCount", killCount);
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
