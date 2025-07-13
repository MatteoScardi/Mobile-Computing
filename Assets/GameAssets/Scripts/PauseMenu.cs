using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;
    public Slider volumeScrollbar;
    private bool isPaused = false;
    private AudioSource backgroundMusic; // Riferimento all'AudioSource della musica

    void Start() {
        // Trova l'AudioSource della musica di sottofondo (assicurati che ce ne sia uno con un tag specifico o cercalo per nome)
        backgroundMusic = FindObjectOfType<AudioSource>();
        if (backgroundMusic == null) {
            Debug.LogWarning("AudioSource della musica di sottofondo non trovato!");
        }

        // Assicurati che il menu di pausa sia inizialmente nascosto
        if (pauseMenuUI != null) {
            pauseMenuUI.SetActive(false);
        }

        // Imposta il valore iniziale della scrollbar se abbiamo l'AudioSource
        if (volumeScrollbar != null && backgroundMusic != null) {
            volumeScrollbar.value = backgroundMusic.volume;
            // Aggiungi un listener per il cambio di valore della scrollbar
            volumeScrollbar.onValueChanged.AddListener(SetVolume);
        } else if (volumeScrollbar == null) {
            Debug.LogError("Scrollbar del volume non assegnata allo script PauseMenu!");
        }
    }

    void Update() {
        // Controlla se il tasto Esc è stato premuto
        if (Input.GetKeyDown(KeyCode.Escape)  && deathMenuUI.activeSelf == false) {
            TogglePause();
        }
    }

    public void TogglePause() {
        isPaused = !isPaused;

        if (pauseMenuUI != null) {
            pauseMenuUI.SetActive(isPaused);
        }

        // Metti in pausa o riprendi il tempo di gioco
        Time.timeScale = isPaused ? 0f : 1f;

        
    }

    // Funzione chiamata quando il valore della scrollbar cambia
    public void SetVolume(float volume) {
        if (backgroundMusic != null) {
            backgroundMusic.volume = volume;
        }
    }


    public void ResumeGame() {
        TogglePause();
    }


    public void QuitGame() {
        SceneManager.LoadScene("MainMenu");
    }
}