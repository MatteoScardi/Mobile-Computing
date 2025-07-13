using System.IO;

using UnityEngine;

using System.Collections.Generic;

using TMPro;



public class ExperienceLevelController : MonoBehaviour {

    public TextMeshProUGUI pointsText;
    public int currentExperience = 0;
    public List<int> leaderboard = new List<int>();
    public int maxEntries = 5;

    public GameoverScript gameoverScript;



    private void Start() {
        UpdatePointsText();
        LoadLeaderboardFromFile();

    }



    private void ResetController() {
        currentExperience = 0;
        UpdatePointsText();
    }



    public void GetExp(int amountToGet) {
        currentExperience += amountToGet;
        UpdatePointsText();

    }



    public void GameOver() {

        gameoverScript.Setup(currentExperience, leaderboard);
        UpdateLeaderboard(currentExperience);
        SaveLeaderboardToFile();

    }



    private void UpdateLeaderboard(int newScore) {

        leaderboard.Add(newScore);
        leaderboard.Sort((a, b) => b.CompareTo(a));

        if (leaderboard.Count > maxEntries) {
           leaderboard.RemoveAt(leaderboard.Count - 1);
        }

    }



    private void UpdatePointsText() {

        if (pointsText != null) {
            pointsText.text = currentExperience.ToString() + " points";
        } else {
            Debug.LogError("TextMeshProUGUI pointsText non assegnato!");
        }

    }



    private void SaveLeaderboardToFile() {

        if (leaderboard == null || leaderboard.Count == 0) {
            Debug.LogWarning("Leaderboard vuota, nessun salvataggio effettuato.");
            return;
        }



        string filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        string json = JsonUtility.ToJson(new LeaderboardData(leaderboard));
        File.WriteAllText(filePath, json);
        Debug.Log("Leaderboard salvata in: " + filePath);

    }



    private void LoadLeaderboardFromFile() {

        string filePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");

        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);

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



    [System.Serializable]

    public class LeaderboardData {
        public List<int> scores;

        public LeaderboardData(List<int> scores) {
            this.scores = scores;
        }

    }
}

