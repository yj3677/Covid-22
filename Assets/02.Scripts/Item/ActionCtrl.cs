using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionCtrl : MonoBehaviour
{
    private PlayerState playerState;
    private Drop drop;
    [SerializeField]
    private float range; //습득 가능한 최대 거리
    private bool pickupActivated = false; //습득 가능 여부
    private RaycastHit hitInfo; //충돌체 정보 저장

    //아이템 레이어에만 반응하도록 레이어 마스크 설정
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Text actionText;

    private void Start()
    {
        playerState = GetComponent<PlayerState>();
        drop = FindObjectOfType<Drop>();
    }
    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        CheckItem();
        //CanPickUp();
    }

    public void CanPickUp()
    {
        //아이템을 주울 수 있는 상태라면 줍기
        if (pickupActivated && (!playerState.isCrouch) && (!playerState.isDead))
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득했습니다.");
                drop.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item); //아이템 장비창에 넣기
                Destroy(hitInfo.transform.gameObject);
                DisAppearInfo();
            }
        }
    }

    //주변에 아이템이 있는지 확인
    private void CheckItem()
    {
        //플레이어 위치에서 바라보는 위치로 레이를 쏴 충돌체의 정보 출력
        if (Physics.Raycast(transform.position,
            transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            DisAppearInfo();
        }
    }

    private void ItemInfoAppear()
    {
        if (playerState.isDead)
        {
            return;
        }
        //플레이어가 바라보고 있다면 활성
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "획득" + "<color=green>" + "(PickUp 버튼)" + "</color>";
    }
    public void DisAppearInfo()
    {
        //플레이어가 바라보고 있지 않다면 비활성
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
}
