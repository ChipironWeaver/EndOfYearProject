using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpItemPicker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _itemPanelPrefab;
    [SerializeField] private GameObject _blurPanel;
    [SerializeField] private UIExpBar _bar;
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
    private List<GameObject> _itemsPanels = new List<GameObject>();

    private int _numberOfItems;
    private Image _blurImage;
    private Image _image;

    private void OnEnable()
    {
        Actions.OnPlayerLevelUp += PanelLogic;
    }
    private void OnDisable()
    {
        Actions.OnPlayerLevelUp -= PanelLogic;
    }
    private void Start()
    {
        _blurImage = _blurPanel.GetComponent<Image>();
        _blurImage.enabled = false;
        _image = GetComponent<Image>();
    }

    public void PanelLogic()
    {
        int nextItems = RandomRound(PlayerInstance.playerStatisticController.playerStats.itemPerLevel);
        if(nextItems > 0 && _numberOfItems == 0)
        {
            _numberOfItems += nextItems;
            FadeIn();
        }
        else _numberOfItems += nextItems;
    }
    
    private void FadeIn()
    {
        if(_numberOfItems <= 0) return;
        _bar.LevelUp(true);
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        _blurImage.enabled = true;
        _blurImage.material.SetInteger("_Blur", 0);
        Sequence fadeInSequence = DOTween.Sequence();
        fadeInSequence.Append(DOTween.To(() => _blurImage.material.GetInteger("_Blur"),
            x => _blurImage.material.SetInteger("_Blur", x), _blurAmount, _blurDuration));
        fadeInSequence.Join(_image.DOColor(_targetColor, _panelTime));
        fadeInSequence.SetUpdate(true);
        fadeInSequence.OnComplete(StartThePanel);
        UIPausePanel.Instance.IsPaused = true;
    }

    private void StartThePanel()
    {
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
        
        int numberOfItems = RandomRound(PlayerInstance.playerStatisticController.playerStats.itemChoicePerLevel);
        List<Item> BlackList = new List<Item>();
        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject itemPanel = Instantiate(_itemPanelPrefab,transform);
            _itemsPanels.Add(itemPanel);
            itemPanel.transform.localScale = _baseItemPanelScale;
            itemPanel.GetComponent<Button>().onClick.AddListener(StartThePanel);
            Item item = LootPoolController.Instance.GetRandomItem(BlackList);
            itemPanel.GetComponent<ItemRenderer>().UpdateItemRender(item);
            BlackList.Add(item);
        }
        DisplayPanel(0);
    }

    private void HideItem(int index)
    {
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
            Cursor.lockState = CursorLockMode.Locked;
            _bar.LevelUp(false);
        });

        UIPausePanel.Instance.IsPaused = false;
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
