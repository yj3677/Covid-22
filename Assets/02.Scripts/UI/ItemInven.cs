using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Item 
/// </summary>

public class OutLines : MonoBehaviour
{
    bool isMouseover = false;

    Outline outline;
    public GameObject inventory;
    private void Start()
    {
        outline = GetComponent<Outline>();
    }


    private void OnMouseEnter()
    {
     
    }

    private void OnMouseExit()
    {
       
    }
    private void OnMouseDown()
    {
        Debug.Log("Item");
        if (gameObject.tag=="Item")
        {
            outline.enabled = false;
            inventory.SetActive(true);
        }
        isMouseover = true;
    }
}
