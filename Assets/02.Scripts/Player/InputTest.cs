using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputTest : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public RectTransform circle;
    private RectTransform rectTransform;

    Transform trCube;
    float radius;
    float speed = 5;
    float sqr = 0;

    Vector3 vecMove;
    Vector2 vecNormal;

    bool touch = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        rectTransform = transform.Find("JoyStick").GetComponent<RectTransform>();
        circle = transform.Find("JoyStic/JoyStickCircle").GetComponent<RectTransform>();
        radius = rectTransform.rect.width * 0.5f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        circle.anchoredPosition = inputDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        circle.anchoredPosition = inputDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        circle.anchoredPosition = Vector2.zero;
    }
}
