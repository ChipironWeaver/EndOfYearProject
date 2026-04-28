using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Image _background;
    [SerializeField] private List<BackgroundPerRarity> _backgroundPerRarity;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI  _description;

    public void UpdateItemRender(Item item)
    {
        _image.sprite = item.sprite;
        _name.text = item.name;
        _description.text = item.description;
        foreach (var perRarity in _backgroundPerRarity)
        {
            if (perRarity.ItemRarity == item.rarity)
            {
                _background.sprite = perRarity.Background;
                break;
            }
        }
    }
}
[Serializable]
public class BackgroundPerRarity
{
    public Sprite Background;
    public ItemRarity ItemRarity;
}
