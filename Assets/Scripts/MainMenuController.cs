using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void NewGame()
    {
        // Za�aduj pierwsz� scen� z gry
        SceneManager.LoadScene("EndOfRoundScene");
    }
}
