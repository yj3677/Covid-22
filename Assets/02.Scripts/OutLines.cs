using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OutLines : MonoBehaviour
{
    bool isMouseover = false;

    Outline outline;
    private void Start()
    {
        outline = GetComponent<Outline>();
    }


    private void OnMouseEnter()
    {
        Debug.Log("HI");
        //if (isMouseover == false)
            //outline.enabled = true;
    }

    private void OnMouseExit()
    {
        //outline.enabled = false;
    }
    private void OnMouseDown()
    {
        Debug.Log("HI111");
        if (gameObject.tag=="Item")
        {
            outline.enabled = false;
        }
        isMouseover = true;
    }
}
