using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundEndController : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    public HitPointsBarController hitPointsBar;  // Odwo³anie do HitPointsBarController

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
        }
    }

    private void CheckLoseCondition()
    {
        if (hitPointsBar.slider.value <= 0)
        {
            ShowLoseScreen();
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

    public void RestartRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Prze³adowanie sceny
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;
//using UnityEngine.SceneManagement;

//public class RoundEndController : MonoBehaviour
//{
//    public GameObject winScreen;
//    public GameObject loseScreen;
//    public HitPointsBarController hitPointsBar;  // Odwo³anie do HitPointsBarController
//    public WavesController wavesController;
//    public TextMeshProUGUI timerText;
//    public TextMeshProUGUI enemiesLeftText;
//    public TextMeshProUGUI waveText;

//    private bool roundEnded = false;

//    private void Start()
//    {
//        winScreen.SetActive(false);
//        loseScreen.SetActive(false);
//    }

//    private void Update()
//    {
//        if (!roundEnded)
//        {
//            CheckLoseCondition();
//            CheckWinCondition();
//        }
//    }

//    private void CheckLoseCondition()
//    {
//        if (hitPointsBar.slider.value <= 0)
//        {
//            ShowLoseScreen();
//        }
//    }

//    private void CheckWinCondition()
//    {
//        if (wavesController.currentWave >= wavesController.numberOfWaves && GetEnemyCount() == 0)
//        {
//            ShowWinScreen();
//        }
//    }

//    private void ShowLoseScreen()
//    {
//        loseScreen.SetActive(true);
//        roundEnded = true;
//        Time.timeScale = 0;
//    }

//    private void ShowWinScreen()
//    {
//        winScreen.SetActive(true);
//        roundEnded = true;
//        Time.timeScale = 0;
//    }

//    public void RestartRound()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//        // Ukrywanie ekranów koñca gry
//        winScreen.SetActive(false);
//        loseScreen.SetActive(false);

//        // Ustawiamy flagê zakoñczenia rundy na false, aby kontynuowaæ grê
//        roundEnded = false;

//        // Wznawiamy up³yw czasu
//        Time.timeScale = 1;

//        // Resetujemy ¿ycie gracza
//        ResetHealth();

//        // Resetujemy kontroler fal do pocz¹tkowego stanu
//        ResetWaveState();

//        // Usuwamy pozosta³ych przeciwników z mapy
//        DestroyEnemies();

//        // Resetujemy timer
//        RestartTimer();

//        // Uruchamiamy pierwsz¹ falê natychmiast po resecie
//        StartWaveImmediately();
//    }

//    private void DestroyEnemies()
//    {
//        // Usuwamy wszystkie przeciwniki znajduj¹ce siê w enemiesParentObject
//        foreach (Transform enemy in wavesController.enemiesParentObject.transform)
//        {
//            Destroy(enemy.gameObject);
//        }
//    }

//    private void ResetHealth()
//    {
//        // Zak³adaj¹c, ¿e pe³ne zdrowie gracza to 1000
//        float maxHealth = 1000f;

//        // Resetujemy wartoœæ zdrowia gracza w zmiennej globalnej (np. playerHitPoints) do pocz¹tkowej wartoœci
//        EnemyController.playerHitPoints = maxHealth;

//        // Resetujemy pasek zdrowia do pe³nej wartoœci
//        hitPointsBar.UpdateHitPointsBar(maxHealth, maxHealth);
//    }

//    private void ResetWaveState()
//    {
//        // Resetujemy zmienne fal w WavesController do ich wartoœci pocz¹tkowych
//        wavesController.currentWave = 0;
//        wavesController.enemyNumberInWave = 0;

//        // Resetujemy wszystkie liczniki przeciwników do pocz¹tkowych wartoœci
//        wavesController.skeletonLVL1CurrentWaveNumber = wavesController.skeletonLVL1InitialNumber;
//        wavesController.skeletonLVL2CurrentWaveNumber = wavesController.skeletonLVL2InitialNumber;
//        wavesController.goblinLVL1CurrentWaveNumber = wavesController.goblinLVL1InitialNumber;
//        wavesController.goblinLVL2CurrentWaveNumber = wavesController.goblinLVL2InitialNumber;
//        wavesController.mushroomLVL1CurrentWaveNumber = wavesController.mushroomLVL1InitialNumber;

//        // Obliczamy now¹ liczbê przeciwników w nastêpnej fali
//        wavesController.enemyNumberInNextWave = wavesController.skeletonLVL1CurrentWaveNumber +
//                                                wavesController.skeletonLVL2CurrentWaveNumber +
//                                                wavesController.goblinLVL1CurrentWaveNumber +
//                                                wavesController.goblinLVL2CurrentWaveNumber +
//                                                wavesController.mushroomLVL1CurrentWaveNumber;

//        // Aktualizujemy tekst fali i liczbê przeciwników
//        waveText.text = wavesController.currentWave.ToString();
//        UpdateEnemiesLeftText(wavesController.enemyNumberInWave);
//    }

//    private void RestartTimer()
//    {
//        // Resetujemy timer w WavesController
//        wavesController.timeToNextWave = wavesController.timeBetweenWavesInSeconds;
//        timerText.text = "00:00";  // Ustawiamy timer na pocz¹tkowy stan
//    }

//    private void UpdateEnemiesLeftText(int enemiesLeft)
//    {
//        enemiesLeftText.text = enemiesLeft.ToString();
//    }

//    public int GetEnemyCount()
//    {
//        return wavesController.enemyNumberInWave;
//    }

//    private void StartWaveImmediately()
//    {
//        // Uruchamiamy metodê SummonNextWave w WavesController, aby natychmiast zacz¹æ falê
//        wavesController.SummonNextWave(
//            wavesController.skeletonLVL1CurrentWaveNumber,
//            wavesController.skeletonLVL2CurrentWaveNumber,
//            wavesController.goblinLVL1CurrentWaveNumber,
//            wavesController.goblinLVL2CurrentWaveNumber,
//            wavesController.mushroomLVL1CurrentWaveNumber
//        );
//    }
//}
