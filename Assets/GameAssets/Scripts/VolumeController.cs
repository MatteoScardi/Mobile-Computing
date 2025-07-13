using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour {
    private AudioSource backgroundMusic;
    public Slider volumeScrollbar;

    void Start() {
        // Ottieni il componente AudioSource sull'oggetto a cui è attaccato questo script
        backgroundMusic = GetComponent<AudioSource>();

        // Assicurati che la scrollbar sia stata assegnata nell'Inspector
        if (volumeScrollbar == null) {
            Debug.LogError("Scrollbar del volume non assegnata allo script VolumeController!");
            enabled = false; // Disabilita lo script per evitare errori
            return;
        }

        // Imposta il valore iniziale della scrollbar in base al volume attuale (opzionale)
        if (backgroundMusic != null) {
            volumeScrollbar.value = backgroundMusic.volume;
        }

        // Aggiungi un listener all'evento "On Value Changed" della scrollbar
        volumeScrollbar.onValueChanged.AddListener(OnVolumeChanged);
    }

    // Questa funzione viene chiamata quando il valore della scrollbar cambia
    void OnVolumeChanged(float newValue) {
        // Imposta il volume dell'AudioSource al nuovo valore della scrollbar
        if (backgroundMusic != null) {
            backgroundMusic.volume = newValue;
        }
    }
}