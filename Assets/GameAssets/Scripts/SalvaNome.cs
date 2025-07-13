using UnityEngine;
using TMPro; // Assicurati di includere questo namespace per TextMeshPro
using UnityEngine.UI; // Assicurati di includere questo namespace per Button e InputField

public class SalvaTesto : MonoBehaviour {
    public TMP_InputField inputField; // Riferimento all'Input Field
    public Button salvaButton;      // Riferimento al Bottone
    public TMP_Text messaggioText;   // Riferimento al Text (opzionale)

    private string testoSalvato = "";

    void Start() {
        // Assicurati che gli elementi UI siano stati assegnati nell'Inspector
        if (inputField == null || salvaButton == null) {
            Debug.LogError("Assegna l'Input Field e il Bottone nell'Inspector!");
            enabled = false; // Disabilita lo script se mancano riferimenti
            return;
        }

        // Aggiungi un listener al click del bottone
        salvaButton.onClick.AddListener(SalvaIlTesto);

        // Inizializza il testo del messaggio (se presente)
        if (messaggioText != null) {
            messaggioText.text = "";
        }
    }

    void SalvaIlTesto() {
        if (inputField != null) {
            // Ottieni il testo inserito
            string testoInserito = inputField.text;

            // Verifica che il testo abbia esattamente 3 lettere
            if (testoInserito.Length <= 5 && IsSoloLettere(testoInserito)) {
                testoSalvato = testoInserito;
                Debug.Log("Testo salvato: " + testoSalvato);

                // Mostra un messaggio di conferma (se presente l'elemento Text)
                if (messaggioText != null) {
                    messaggioText.text = "Testo salvato: " + testoSalvato;
                }

                // Qui puoi aggiungere il codice per salvare il testo in modo persistente
                // (ad esempio, PlayerPrefs, file, database, ecc.)
            } else {
                Debug.LogWarning("Inserisci al massimo 5 lettere!");
                if (messaggioText != null) {
                    messaggioText.text = "Inserisci 5 lettere!";
                }
            }
        }
    }

    // Funzione per verificare se una stringa contiene solo lettere
    bool IsSoloLettere(string str) {
        foreach (char c in str) {
            if (!char.IsLetter(c)) {
                return false;
            }
        }
        return true;
    }
}