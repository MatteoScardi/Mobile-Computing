using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public float currentHealth, maxHealth;
    public static PlayerHealth instance;
    public Slider healthSlider;

    public ExperienceLevelController exp;

    private void Awake() {
        instance = this;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(float damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            gameObject.SetActive(false);

            if (exp != null) {
                exp.GameOver();
            } else {
                Debug.LogError("ExperienceLevelController non trovato!");
            }
        }

        healthSlider.value = currentHealth;
    }

    public void healDamage(float heal) {
        currentHealth += heal;

        if (currentHealth > maxHealth) {
            currentHealth = 100;
        }

        healthSlider.value = currentHealth;
    }

    public void IncreaseMaxHealth(float amount) {
        maxHealth += amount;
        currentHealth += amount; // Guarisce anche il giocatore

        // Aggiorna la slider della salute
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        Debug.Log("Salute massima aumentata a: " + maxHealth);
    }
}