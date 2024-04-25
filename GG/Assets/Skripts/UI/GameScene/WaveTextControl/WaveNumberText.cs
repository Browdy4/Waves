using TMPro;
using UnityEngine;

public class WaveNumberText : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;

    [SerializeField] private TextMeshProUGUI _textWaveNumber;

    [Header("DefaultColorText")]
    [SerializeField] private Color _defaultColor;

    [Header("IsWaveStartedColorText")]
    [SerializeField] private Color _activeColor;

    private int _currentIndexWave = 0;

    private void OnEnable()
    {
        _waveManager.OnWaveStarted += WaveStart;
        _waveManager.OnWaveEnded += WaveEnd;
    }

    private void OnDisable()
    {
        _waveManager.OnWaveStarted -= WaveStart;
        _waveManager.OnWaveEnded -= WaveEnd;
    }

    private void WaveStart()
    {
        _currentIndexWave++;

        _textWaveNumber.color = _activeColor;
        _textWaveNumber.text = $"Волна: {_currentIndexWave} / 7";
    }

    private void WaveEnd()
    {
        _textWaveNumber.color = _defaultColor;
    }
}
