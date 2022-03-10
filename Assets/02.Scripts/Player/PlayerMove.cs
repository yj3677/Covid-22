using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField]
    private RectTransform stickCircle;
    private RectTransform rectTramsform;

    [SerializeField,Range(-20,360)]
    float stickCircleRange;
    private void Awake()
    {
        rectTramsform = GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTramsform.anchoredPosition;
        var inputVector = inputPos.magnitude < stickCircleRange ? inputPos : inputPos.normalized * stickCircleRange;
        stickCircle.anchoredPosition = inputVector;
        Debug.Log("Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTramsform.anchoredPosition;
        var inputVector = inputPos.magnitude < stickCircleRange ? inputPos : inputPos.normalized * stickCircleRange;
        stickCircle.anchoredPosition = inputVector;
        Debug.Log("Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stickCircle.anchoredPosition = new Vector3(0,0,0);
        Debug.Log("End");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
