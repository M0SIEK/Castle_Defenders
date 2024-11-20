using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        // Za³aduj pierwsz¹ scenê z gry
        SceneManager.LoadScene("EndOfRoundScene"); // Upewnij siê, ¿e Twoja scena gry nazywa siê "GameScene"
    }
}
