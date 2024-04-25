using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanelButtons;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _tutorialPanel;
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void OpenSettingsWindow()
    {
        _settingsPanel.SetActive(true);
        ShowMainPanel(false);
    }

    public void OpenTutorialWindow()
    {
        _tutorialPanel.SetActive(true);
        ShowMainPanel(false);
    }

    public void CloseTutorialPanel()
    {
        _tutorialPanel.SetActive(false);
        ShowMainPanel(true);
    }

    public void SaveSettings()
    {
        _settingsPanel.SetActive(false);
        _mainPanelButtons.SetActive(true);
    }

    private void ShowMainPanel(bool result)
    {
        _mainPanelButtons.SetActive(result);
    }
}
