using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    public PlayerMove player;
    public Play mainCamera;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
        mainCamera = FindObjectOfType<Play>();
        if (startPoint == player.currentMapName)
        {
            Debug.Log("플레이어 시작위치");
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
            player.transform.position = new Vector3(transform.position.x, transform.position.y,transform.position.z);

        }
    }
    private void OnEnable()
    {
        player = FindObjectOfType<PlayerMove>();
        mainCamera = FindObjectOfType<Play>();
        if (startPoint == player.currentMapName)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
            player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        SceneMovePlayer();
        Invoke("DontSceneMovePlayer", 2);
    }
    void SceneMovePlayer()
    {
        player = FindObjectOfType<PlayerMove>();
        mainCamera = FindObjectOfType<Play>();
        if (startPoint == player.currentMapName)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
            player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        }
    }
    void DontSceneMovePlayer()
    {
        gameObject.SetActive(false);
    }
}
