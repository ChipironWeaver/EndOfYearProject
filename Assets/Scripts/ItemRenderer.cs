using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ItemRenderer : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Image _background;
    [SerializeField] private Button _button;
    [SerializeField] private List<BackgroundPerRarity> _backgroundPerRarity;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI  _description;

    public void UpdateItemRender(Item item)
    {
        if (item.isVideo)
        {
            _image.enabled = false;
            _rawImage.enabled = true;
            _videoPlayer.targetTexture = new RenderTexture(300, 300, 24);
            _videoPlayer.clip = item.videoClip;
            _videoPlayer.Play();
            _videoPlayer.SetDirectAudioVolume(0,0.075f);
            _rawImage.texture = _videoPlayer.targetTexture;
        }
        else
        {
            _image.enabled = true;
            _rawImage.enabled = false;
            _image.sprite = item.sprite;
        }
        _name.text = item.itemName;
        _description.text = item.description;
        foreach (var perRarity in _backgroundPerRarity)
        {
            if (perRarity.ItemRarity == item.rarity)
            {
                _background.sprite = perRarity.Background;
                break;
            }
        }
        if(_button != null)
        {
            _button.onClick.AddListener(() => PlayerInstance.playerInventory.AddItem(item));
        }
    }
}
[Serializable]
public class BackgroundPerRarity
{
    public Sprite Background;
    public ItemRarity ItemRarity;
}
