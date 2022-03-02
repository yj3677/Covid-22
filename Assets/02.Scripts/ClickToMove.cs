using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 플레이어 이동 감지
/// </summary>
public class ClickToMove : MonoBehaviour, IPointerDownHandler
{
    public GameObject player;  //이동 대상
    private float targetDis;   //목표거리
    public float speed;

    public Camera cam;
    public GameObject clickEffect; 

    private RaycastHit hit;
    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = cam.ScreenPointToRay(eventData.position);
        Physics.Raycast(ray, out hit);
        clickEffect.transform.position = hit.point;
        StopCoroutine("PlayerMove");  //중복터치 방지
        StartCoroutine("PlayerMove");
    }
    IEnumerator PlayerMove()
    {
        clickEffect.SetActive(true);
        while(true)
        {
            targetDis = (hit.point-player.transform.position).magnitude; //거리로 변경
            player.transform.LookAt(hit.point);
            player.transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (targetDis<=0.1f)
            {
                StopCoroutine("PlayerMove");
                clickEffect.SetActive(false);
            }

            yield return null;
        }
    }
}
