using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundEndController : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public HitPointsBarController hitPointsBar;  // Odwo�anie do HitPointsBarController
    public WavesController wavesController;     // Odwo�anie do WavesController (dodane)
    private bool roundEnded = false;

    private void Start()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void Update()
    {
        if (!roundEnded)
        {
            CheckLoseCondition();
            CheckWinCondition(); // Dodane
        }
    }

    private void CheckLoseCondition()
    {
        if (hitPointsBar.slider.value <= 0)
        {
            ShowLoseScreen();
        }
    }

    private void CheckWinCondition() // Nowa metoda
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
        Time.timeScale = 0; // Zatrzymuje czas w grze
    }

    private void ShowWinScreen()
    {
        winScreen.SetActive(true);
        roundEnded = true;
        Time.timeScale = 0; // Zatrzymuje czas w grze
    }

    public void ContinueToNextLevel()
    // Sceny poziom�w musz� by� w odpowiedniej kolejno�ci!!!!!
    {
        Time.timeScale = 1; // Przywr�cenie normalnego czasu gry

        // Pobierz indeks aktualnej sceny
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Za�aduj nast�pn� scen� na podstawie indeksu
        int nextLevelIndex = currentSceneIndex + 1;

        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex);
        }
    }

    public void RestartRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Prze�adowanie sceny
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1; // Przywr�cenie normalnego czasu gry
        SceneManager.LoadScene("MainMenuScene");
    }
}
