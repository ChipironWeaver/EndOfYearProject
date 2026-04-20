using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Utkaka.ScaleNineSlicer.UI;

public class UIExpBar : MonoBehaviour
{
    [Foldout("Base")]
    [SerializeField] private List<Color> _baseColors = new List<Color>();
    [Foldout("Base")]
    [SerializeField] private float _colorDuration = 0.5f;
    [Foldout("Base")]
    [SerializeField] private Ease _baseEaseType;
    
    [Foldout("Level Up")]
    [SerializeField] private List<Color> _levelUpColors = new List<Color>();
    [Foldout("Level Up")]
    [SerializeField] private float _levelUpColorDuration = 0.5f;
    [Foldout("Level Up")]
    [SerializeField] private Ease _levelUpEaseType;
    
    [Foldout("Flash")]
    [SerializeField] private Color _flashColor;
    [Foldout("Flash")]
    [SerializeField] private float _flashDuration;
    [Foldout("Flash")]
    [SerializeField] private Ease _flashEaseType;
    
    private SlicedImage _image;
    private Sequence _baseColorSequence;
    private Sequence _levelUpSequence;
    private void Start()
    {
        _image = GetComponent<SlicedImage>();
        _baseColorSequence = DOTween.Sequence();
        foreach (Color color in _baseColors)
        {
            _baseColorSequence.Append(_image.DOColor(color, _colorDuration)).SetEase(_baseEaseType);
        }
        _baseColorSequence.SetLoops(-1);
        
        _levelUpSequence = DOTween.Sequence();
        
        foreach (Color color in _levelUpColors)
        {
            _levelUpSequence.Append(_image.DOColor(color, _levelUpColorDuration)).SetEase(_levelUpEaseType);
        }
        _levelUpSequence.SetLoops(-1);
        _levelUpSequence.Pause();
        _image.color = _baseColors[^1];
    }

    [Button]
    public void Flash()
    {
        _baseColorSequence.Pause();
        Sequence flashSequence = DOTween.Sequence();
        flashSequence.Append(_image.DOColor(_flashColor, _flashDuration)).SetEase(_flashEaseType);
        flashSequence.Append(_image.DOColor(_image.color, _flashDuration)).SetEase(_flashEaseType);
        _baseColorSequence.Play();
    }
    
    [Button]
    public void LevelUp()
    {
        _baseColorSequence.Pause();
        _image.color = _levelUpColors[^1];
        _levelUpSequence.Restart();
    }
    [Button]
    public void Normal()
    {
        _levelUpSequence.Pause();
        _baseColorSequence.Restart();
    }
}
