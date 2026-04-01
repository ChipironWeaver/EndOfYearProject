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
        
        _bar.fillAmount = HealthController.Instance.currentHealth / HealthController.Instance.maxHealth;
        
        _text.text = HealthController.Instance.currentHealth.ToString("F0") + " / " + HealthController.Instance.maxHealth.ToString("F0");
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

    [Button]
    private void Damage()
    {
        _flashColorHealth = _damageHealthColor;
        _flashColorEdge = _damageEdgeColor;
        _flashColorText= _damageTextColor;
        _flashing = true;
    }
    [Button]
    private void Heal()
    {
        _flashColorHealth = _healHealthColor;
        _flashColorEdge = _healEdgeColor;
        _flashColorText= _healTextColor;
        _flashing = true;
    }
}
