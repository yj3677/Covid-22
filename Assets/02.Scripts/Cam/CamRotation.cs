using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotation : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform cam;
   

    
    void Update()
    {
        LookAround();
    }
    void LookAround()
    {
        if (Input.GetMouseButton(0))
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
}
