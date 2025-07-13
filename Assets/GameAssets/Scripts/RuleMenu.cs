using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleMenu : MonoBehaviour
{
    public void playGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level01");
    }
}
