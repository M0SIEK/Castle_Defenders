using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        // Za�aduj pierwsz� scen� z gry
        SceneManager.LoadScene("EndOfRoundScene"); // Upewnij si�, �e Twoja scena gry nazywa si� "GameScene"
    }
}
