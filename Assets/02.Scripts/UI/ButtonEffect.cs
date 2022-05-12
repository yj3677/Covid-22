using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    float time = 0;
    public GameObject StartBtn;
    public Text titleTxt;
    void Start()
    {
        Invoke("ActiveButton", 4);
    }

    void Update()
    {
        if (time<3f)
        {
            titleTxt.color = new Color(1, 1, 1, time / 3);

        }
        else
        {
            time = 0;
        }
        time += Time.deltaTime;
    }
    public void ActiveButton()
    {
        //(4초 뒤,) 버튼오브젝트 활성화
        StartBtn.SetActive(true);
    }
    public void ResetAnim()
    {
        titleTxt.color = new Color(1, 1, 1, 0);
        gameObject.SetActive(true);
        time = 0;
    }
}
