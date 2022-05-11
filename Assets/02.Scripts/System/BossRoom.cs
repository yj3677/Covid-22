using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 30명을 치유 시키고 해금된 보스방 열쇠와 보스전용 백신총알을 찾아
/// 보스방 입구로 가 열쇠를 사용하면 보스방으로 이동
/// </summary>
public class BossRoom : MonoBehaviour
{
    public string transferMapName; //이동할 맵이름.
    public GameObject doorEffect; //다음 스테이지로 이동할 포탈 
    public GameObject NoticeBossRoom;
    public GameObject startPoint; //맵 이동 시 시작 지점

    public bool isDoorOpen; //열쇠 아이템 사용했는지

    public PlayerMove player;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //플레이어가 해당 콜라이더에 닿아있고, 문이 열린 상태(아이템 사용)에서 문을 클릭하면 씬 이동
        if (other.tag == "Player" && isDoorOpen)
        {
            player.currentMapName = transferMapName;
            NoticeBossRoom.SetActive(true);
            GameManager.instance.isBossRoom = true;

        }
        else
        {
            return;
        }
    }
    void GoBossRoom()
    {
        
    }
    public void NoticeYes()
    {
        NoticeNo();
        SceneManager.LoadScene("BossRoom");

    }
    public void NoticeNo()
    {
        NoticeBossRoom.SetActive(false);
    }
}
