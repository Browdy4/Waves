using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthCountText;

    private void OnEnable()
    {
        Player.OnUpdatePlayerHealth += UpdateHealthInfo;
    }
    private void OnDisable()
    {
        Player.OnUpdatePlayerHealth += UpdateHealthInfo;
    }

    public void UpdateHealthInfo(float healthCount)
    {
        _healthCountText.text = healthCount.ToString();
    }
}
