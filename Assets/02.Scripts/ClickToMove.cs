using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
/// <summary>
/// 조이스틱 이동XX
/// 땅을 클릭했을 때 이동
/// 플레이어 이동 터치 감지
/// </summary>
public class ClickToMove : MonoBehaviour, IPointerDownHandler
{
    public GameObject player;  //이동 대상
    private float targetDis;   //목표거리
    //public float speed;

    private NavMeshAgent navmesh;

    public Camera cam;
    public GameObject clickEffect; 

    private RaycastHit hit;
    

    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        Debug.Log("Touch");
    }
    private void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
            Debug.Log("Touch");
            Ray ray = cam.ScreenPointToRay(eventData.position);
            Physics.Raycast(ray, out hit);
            if (hit.transform != null)
            {
                clickEffect.SetActive(false);
                clickEffect.transform.position = cam.WorldToScreenPoint(hit.point);
                clickEffect.SetActive(true);
            }
            clickEffect.transform.position = hit.point;
            StopCoroutine("PlayerMove");  //중복터치 방지
            StartCoroutine("PlayerMove");
        
     
    }
    IEnumerator PlayerMove()
    {
        clickEffect.SetActive(true);
        while (true)
        {
            targetDis = (hit.point - player.transform.position).magnitude; //거리로 변경
            player.transform.LookAt(hit.point);
            player.transform.Translate(Vector3.forward * navmesh.speed * Time.deltaTime);

            if (targetDis <= 0.1f)
            {
                StopCoroutine("PlayerMove");
                clickEffect.SetActive(false);
            }

            yield return null;
        }
    }

}
