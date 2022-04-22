using UnityEngine;

public class Stamina : MonoBehaviour, IItem
{
    public int stamina = 50;
    public void Use(GameObject target)
    {
        Debug.Log("스테미너를 50회복 했다" + stamina);
    }
}
