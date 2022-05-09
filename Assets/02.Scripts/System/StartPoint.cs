using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    private PlayerMove player;
    private Play mainCamera;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
        mainCamera = FindObjectOfType<Play>();
        if (startPoint == player.currentMapName)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
            player.transform.position = new Vector3(transform.position.x, transform.position.y,transform.position.z);

        }
    }
    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        mainCamera = FindObjectOfType<Play>();
        if (startPoint == player.currentMapName)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, mainCamera.transform.position.z);
            player.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
