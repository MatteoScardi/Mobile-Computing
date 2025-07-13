using UnityEngine;
using TMPro;

public class ExperienceDisplay : MonoBehaviour {
    public TextMeshProUGUI experienceText;
    public ExperienceLevelController exp;
    void Start() {
        // Controlla se esiste il controller prima di aggiornare il punteggio
        if (exp == null) {
            Debug.LogError("ExperienceLevelController non trovato! Assicurati di avviare il gioco dalla scena MainMenu.");
            return;
        }

        UpdateExperienceText();
    }

    void Update() {
        // Aggiorna il testo solo se il controller esiste
        if (exp != null) {
            UpdateExperienceText();
        }
    }

    void UpdateExperienceText() {
        experienceText.text = "EXP: " + exp.currentExperience.ToString();
    }
}
