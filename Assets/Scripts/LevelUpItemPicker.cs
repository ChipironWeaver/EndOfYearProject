using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpItemPicker : MonoBehaviour
{
    [SerializeField] private Item _itemTest;
    [Header("References")]
    [SerializeField] private GameObject _itemPanelPrefab;
    [SerializeField] private GameObject _blurPanel;
    [Header("Blue Fade")]
    [SerializeField] private int _blurAmount;
    [SerializeField] private float _blurDuration;
    [Header("Item Panel")]
    [SerializeField] private Color _targetColor;
    [SerializeField] private float _panelTime;
    [Header("Item Display")] 
    [SerializeField] private Vector3 _baseItemPanelScale;
    [SerializeField] private float _itemPanelFadeDuration;
    [SerializeField] private Ease _itemPanelEase;
    [Header("Values")]
    [SerializeField] private int _numberOfItems = 2;
    [SerializeField] private float _numberOfItemChoice = 4;
    [SerializeField] private List<GameObject> _itemsPanels = new List<GameObject>();

    private Image _blurImage;
    private Image _image;


    [Button]
    private void Reset()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        _blurImage = _blurPanel.GetComponent<Image>();
        //_blurMaterial.SetInteger("_Blur",5);
        _blurImage.enabled = false;
        _image = GetComponent<Image>();
    }
    
    [Button]
    private void FadeIn()
    {
        if(_numberOfItems <= 0) return;
        Time.timeScale = 0;
        _blurImage.enabled = true;
        _blurImage.material.SetInteger("_Blur", 0);
        Sequence fadeInSequence = DOTween.Sequence();
        fadeInSequence.Append(DOTween.To(()=> _blurImage.material.GetInteger("_Blur"), x=>_blurImage.material.SetInteger("_Blur",x), _blurAmount, _blurDuration));
        fadeInSequence.Join(_image.DOColor(_targetColor, _panelTime));
        fadeInSequence.SetUpdate(true);
        fadeInSequence.OnComplete(StartThePanel);
    }

    private void StartThePanel()
    {
        print("starting the panel");
        if (_itemsPanels.Count > 0)
        {
            foreach (GameObject itemPanel in _itemsPanels)
            {
                itemPanel.GetComponent<Button>().interactable = false;
            }
            
            
            HideItem(0);
            return;
        }
        if(_numberOfItems <= 0)
        {
            FadeOut();
            return;
        }
        _numberOfItems--;
        
        int numberOfItems = RandomRound(_numberOfItemChoice);
        print(numberOfItems);
        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject item = Instantiate(_itemPanelPrefab,transform);
            _itemsPanels.Add(item);
            item.transform.localScale = _baseItemPanelScale;
            item.GetComponent<Button>().onClick.AddListener(StartThePanel);
            item.GetComponent<ItemRenderer>().UpdateItemRender(_itemTest);
        }
        DisplayPanel(0);
    }

    private void HideItem(int index)
    {
        print("hiding the item");
        if (index >= _itemsPanels.Count)
        {
            foreach(GameObject itemPanel in _itemsPanels) Destroy(itemPanel);
            _itemsPanels.Clear();
            StartThePanel();
            return;
        }
        GameObject panel = _itemsPanels[index];
        Sequence panelFadeOut = DOTween.Sequence();
        panelFadeOut.SetTarget(panel);
        panelFadeOut.SetUpdate(true);
        panelFadeOut.Append(panel.transform.DOScale(_baseItemPanelScale, _itemPanelFadeDuration)).SetEase(_itemPanelEase);
        panelFadeOut.OnComplete(() => HideItem(index + 1));
        
    }

    private void DisplayPanel(int index)
    {
        if(index >= _itemsPanels.Count) return;
        GameObject panel = _itemsPanels[index];
        Sequence panelFadeIn = DOTween.Sequence();
        panelFadeIn.SetTarget(panel);
        panelFadeIn.SetUpdate(true);
        panelFadeIn.Append(panel.transform.DOScale(Vector3.one, _itemPanelFadeDuration)).SetEase(_itemPanelEase);
        panelFadeIn.OnComplete(() => DisplayPanel(index + 1));

    }
    
    private void FadeOut()
    {
        Sequence fadeInSequence = DOTween.Sequence();
        fadeInSequence.Append(DOTween.To(()=> _blurImage.material.GetInteger("_Blur"), x=>_blurImage.material.SetInteger("_Blur",x), 0, _blurDuration));
        fadeInSequence.Join(_image.DOColor(Color.clear, _panelTime));
        fadeInSequence.SetUpdate(true);
        fadeInSequence.OnComplete(() =>
        {
            Time.timeScale = 1;
            _blurImage.enabled = false;
        });

    }
    
    private int RandomRound(float num)
    {
        int number = Mathf.FloorToInt(num);
        float rest = num - number;
        if (rest > 0)
        {
            if (rest * 100 > Random.Range(0, 100))
            {
                number++;
            }
        }
        return number;
    }
}
