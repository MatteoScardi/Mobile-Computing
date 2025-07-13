using UnityEngine;

public class ChangeWeapon : MonoBehaviour {
    public GameObject woodSword;
    public GameObject ironSword;
    public GameObject goldenSword;
    public GameObject Hammer;

    private int lastExperience = -1;

    public ExperienceLevelController exp;

    void Start() {
        if (exp == null) {
            Debug.LogError("ExperienceLevelController non trovato in Start()!");
            return;
        }
        SetWeapon(0); // Imposta la spada di legno come arma iniziale
    }

    void Update() {
        int currentPoints = exp.currentExperience;

        if (currentPoints != lastExperience) {
            SetWeapon(currentPoints);
            lastExperience = currentPoints;
        }
    }

    private void SetWeapon(int points) {
        woodSword.SetActive(false);
        ironSword.SetActive(false);
        goldenSword.SetActive(false);
        Hammer.SetActive(false);

        switch (points) {
            case >= 120:
                goldenSword.SetActive(true);
                break;
            case >= 70:
                Hammer.SetActive(true);
                break;
            case >= 30:
                ironSword.SetActive(true);
                break;
            default:
                woodSword.SetActive(true);
                break;
        }
    }
}