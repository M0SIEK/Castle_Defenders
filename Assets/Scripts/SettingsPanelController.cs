using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public GameObject settingsPanel;
    public Toggle SoundToggle;
    public Toggle MusicToggle;

    private bool soundOn;
    private bool musicOn;
    private void Start()
    {
        settingsPanel.SetActive(false);
        soundOn = PlayerPrefs.GetInt("sound") == 0 ? false : true;
        musicOn = PlayerPrefs.GetInt("music") == 0 ? false : true;
        
        if(soundOn != SoundToggle)
        {
            SoundToggle.isOn = soundOn;
        }
        if(musicOn != MusicToggle)
        {
            MusicToggle.isOn = musicOn;
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
        ResumeGame();
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
        soundOn = SoundToggle.isOn;
        PlayerPrefs.SetInt("sound", soundOn ? 1 : 0);
    }

    public void OnMusicToggle()
    {
        musicOn = MusicToggle.isOn;
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
}
