using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePanel : MonoBehaviour
{
    public static UIPausePanel Instance { get; private set; }
    
    public bool IsPaused;
    
    
    [SerializeField] private Image _blurImage;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _fadeOutDuration;
    [SerializeField] private AnimationCurve _EaseCurve;
    [SerializeField] private int _blurAmount;
    [SerializeField] private Vector3 _initialPanelSize;
    [SerializeField] private float _panelOffset;
    [SerializeField] private Vector3 _panelDirection;
    [SerializeField] private List<Button> _buttons;

    private Image _image;
    private bool _canCancel;

    private void OnEnable()
    {
        Singleton();
    }
    
    private void Start()
    {
        _image = GetComponent<Image>();
        transform.localScale = _initialPanelSize;
        transform.localPosition = _panelDirection * _panelOffset;
        foreach (Button button in _buttons) button.interactable = false; 
    }
    private void ShowPauseMenu()
    {
        IsPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;

        _blurImage.enabled = true;
        _blurImage.material.SetInteger("_Blur", 0);

        Sequence fadeInSequence = DOTween.Sequence();
        fadeInSequence.SetUpdate(true);
        fadeInSequence.Append(DOTween.To(() => _blurImage.material.GetInteger("_Blur"),
            x => _blurImage.material.SetInteger("_Blur", x), _blurAmount, _fadeInDuration));
        fadeInSequence.Join(transform.DOScale(Vector3.one, _fadeInDuration)).SetEase(_EaseCurve);
        fadeInSequence.Join(_image.rectTransform.DOLocalMove(Vector3.zero, _fadeInDuration).SetEase(_EaseCurve));
        fadeInSequence.OnComplete(() =>
            {
                foreach (Button button in _buttons) button.interactable = true;
                _canCancel = true;
            }
        );
    }

    private void HidePauseMenu()
    {
        foreach (Button button in _buttons) button.interactable = false; 
        
        Cursor.lockState = CursorLockMode.Locked;

        Sequence fadeOutSequence = DOTween.Sequence();
        fadeOutSequence.SetUpdate(true);
        fadeOutSequence.Append(DOTween.To(() => _blurImage.material.GetInteger("_Blur"),
            x => _blurImage.material.SetInteger("_Blur", x), 0, _fadeOutDuration));
        fadeOutSequence.Join(transform.DOScale(_initialPanelSize, _fadeOutDuration).SetEase(_EaseCurve));
        fadeOutSequence.Join(_image.rectTransform.DOLocalMove(_panelDirection * _panelOffset, _fadeOutDuration).SetEase(_EaseCurve));
        fadeOutSequence.OnComplete(() =>
            {
                Time.timeScale = 1;
                _blurImage.enabled = false;
                IsPaused = false;
                _canCancel = false;
            }
        );
    }

    public void OnCancel()
    {
        if (!IsPaused) ShowPauseMenu();
        else if(_canCancel) HidePauseMenu();
    }
    
    void Singleton()
    {
        if (Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}

