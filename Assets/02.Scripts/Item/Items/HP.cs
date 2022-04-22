using UnityEngine;

public class HP : MonoBehaviour,IItem
{
    public float health = 50;

    public void Use(GameObject target)
    {
        Debug.Log("체력을 회복" + health);
    }

}
