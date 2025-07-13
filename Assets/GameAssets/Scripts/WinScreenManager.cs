using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Importante! Per usare TextMeshPro

public class WinScreenManager : MonoBehaviour {
   
    public TextMeshProUGUI pointsText; // Riferimento al testo che mostrerà i punti
    public ExperienceLevelController experienceController; // Riferimento al controller dell'esperienza

    // Questo metodo viene chiamato quando l'oggetto (il pannello della vittoria) viene attivato.
    // È perfetto per aggiornare la UI non appena appare.
    void OnEnable() {
        // Aggiorna il testo con il punteggio finale
        UpdatePointsText();

        // Mostra il cursore del mouse per permettere di cliccare sui bottoni
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UpdatePointsText() {
        // Controlla se i riferimenti sono stati assegnati per evitare errori
        if (pointsText == null) {
            Debug.LogError("Il campo 'pointsText' non è stato assegnato nello WinScreenManager!");
            return;
        }

        if (experienceController == null) {
            Debug.LogError("Il campo 'experienceController' non è stato assegnato nello WinScreenManager!");
            pointsText.text = "Punteggio non trovato!";
            return;
        }

        // Imposta il testo con il punteggio finale
        pointsText.text = "PUNTEGGIO: " + experienceController.currentExperience.ToString();
    }

    // --- Funzioni per i Bottoni ---

    /// <summary>
    /// Ricarica la scena di gioco per giocare di nuovo.
    /// </summary>
    public void ReplayGame() {
        // È fondamentale resettare il Time.timeScale prima di cambiare scena!
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level01"); // Assicurati che il nome della scena sia corretto
    }

    /// <summary>
    /// Torna alla scena del menu principale.
    /// </summary>
    public void ReturnToMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu"); // Assicurati che il nome della scena sia corretto
    }
}