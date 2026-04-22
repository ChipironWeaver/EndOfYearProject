using UnityEngine;

public class ExpCoinGiver : MonoBehaviour
{
    public void GiveExp(int expAmount)
    {
        if(expAmount > 0) PlayerInstance.playerLevelController.ChangeExp(expAmount);
    }

    public void GiveCoin(int coinAmount)
    {
        Debug.LogWarning("Giving Coin Not Implemented");
    }
}
