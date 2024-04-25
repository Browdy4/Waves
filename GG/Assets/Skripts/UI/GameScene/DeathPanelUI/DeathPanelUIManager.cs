using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPanelUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _deathPanel;

    private bool _revived = false;
    public bool Revived => _revived;

    public void ReloadScene()
    {
        SceneManager.LoadScene("Game");
        RevertTimeScale();

    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
        RevertTimeScale();
    }

    public void RevivePlayerAd()
    {
        RevertTimeScale();

        _revived = true;
    }

    private void RevertTimeScale()
    {
        _deathPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    //public void ReviveForAd()
    //{

    //}
}
