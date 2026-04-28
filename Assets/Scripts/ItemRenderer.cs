using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI  _description;

    public void UpdateItemRender(Item item)
    {
        _image.sprite = item.sprite;
        _name.text = item.name;
        _description.text = item.description;
    }
}
