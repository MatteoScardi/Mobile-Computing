using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using TMPro;

using UnityEngine.SceneManagement;



public class GameoverScript : MonoBehaviour {

    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI newText;
    public RandomSpawner pickup;
    public EnemySpawner enemy;
    public GameObject experienceUI;


    public void Setup(int score, List<int> leaderboard) {

        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";

        int bestscore = leaderboard.Count > 0 ? leaderboard[0] : 0;
        highScoreText.text = "HighScore: " + bestscore.ToString() + " Points";



        if (score > bestscore) {
            newText.gameObject.SetActive(true);
        }

        if (experienceUI != null)
            experienceUI.SetActive(false);


        if (pickup != null)
            pickup.gameObject.SetActive(false);


        if (enemy != null)
            enemy.gameObject.SetActive(false);
    }


    public void RestartButtom() {
        SceneManager.LoadScene("Level01");

        if (experienceUI != null)
            experienceUI.SetActive(true);

    }



    public void ExitButtom() {
        SceneManager.LoadScene("MainMenu");

        if (experienceUI != null)
           experienceUI.SetActive(true);
    }

}