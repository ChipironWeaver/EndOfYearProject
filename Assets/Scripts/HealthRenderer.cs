using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthRenderer : MonoBehaviour
{
    [Foldout("References"),SerializeField]
    private Image _bar;
    [Foldout("References"),SerializeField]
    private Image _edge;
    [Foldout("References"),SerializeField]
    private TextMeshProUGUI _text;
    
    [Foldout("Base Colors"),SerializeField]
    private Color _baseHealthColor = Color.white;
    [Foldout("Base Colors"),SerializeField]
    private Color _baseEdgeColor = Color.white;
    [Foldout("Base Colors"),SerializeField]
    private Color _baseTextColor = Color.white;
    
    [Foldout("Damage Colors"),SerializeField]
    private Color _damageHealthColor = Color.white;
    [Foldout("Damage Colors"),SerializeField]
    private Color _damageEdgeColor = Color.white;
    [Foldout("Damage Colors"),SerializeField]
    private Color _damageTextColor = Color.white;
    
    [Foldout("Heal Colors"),SerializeField]
    private Color _healHealthColor = Color.white;
    [Foldout("Heal Colors"),SerializeField]
    private Color _healEdgeColor = Color.white;
    [Foldout("Heal Colors"),SerializeField]
    private Color _healTextColor = Color.white;
    
    [Header("Timing")]
    [SerializeField] float _flashDuration = 0.5f;

    private bool _flashing;
    private float _flashTimer;
    private Color _flashColorHealth;
    private Color _flashColorEdge;
    private Color _flashColorText;

    private void Start()
    {
        _bar.color = _baseHealthColor;
        _edge.color = _baseEdgeColor;
        _text.color = _baseTextColor;
    }
    private void Update()
    {
        if (_flashing)
        {
            _flashTimer += Time.deltaTime;
            if (_flashTimer >= _flashDuration)
            {
                _flashing = false;
                _flashTimer = 0;
            }
            else
            {
                _bar.color = Color.Lerp(_flashColorHealth,_baseHealthColor, _flashTimer/_flashDuration);
                _edge.color = Color.Lerp(_flashColorEdge, _baseEdgeColor,_flashTimer/_flashDuration);
                _text.color = Color.Lerp(_flashColorText, _baseTextColor, _flashTimer/_flashDuration);
            }
        }
        
        _text.text = PlayerInstance.healthController.currentHealth.ToString("F0") + " / " + PlayerInstance.healthController.maxHealth.ToString("F0");
    }
    
    
    private void OnEnable()
    {
        HealthController.onPlayerDamage += Damage;
        HealthController.onPlayerHeal += Heal;
    }

    private void OnDisable()
    {
        HealthController.onPlayerDamage -= Damage;
        HealthController.onPlayerHeal -= Heal;
    }
    //_bar.fillAmount = PlayerInstance.healthController.currentHealth / PlayerInstance.healthController.maxHealth;
    [Button]
    private void Damage()
    {
        _text.text = PlayerInstance.healthController.currentHealth.ToString("F0") + " / " + PlayerInstance.healthController.maxHealth.ToString("F0");
        Sequence damageSequence = DOTween.Sequence();
        damageSequence.SetUpdate(true);
        damageSequence.Append(_bar.DOColor(_damageHealthColor, _flashDuration/2));
        damageSequence.Join(_edge.DOColor(_damageEdgeColor, _flashDuration/2));
        damageSequence.Join(_text.DOColor(_damageTextColor, _flashDuration/2));
        damageSequence.Join(_bar.DOFillAmount(PlayerInstance.healthController.currentHealth / PlayerInstance.healthController.maxHealth, _flashDuration)).SetEase(Ease.InOutQuart);
        damageSequence.Append(_bar.DOColor(_baseHealthColor, _flashDuration/2));
        damageSequence.Join(_edge.DOColor(_baseEdgeColor, _flashDuration/2));
        damageSequence.Join(_text.DOColor(_baseTextColor, _flashDuration/2));
        
    }
    [Button]
    private void Heal()
    {
        _text.text = PlayerInstance.healthController.currentHealth.ToString("F0") + " / " + PlayerInstance.healthController.maxHealth.ToString("F0");
        Sequence healSequence = DOTween.Sequence();
        healSequence.SetUpdate(true);
        healSequence.Append(_bar.DOColor(_healHealthColor, _flashDuration/2));
        healSequence.Join(_edge.DOColor(_healEdgeColor, _flashDuration/2));
        healSequence.Join(_text.DOColor(_healTextColor, _flashDuration/2));
        healSequence.Join(_bar.DOFillAmount(PlayerInstance.healthController.currentHealth / PlayerInstance.healthController.maxHealth, _flashDuration*0.75f)).SetEase(Ease.InOutQuart);
        healSequence.Append(_bar.DOColor(_baseHealthColor, _flashDuration/2));
        healSequence.Join(_edge.DOColor(_baseEdgeColor, _flashDuration/2));
        healSequence.Join(_text.DOColor(_baseTextColor, _flashDuration/2));
    }
}
