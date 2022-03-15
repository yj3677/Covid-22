using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [SerializeField]
    private RectTransform stickCircle;  
    private RectTransform rectTransform;

    [SerializeField,Range(0,360)]
    float stickCircleRange;

    
    public PlayerMove playerMove;
    public Vector2 inputDirection;
    bool isInput;

    public enum JoyStickType { Move, Rotate }
    public JoyStickType joystickType;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
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
        stickCircle.anchoredPosition = Vector2.zero;
        isInput = false;
        playerMove.anim.SetBool("IsWalk", false);  //백조이스틱 안에서 손 땠을때 움직이는 현상 방지
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {
            switch (joystickType)
            {
                case JoyStickType.Move:
                    playerMove.Move(Vector2.zero);
                    break;
                case JoyStickType.Rotate:
                    break;
            }
        }
      
        
        
    }
    private void JoystickCircle(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition; 
        var inputVector = inputPos.magnitude < stickCircleRange ? inputPos : inputPos.normalized * stickCircleRange; //스틱범위
        stickCircle.anchoredPosition = inputVector;  
        inputDirection = inputVector / stickCircleRange; //0-1정규화된 값으로 캐릭터에게 전달 
    }
    public void InputControlVector()
    {
        //캐릭터에게 입력벡터를 전달
        switch (joystickType)
        {
            case JoyStickType.Move:
                playerMove.Move(inputDirection);
                break;
            case JoyStickType.Rotate:
                playerMove.LookAround(inputDirection);
                break;
        }

       
        Debug.Log(inputDirection.x + "/" + inputDirection.y);
    }


}
