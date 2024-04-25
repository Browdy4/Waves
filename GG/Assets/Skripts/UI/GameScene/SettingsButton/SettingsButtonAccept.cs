using UnityEngine;

public class SettingsButtonAccept : MonoBehaviour
{
    [SerializeField] private GameObject _settingsPanel;

    public void OpenSettingsPanel()
    {
        _settingsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void AcceptSettings()
    {
        _settingsPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
