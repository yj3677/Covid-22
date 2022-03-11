using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Play : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;

    private int rightFingerId;
    float halfScreenWidth;  //화면 절반만 터치하면 카메라 회전
    private Vector2 prevPoint;
    

    public Transform cameraTransform;
    public float cameraSensitivity;

    private Vector2 lookInput;
    private float cameraPitch; //pitch 시점


    void Start()
    {
        this.rightFingerId = -1;    //-1은 추적중이 아닌 손가락
        this.halfScreenWidth = Screen.width / 2;
        this.cameraPitch = 35f;

       
    }

  
    void Update()
    {
        //this.transform.position = Vector3.Lerp(this.transform.position, this.player.transform.position + new Vector3(0, this.transform.position.y, 0), this.speed);

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

                            //수평
                            this.prevPoint = touch.position - touch.deltaPosition;
                            this.transform.RotateAround(this.player.transform.position, Vector3.up, -(touch.position.x - this.prevPoint.x) * 0.2f);
                            this.prevPoint = touch.position;


                            //수직
                            this.lookInput = touch.deltaPosition * this.cameraSensitivity * Time.deltaTime;
                            this.cameraPitch = Mathf.Clamp(this.cameraPitch - this.lookInput.y, 10f, 35f);
                            this.cameraTransform.localRotation = Quaternion.Euler(this.cameraPitch, 0, 0);
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


}

