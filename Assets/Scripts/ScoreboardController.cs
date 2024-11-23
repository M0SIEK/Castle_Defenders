using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardController : MonoBehaviour
{
    public GameObject scoreboardPanel;
    private void Start()
    {
        scoreboardPanel.SetActive(false);
    }
    public void ShowScoreboardPanel()
    {
        scoreboardPanel.SetActive(true);
    }

    public void CloseScoreboardPanel()
    {
        scoreboardPanel.SetActive(false);
    }
}
