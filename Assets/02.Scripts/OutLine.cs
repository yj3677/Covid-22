using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutLine : MonoBehaviour
{
    bool isMouseover = false;

    Outline outline;
    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        if (isMouseover == false)
            outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
    private void OnMouseDown()
    {
        isMouseover = true;
    }
}
