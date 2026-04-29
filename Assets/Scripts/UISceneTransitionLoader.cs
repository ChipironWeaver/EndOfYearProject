using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;


public class UISceneTransitionLoader : MonoBehaviour
{
    [SerializeField] private bool _autoFadeIn;
    [SerializeField] private Color _autoFadeColor =  Color.white;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private List<SceneColorTransition> _sceneColorTransitions;
    [Serializable]
    public class SceneColorTransition
    {
        [SerializeField] public Color color = Color.black;
        [Scene][SerializeField] public string sceneName;
    }

    private Image _image;
    
    private void Start()
    {
        _image = GetComponent<Image>();
        if  (_autoFadeIn) FadeIn();
    }

    public void FadeOut(string sceneName)
    {
        Color color = _autoFadeColor;
        foreach (var sceneColorTransition in _sceneColorTransitions)
        {
            if (sceneColorTransition.sceneName == sceneName)
            {
                color = sceneColorTransition.color;
            }
        }
        Sequence FadeOutSequence = DOTween.Sequence();
        FadeOutSequence.Append(_image.DOColor(color, _fadeDuration));
        FadeOutSequence.OnComplete(() =>
        {
            Time.timeScale = 1;
            if (sceneName != null) SceneManager.LoadScene(sceneName);
        }) ;
        FadeOutSequence.SetUpdate(true);
    }

    private void FadeIn()
    {
        _image.color = _autoFadeColor;
        Sequence FadeInSequence = DOTween.Sequence();
        FadeInSequence.Append(_image.DOColor(Color.clear, _fadeDuration));
    }
}
