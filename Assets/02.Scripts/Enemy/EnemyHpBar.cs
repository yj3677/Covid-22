using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    private Camera uiCamera;
    private Canvas canvas;
    private RectTransform rectParent;
    private RectTransform rectHp;

    [HideInInspector]
    public Vector3 offset = Vector3.zero;  //캐릭터에서 얼마만큼 떨어진 위치에 놔둘건지
    [HideInInspector]
    public Transform targetTr;  //적의 위치를 알아내기 위해
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        uiCamera = canvas.worldCamera;  //캔버스에 있는 카메라 불러오기
        rectParent = canvas.GetComponent<RectTransform>(); //부모의 위치값
        rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset);
        if (screenPos.z<0)
        {
            screenPos *= -1f;
            //z-메인카메라에서 XY평면까지의 거리
        }

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos);

        rectHp.localPosition = localPos;
    }

}
