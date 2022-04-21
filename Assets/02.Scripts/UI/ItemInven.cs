using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Item Box 구현
/// 사용 안함
/// </summary>

public class ItemInven : MonoBehaviour
{
    bool isMouseover = false;
    Vector3 dir;
    Vector3 curInside;
    [SerializeField]
    private float boxOpenRange;

    public Transform playerPos;
    Outline outline;
    public GameObject inventory;
    private void Awake()
    {
       
    }
    private void Start()
    {
        outline = GetComponent<Outline>();
        
       // dir = (playerPos.position - transform.position);
    }
    private void Update()
    {
       // ItemBoxOpen();
    }

    private void OnMouseEnter()
    {
     
    }

    private void OnMouseExit()
    {
       
    }
    private void OnMouseDown()
    {
        
        float distance = Vector3.Distance(playerPos.position, transform.position); //플레이어가 아이템박스 근처로 갔을때 활성화
        Debug.Log(distance);
        if (gameObject.tag=="Item")
        {
            Debug.Log("Item");
            if (distance < boxOpenRange)
            {
                outline.enabled = false;
                inventory.SetActive(true);
            }
        }
        isMouseover = true;
    }
    void ItemBoxOpen()
    {
        float distance = Vector3.Distance(playerPos.position, transform.position);

        
    }
}
