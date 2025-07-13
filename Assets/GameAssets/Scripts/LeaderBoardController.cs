using System.IO;
using UnityEngine;
using TMPro;
using System.Collections.Generic;



public class LeaderBoardController : MonoBehaviour {

    public TextMeshProUGUI leaderboardText;
    private List<int> leaderboard = new List<int>();



    private void LoadLeaderboardFromFile() {

        string filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            ExperienceLevelController.LeaderboardData data = JsonUtility.FromJson<ExperienceLevelController.LeaderboardData>(json);
            if (data != null) {
                leaderboard = data.scores;
                Debug.Log("Leaderboard caricata da: " + filePath);
            } else {
                Debug.LogError("File Leaderboard corrotto");
            }
        } else {
            Debug.Log("File Leaderboard non trovato, prima partita");
        }

    }



    void OnEnable() {
        LoadLeaderboardFromFile();
        DisplayLeaderboard();
    }



    private void DisplayLeaderboard() {
        string leaderboardString = "";

        for (int i = 0; i < leaderboard.Count; i++) {
           leaderboardString += leaderboard[i] + "\n"; // Aggiungi il punteggio alla stringa
        }
    
        leaderboardText.text = leaderboardString;
        leaderboardText.alignment = TextAlignmentOptions.Center; // Centra il testo
    }

}