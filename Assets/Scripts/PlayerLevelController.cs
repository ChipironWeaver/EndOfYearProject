using NaughtyAttributes;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    [CurveRange(0,0,200000,100)]
    public AnimationCurve expPerLevel;
}
