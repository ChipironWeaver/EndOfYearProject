using UnityEngine;
using UnityEngine.Events;

public class ItemPickupBehavior : MonoBehaviour
{
    [SerializeField] private UnityEvent onPickup;

    private bool _isPickedUp = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(!_isPickedUp) onPickup?.Invoke();
            Destroy(gameObject);
            _isPickedUp = true;
        }
    }
}
