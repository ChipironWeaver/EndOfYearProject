using NaughtyAttributes;
using UnityEngine;
using Unity;

public class PlayerDamageDealer : MonoBehaviour
{
    public bool isTrueDamage;
    public float damage;
    [Tag]
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            print("boot");
            PlayerInstance.healthController.TakeDamage(damage,isTrueDamage);
        }
    }
}
