using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundEndController : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public HitPointsBarController hitPointsBar; // Odwo³anie do HitPointsBarController
    public WavesController wavesController;    // Odwo³anie do WavesController
    public GameObject roundEndPanel;           // Panel zakoñczenia rundy
    public ScoreboardController scoreboardController; // Odwo³anie do ScoreboardController

    private bool roundEnded = false;
    public static bool RoundEndPanelActive { get; internal set; } = false;

    private void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        roundEndPanel.SetActive(true);
    }

    private void Update()
    {
        if (!roundEnded)
        {
            CheckLoseCondition();
            CheckWinCondition();
        }
    }

    private void CheckLoseCondition()
    {
        if (hitPointsBar.slider.value <= 0)
        {
            ShowLoseScreen();
        }
    }

    private void CheckWinCondition()
    {
        if (wavesController.GetGameStarted() && wavesController.enemyNumberInWave <= 0 && wavesController.currentWave == wavesController.numberOfWaves)
        {
            ShowWinScreen();
        }
    }

    private void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        roundEnded = true;
        RoundEndPanelActive = true; // Ustaw flagê
        Time.timeScale = 0; // Zatrzymuje czas w grze
    }

    private void ShowWinScreen()
    {
        winScreen.SetActive(true);
        roundEnded = true;
        RoundEndPanelActive = true; // Ustaw flagê
        Time.timeScale = 0; // Zatrzymuje czas w grze
    }

    public void ContinueToNextLevel()
    {
        RoundEndPanelActive = false; // Wy³¹cz flagê
        Time.timeScale = 1; // Przywrócenie normalnego czasu gry
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentSceneIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
    }

    public void RestartRound()
    {
        RoundEndPanelActive = false; // Wy³¹cz flagê
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Prze³adowanie sceny
    }

    public void GoToMainMenu()
    {
        RoundEndPanelActive = false; // Wy³¹cz flagê
        Time.timeScale = 1; // Przywrócenie normalnego czasu gry
        SceneManager.LoadScene("MainMenuScene");
    }

    public void ShowScoreboard()
    {
        scoreboardController.ShowScoreboardPanel(); // Wyœwietlenie Scoreboard
    }
}
