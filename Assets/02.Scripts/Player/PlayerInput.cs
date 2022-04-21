using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //[SerializeField]
    //private Transform cam;

    private Image joyBackground; //조이스틱 배경UI
    private Image joyController; //이동값을 조정하는 내부 컨트롤러 UI
    public Vector2 inputDirection; //조이스틱 방향정보

    public enum JoyStickType { Move, Rotate }
    public JoyStickType joystickType;
    public float sensitivity; //조작민감도

    public bool isInput=false;
    public float horizontal { get { return inputDirection.x * sensitivity; } }
    public float vertical { get { return inputDirection.y * sensitivity; } }

    private void Awake()
    {
        joyBackground = GetComponent<Image>();
        joyController = transform.GetChild(0).GetComponent<Image>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {   //오브젝트를 터치했을 때 실행
        Debug.Log("터치 시작");
        //JoystickCircle(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {   //터치상태에서 드래그 했을 때 매 프레임마다 실행
        Debug.Log("드래그");
        inputDirection = Vector2.zero;

        //조이스틱의 위치가 어디에 있든 동일한 값을 연산하기 위해
        //'touchPosition'의 위치 값은 이미지의 현재 위치를 기준으로 얼마나 떨어져 있지는지에 따라 다르게 나옴
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joyBackground.rectTransform, eventData.position,eventData.pressEventCamera, out inputDirection))
        {
            //Debug.Log(inputDirection);
            //touchPosition 값의 정규화(0~1)
            //touchPosition을 이미지 크기로 나눔
            inputDirection.x = (inputDirection.x / joyBackground.rectTransform.sizeDelta.x);
            inputDirection.y = (inputDirection.y / joyBackground.rectTransform.sizeDelta.y);

            //touchPosition 값의 정규화(-1~1)
            switch (joystickType)
            {
                case JoyStickType.Move:
                    inputDirection = new Vector2(inputDirection.x * 2 - 1, inputDirection.y * 2 - 1);
                    break;
                case JoyStickType.Rotate:
                    inputDirection = new Vector2(inputDirection.x * 2 + 1, inputDirection.y * 2 - 1);
                    break;
            }
            //조이스틱 배경 이미지 밖으로 터치가 나갔을 때 -1~1보다 큰 값이 나올 수 있는데 이것을 normalized로 -1~1사이의 값만 나오게 한다
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
        //이동 방향 초기화
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
