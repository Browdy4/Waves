using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDeathPanel : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private Player _player;
    [SerializeField] private DeathPanelUIManager _deathPanelUIManager;
    [Header("AdDeathPanel")]
    [SerializeField] private GameObject _adDeathPanel;
    [Header("DeathPanel")]
    [SerializeField] private GameObject _deathPanel;

    private bool _revived => _deathPanelUIManager.Revived;

    private void OnEnable()
    {
        _player.OnPlayerDeath += OpenDeathPanelUI;
    }

    private void OnDisable()
    {
        _player.OnPlayerDeath -= OpenDeathPanelUI;
    }

    private void OpenDeathPanelUI()
    {
        if (!_revived)
        {
            _adDeathPanel.SetActive(true);
        }
        else
            _deathPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}
