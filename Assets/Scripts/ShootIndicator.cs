using UnityEngine;
using UnityEngine.UI;

public class ShootIndicator : MonoBehaviour
{
    private Image _image;
    void Start()
    {
        _image = GetComponent<Image>();
    }
    
    void Update()
    {
        _image.fillAmount = PlayerInstance.shootController.currentCooldown /
                            (1 / PlayerInstance.shootController.fireRate);
    }
}
