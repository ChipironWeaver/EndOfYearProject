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
    private bool _inLevelUp;

    private void OnEnable()
    {
        Actions.OnPlayerEXPGained += UpdateBar;
        Actions.OnPlayerEXPGained += Flash;
        
    }

    private void OnDisable()
    {
        Actions.OnPlayerEXPGained -= UpdateBar;
        Actions.OnPlayerEXPGained -= Flash;
    }
    
    
    private void Start()
    {
        _image = GetComponent<SlicedImage>();
        SetSequence(false);
    }
    
    private void Flash()
    {
        Sequence flashSequence = DOTween.Sequence();
        flashSequence.Append(_image.DOColor(_flashColor, _flashDuration)).SetEase(_flashEaseType);
        flashSequence.Append(_image.DOColor(_image.color, _flashDuration)).SetEase(_flashEaseType);
    }
    
    public void LevelUp(bool isStart)
    {
        if(isStart)
        {
            SetSequence(true);
            _image.fillAmount = 1;
            _inLevelUp = true;
        }
        else
        {
            SetSequence(false);
            UpdateBar();
            _inLevelUp = false;
        }
    }

    [Button]
    private void UpdateBar()
    {
        print(_inLevelUp);
        if(_inLevelUp) return;
        AnimationCurve levelCurve = PlayerInstance.playerLevelController.expRequirementPetLevel;
        int currentLevel = PlayerInstance.playerLevelController.currentLevel;
        float currentExp =  PlayerInstance.playerLevelController.currentExp;
        float fillAmount;
        if(currentLevel == 0)
        {
            fillAmount = currentExp  / levelCurve.Evaluate(currentLevel + 1);
        }
        else
        {
            fillAmount = (currentExp - levelCurve.Evaluate(currentLevel)) / (levelCurve.Evaluate(currentLevel + 1) - levelCurve.Evaluate(currentLevel));
        }
        _image.fillAmount = fillAmount;
    }

    private void SetSequence(bool isLevelUp)
    {
        DOTween.Clear();
        if (!isLevelUp)
        {
            Sequence _baseColorSequence = DOTween.Sequence();
            _image.color = _baseColors[0];

            for(int i = 0; i < _baseColors.Count; i++)
            {
                _baseColorSequence.Append(_image.DOColor(_baseColors[i], _colorDuration)).SetEase(_baseEaseType);
            }
            _baseColorSequence.SetLoops(-1);
        }
        else
        {
            _image.color = _levelUpColors[0];
            Sequence _levelUpSequence = DOTween.Sequence();
            for(int i = 0; i < _levelUpColors.Count; i++)
            {
                _levelUpSequence.Append(_image.DOColor(_levelUpColors[i], _levelUpColorDuration)).SetEase(_levelUpEaseType);
            }

            _levelUpSequence.Append(_image.DOColor(_levelUpColors[0], _levelUpColorDuration)).SetEase(_levelUpEaseType);
            _levelUpSequence.SetLoops(-1);
            _levelUpSequence.SetUpdate(true);
        }
    
    }
}
