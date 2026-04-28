using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class PlayerLevelController : MonoBehaviour
{
    [CurveRange(0,0,100,200000)]
    public AnimationCurve expRequirementPetLevel;
    public float currentExp;
    public int currentLevel;
    public float expMultiplier;

    public void Start()
    {
        /*for(int i = 1; i < 100; i++)
        {
            if(i >1) print( i+" : " +expRequirementPetLevel.Evaluate(i) +" + " + (expRequirementPetLevel.Evaluate(i) - expRequirementPetLevel.Evaluate(i-1)));
            else print(expRequirementPetLevel.Evaluate(i));
        }*/
    }
    
    
    public void ChangeExp(float exp)
    {
        currentExp += exp * expMultiplier;
        if(currentLevel >= 100)
        {
            while (currentExp >= expRequirementPetLevel.Evaluate(100))
            {
                currentLevel++;
                currentExp -= expRequirementPetLevel.Evaluate(100) - expRequirementPetLevel.Evaluate(99);
                Actions.OnPlayerLevelUp?.Invoke();
                return;
            }
        }
        else
        {
            while (currentExp >= expRequirementPetLevel.Evaluate(currentLevel + 1))
            {
                currentLevel++;
                Actions.OnPlayerLevelUp?.Invoke();
                return;
            }
        }
        Actions.OnPlayerEXPGained?.Invoke();
    }
}
