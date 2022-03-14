using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Play : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    Vector3 offset;

    private int rightFingerId;
    float halfScreenWidth;  //화면 절반만 터치하면 카메라 회전
    private Vector2 prevPoint;
    

    public Transform cam;
    public float cameraSensitivity;

    private Vector2 lookInput;
    private float cameraPitch; //pitch 시점


    void Start()
    {
        this.rightFingerId = -1;    //-1은 추적중이 아닌 손가락
        this.halfScreenWidth = Screen.width / 2;
        this.cameraPitch = 35f;
        offset = transform.position - player.transform.position;
        transform.position = player.transform.position + offset;
    }
    private void FixedUpdate()
    {
       
    }

    void Update()
    {
        transform.position = player.transform.position + offset;  //카메라
        GetTouchInput();
    }

    private void GetTouchInput()
    {
        //몇개의 터치가 입력되는가
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (touch.position.x > this.halfScreenWidth && this.rightFingerId == -1)
                    {
                        this.rightFingerId = touch.fingerId;
                        Debug.Log("오른쪽 손가락 입력");
                    }
                    break;

                case TouchPhase.Moved:

                    //이것을 추가하면 시점 원상태 버튼을 누를 때 화면이 돌아가지 않는다
                    if (!EventSystem.current.IsPointerOverGameObject(i))
                    {
                        if (touch.fingerId == this.rightFingerId)
                        {
                            
                            CamLookAround();
                        }
                    }
                    break;

                case TouchPhase.Stationary:

                    if (touch.fingerId == this.rightFingerId)
                    {
                        this.lookInput = Vector2.zero;
                    }
                    break;

                case TouchPhase.Ended:

                    if (touch.fingerId == this.rightFingerId)
                    {
                        this.rightFingerId = -1;
                        Debug.Log("오른쪽 손가락 끝");
                    }
                    break;

                case TouchPhase.Canceled:

                    if (touch.fingerId == this.rightFingerId)
                    {
                        this.rightFingerId = -1;
                        Debug.Log("오른쪽 손가락 끝");

                    }
                    break;
            }
        }
    }
    void CamLookAround()
    {
        
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); //이전값과 현재값의 차이
            Vector3 camAngle = cam.rotation.eulerAngles;
        

        float x = camAngle.x - mouseDelta.y;
            if (x < 180)
            {
                x = Mathf.Clamp(x, -70, -1);
            }
            else
            {
                x = Mathf.Clamp(x, 315, 359);
            }
            cam.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
        


    }

}

