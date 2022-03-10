using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField]
    private RectTransform stickCircle;
    private RectTransform rectTramsform;

    [SerializeField,Range(-20,360)]
    float stickCircleRange;

    public PlayerMove playerMove;
    public Vector2 inputDirection;
    private bool isInput; 
    private void Awake()
    {
        rectTramsform = GetComponent<RectTransform>();
        playerMove = GetComponent<PlayerMove>();
    }
    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        JoystickCircle(eventData);
        isInput=true;    
    }

    public void OnDrag(PointerEventData eventData)
    {
        JoystickCircle(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stickCircle.anchoredPosition = Vector3.zero;
        isInput = false;
        //playerMove.Move(inputDirection);
        Debug.Log("End");
    }
    private void JoystickCircle(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTramsform.anchoredPosition; 
        var inputVector = inputPos.magnitude < stickCircleRange ? inputPos : inputPos.normalized * stickCircleRange;
        stickCircle.anchoredPosition = inputVector;
        inputDirection = inputVector / stickCircleRange;
    }
    public void InputControlVector()
    {
        Debug.Log("1");
        //캐릭터에게 입력벡터를 전달
        playerMove.Move(inputDirection);
        Debug.Log(inputDirection.x + "/" + inputDirection.y);
    }


}
