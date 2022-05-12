using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    public PlayerMove player;
    public Play mainCamera;
    public GameObject player2;
    //private void Awake()
    //{
    //    if (startPoint == player.currentMapName)
    //    {
    //        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
    //        player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    //    }
    //}
    void Start()
    {
        player2 = GameObject.Find("Player");
        player = FindObjectOfType<PlayerMove>();
        mainCamera = FindObjectOfType<Play>();
        player2.SetActive(false);

        //player.enabled = false;
        //SceneMovePlayer();
        player2.SetActive(true);
        Invoke("SceneMovePlayer", 5);
    }
    private void Update()
    {
        //SceneMovePlayer();
        //Invoke("DontSceneMovePlayer", 2);
    }
    void SceneMovePlayer()
    {
        
        if (startPoint == player.currentMapName)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
            player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
        
        player.enabled = true;
    }
    void DontSceneMovePlayer()
    {
        gameObject.SetActive(false);
    }
}
