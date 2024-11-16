using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.SceneManagement;

public class SettingsPanelController : MonoBehaviour
{
    public GameObject settingsPanel;
    private void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
        PauseGame();
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
        ResumeGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
