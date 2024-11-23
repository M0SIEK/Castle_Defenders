using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Runtime.Serialization;

[DataContract]
public class Scoreboard
{
    [DataMember]
    public string LevelName { get; set; }

    [DataMember]
    public string[] scoreboardContent { get; set; }
}

public class ScoreboardController : MonoBehaviour
{
    public GameObject scoreboardPanel;
    public TextMeshProUGUI scoreboardTableText;
    public int numberOfLevels;

    private string scoreboardTableHeader;
    private string scoreboardTableContent;
    private Scoreboard scoreboard;
    private string selectedLvlName;
    private void Start()
    {
        scoreboardPanel.SetActive(false);
        scoreboardTableHeader = "Nr.\t\tWynik\n";
        scoreboardTableContent = GetScoreboardTableContent();
        scoreboardTableText.GetComponent<TextMeshProUGUI>().text = scoreboardTableHeader + scoreboardTableContent;

        selectedLvlName = "LVL Test";
        string[] st = { "test1", "test2" };
        scoreboard = new Scoreboard { LevelName=selectedLvlName, scoreboardContent = st };
        WriteToJsonFile(scoreboard);
        Scoreboard scoreboard2 = ReadFromJsonFile(selectedLvlName);
        Debug.Log(scoreboard2.LevelName + " " + scoreboard2.scoreboardContent[0] + " " + scoreboard2.scoreboardContent[1]);
    }
    public void ShowScoreboardPanel()
    {
        scoreboardPanel.SetActive(true);
    }

    public void CloseScoreboardPanel()
    {
        scoreboardPanel.SetActive(false);
    }

    private string GetScoreboardTableContent()
    {
        string content = string.Empty;
        for (int i = 0; i<numberOfLevels; i++)
        {
            content += (i+1).ToString() + ". .............................................\n";
        }
        return content;
    }

    private void WriteToJsonFile(Scoreboard sb)
    {
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Scoreboard));
        using (MemoryStream ms = new MemoryStream())
        {
            serializer.WriteObject(ms, sb);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            File.WriteAllText(sb.LevelName + ".json", jsonString);
        }
    }

    private Scoreboard ReadFromJsonFile(string fileName)
    {
        string jsonString = File.ReadAllText(fileName + ".json");
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Scoreboard));
        
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
        {
            Scoreboard scoreboard = (Scoreboard)serializer.ReadObject(ms);
            return scoreboard;
        }
    }
}
