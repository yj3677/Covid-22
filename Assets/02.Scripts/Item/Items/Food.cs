using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IItem
{
    public float hungry = 30;
    public void Use(GameObject target)
    {
        Debug.Log("배고픔이 30줄었다" + hungry);
    }
}
