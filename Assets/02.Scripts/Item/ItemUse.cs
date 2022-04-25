using UnityEngine;

public class ItemUse : MonoBehaviour
{
    Item item;

    public int recoverHealth = 50;
    public int recoverStamina = 30;
    public int hungry = 30;
    PlayerState playerState;
    private void Start()
    {
        recoverHealth = item.num;
        recoverStamina = item.num;
        hungry = item.num;
        playerState = FindObjectOfType<PlayerState>();
    }

    public void HPUse()
    {

        Debug.Log("체력을 회복" + recoverHealth);
        playerState.currentHp += recoverHealth;


    }
    public void StaminaUse()
    {

        Debug.Log("스테미나 회복" + recoverStamina);
        playerState.currentStamina += item.num;


    }
    public void FoodEat()
    {

        Debug.Log("배고픔 회복" + hungry);
        playerState.currentHungry += item.num;


    }
}
