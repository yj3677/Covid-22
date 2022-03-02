using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어 이동 구현
/// 
/// </summary>
public class Player : MonoBehaviour
{
    //플레이어 이동
    [SerializeField]
    int walkSpeed = 10;

    float xInput;
    float zInput;

    [SerializeField]
    float cameraRotationLimit = 45;
    float currentCameraRotationX = 0;
    float lookSensitivity = 1;

    Rigidbody rb;
    Camera theCamera;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        theCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        Walk();
        // CameraRotation();
    }

    void Walk()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");

        Vector3 xWalk = transform.right * xInput;
        Vector3 zWalk = transform.forward * zInput;

        Vector3 velocity = (xWalk + zWalk).normalized * walkSpeed;
        rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }
    void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity; //천천히 움직이기
        currentCameraRotationX += cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); //카메라 범위 가두기

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
    }
}
