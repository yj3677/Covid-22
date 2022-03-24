using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputTest : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private Image joyBackground;
    private Image joyController;
    public Vector2 inputDirection;

    public enum JoyStickType { Move, Rotate }
    public JoyStickType joystickType;
    public float sensitivity;

    public bool isInput=false;
    public float horizontal { get { return inputDirection.x * sensitivity; } }
    public float vertical { get { return inputDirection.y * sensitivity; } }

    private void Awake()
    {
        joyBackground = GetComponent<Image>();
        joyController = transform.GetChild(0).GetComponent<Image>();

    }
 
 

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("터치 시작");
        //JoystickCircle(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        inputDirection = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joyBackground.rectTransform, eventData.position,eventData.pressEventCamera, out inputDirection))
        {
            //Debug.Log(inputDirection);
            inputDirection.x = (inputDirection.x / joyBackground.rectTransform.sizeDelta.x);
            inputDirection.y = (inputDirection.y / joyBackground.rectTransform.sizeDelta.y);

            switch(joystickType)
            {
                case JoyStickType.Move:
                    inputDirection = new Vector2(inputDirection.x * 2 - 1, inputDirection.y * 2 - 1);
                    break;
                case JoyStickType.Rotate:
                    inputDirection = new Vector2(inputDirection.x * 2 + 1, inputDirection.y * 2 - 1);
                    break;
            }
            inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

            Vector2 controllerPosition = new Vector2(inputDirection.x * joyBackground.rectTransform.sizeDelta.x / 3.5f,
                                                   inputDirection.y * joyBackground.rectTransform.sizeDelta.y / 3.5f);
            joyController.rectTransform.anchoredPosition = controllerPosition;
            isInput = true;
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //터치 종료 시 컨트롤러 위치를 제자리로 옮긴다.
        joyController.rectTransform.anchoredPosition = Vector2.zero;

        inputDirection = Vector2.zero;
        isInput = false;
        Debug.Log("터치 종료");


    }
    //private void JoystickCircle(PointerEventData eventData)
    //{
    //    var inputPos = eventData.position - rectTransform.anchoredPosition;
    //    var inputVector = inputPos.magnitude < stickCircleRange ? inputPos : inputPos.normalized * stickCircleRange; //스틱범위
    //    stickCircle.anchoredPosition = inputVector;
    //    inputDirection = inputVector / stickCircleRange; //0-1정규화된 값으로 캐릭터에게 전달 
    //}
    //public void InputControlVector()
    //{
    //    캐릭터에게 입력벡터를 전달
    //    switch (joystickType)
    //    {
    //        case JoyStickType.Move:
    //            playerMove.Move(inputDirection);
    //            break;
    //        case JoyStickType.Rotate:
    //            playerMove.LookAround(inputDirection);
    //            break;
    //    }


    //    Debug.Log(inputDirection.x + "/" + inputDirection.y);
    //}
}
