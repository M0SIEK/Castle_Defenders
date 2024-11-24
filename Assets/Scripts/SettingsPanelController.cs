using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Toggle soundToggle;
    public Toggle musicToggle;
    public GameObject wavesController;

    private bool soundOn;
    private bool musicOn;
    private void Start()
    {
        settingsPanel.SetActive(false);
        soundOn = PlayerPrefs.GetInt("sound") == 0 ? false : true;
        musicOn = PlayerPrefs.GetInt("music") == 0 ? false : true;
        
        if(soundOn != soundToggle)
        {
            soundToggle.isOn = soundOn;
        }
        if(musicOn != musicToggle)
        {
            musicToggle.isOn = musicOn;
        }
    }
    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
        PauseGame();
    }

    public void CloseSettingsPanel()
    {
        settingsPanel.SetActive(false);
        if(wavesController.GetComponent<WavesController>().GetGameStarted())
        {
            ResumeGame();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log("Load " + sceneName);
        SceneManager.LoadScene(sceneName);   
    }

    public void OnSoundToggle()
    {
        soundOn = soundToggle.isOn;
        PlayerPrefs.SetInt("sound", soundOn ? 1 : 0);
    }

    public void OnMusicToggle()
    {
        musicOn = musicToggle.isOn;
        PlayerPrefs.SetInt("music", musicOn ? 1 : 0);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }
}
