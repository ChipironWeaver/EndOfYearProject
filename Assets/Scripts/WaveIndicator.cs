using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveIndicator : MonoBehaviour
{
    [SerializeField]
    private Image timing;
    [SerializeField]
    private TextMeshProUGUI enemyText;
    [SerializeField]
    private string enemyPrefix;
    [SerializeField]
    private TextMeshProUGUI waveText;

    private void Update()
    {
        timing.fillAmount = EnemySpawner.Instance.currentWaveTime / (EnemySpawner.Instance.EvaluateVector2(EnemySpawner.Instance.waveTimeRange, EnemySpawner.Instance.waveTime.Evaluate(EnemySpawner.Instance.currentWave)));
        waveText.text = EnemySpawner.Instance.currentWave.ToString("D2");
        enemyText.text = enemyPrefix + EnemySpawner.Instance.enemies.Count.ToString("D3");
    }
}
