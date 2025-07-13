using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;

// La classe 'Upgrade' rimane identica a prima, serve per definire i potenziamenti.
public class Upgrade {
    public string name;
    public string description;
    public System.Action applyEffect;

    public Upgrade(string name, string description, System.Action applyEffect) {
        this.name = name;
        this.description = description;
        this.applyEffect = applyEffect;
    }
}

public class LevelUpManager : MonoBehaviour {
    [Header("Riferimenti Esistenti")]
    public ExperienceLevelController expController;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;

    [Header("Riferimenti Armi e UI")]
    public GameObject levelUpPanel;
    public Transform weaponHolder; // Oggetto figlio del player dove spawnare l'arma
    public WeaponController currentWeapon; // Riferimento all'arma ATTIVA

    [Header("Bottoni UI")]
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;

    [Header("Prefab delle Armi")]
    public GameObject woodSwordPrefab;
    public GameObject ironSwordPrefab;
    public GameObject goldenSwordPrefab;
    public GameObject hammerPrefab; // Usa il prefab del martello

    private List<Upgrade> availableUpgrades = new List<Upgrade>();
    private int[] levelUpThresholds = { 10, 25, 45, 70, 100, 150, 175, 200}; // Punti per salire di livello
    private int currentLevel = 0;

    // Flag per evitare di offrire più volte lo stesso cambio arma
    private bool ironSwordUnlocked = false;
    private bool hammerUnlocked = false;
    private bool goldenSwordUnlocked = false;
    private float totalDamageBonus = 0f;
    private float totalRotationSpeedBonus = 0f;

    private List<Upgrade> masterUpgradeList = new List<Upgrade>();

    void Start() {
        levelUpPanel.SetActive(false);
        CreateMasterUpgradeList();
        SpawnWeapon(woodSwordPrefab);
    }

    void Update() {
        // Controlla se abbiamo raggiunto la soglia per il prossimo livello
        if (currentLevel < levelUpThresholds.Length && expController.currentExperience >= levelUpThresholds[currentLevel]) {
            LevelUp();
        }
    }

    void CreateMasterUpgradeList() {
        masterUpgradeList.Add(new Upgrade("Volonta' Ferro", "+20 Salute Massima", () => playerHealth.IncreaseMaxHealth(20f)));
        masterUpgradeList.Add(new Upgrade("Stivali Alati", "+10% Velocita' Movimento", () => playerMovement.IncreaseMoveSpeed(0.1f)));

        masterUpgradeList.Add(new Upgrade("Lama Affilata", "+2 Danno Arma", () => {
            totalDamageBonus += 2f;
            currentWeapon.SetDamage(totalDamageBonus);
        }));

        masterUpgradeList.Add(new Upgrade("Vortice d'Acciaio", "+10% Velocita' Rotazione", () => {
            totalRotationSpeedBonus += 0.10f;
            currentWeapon.SetRotationSpeed(totalRotationSpeedBonus);
        }));

        // --- ESEMPIO: Aggiungiamo altri potenziamenti per avere più scelta ---
        masterUpgradeList.Add(new Upgrade("Forza Bruta", "+4 Danno Arma", () => {
            totalDamageBonus += 4f;
            currentWeapon.SetDamage(totalDamageBonus);
        }));

        masterUpgradeList.Add(new Upgrade("Furia Berserker", "+20% Velocita' Rotazione", () => {
            totalRotationSpeedBonus += 0.20f;
            currentWeapon.SetRotationSpeed(totalRotationSpeedBonus);
        }));
    }


    void SpawnWeapon(GameObject weaponPrefab) {
        if (currentWeapon != null) {
            Destroy(currentWeapon.gameObject);
        }

        GameObject newWeaponObj = Instantiate(weaponPrefab, weaponHolder.position, weaponHolder.rotation, weaponHolder);
        currentWeapon = newWeaponObj.GetComponent<WeaponController>();

        currentWeapon.SetDamage(totalDamageBonus);
        currentWeapon.SetRotationSpeed(totalRotationSpeedBonus);
    }

    void LevelUp() {
        currentLevel++;
        Time.timeScale = 0f;
        levelUpPanel.SetActive(true);

        // Logica per offrire il cambio arma a punteggi specifici
        int currentPoints = expController.currentExperience;

        if (currentPoints >= 120 && !goldenSwordUnlocked) {
            PresentWeaponUpgrade(goldenSwordPrefab, "Forgia la Spada d'Oro!", () => { goldenSwordUnlocked = true;
                totalDamageBonus += 7f;
                currentWeapon.SetDamage(totalDamageBonus);
                totalRotationSpeedBonus += 0.20f; // Accumuliamo il bonus percentuale
                currentWeapon.SetRotationSpeed(totalRotationSpeedBonus); // Usiamo un nuovo metodo per la velocità
            });
        } else if (currentPoints >= 70 && !hammerUnlocked) {
            PresentWeaponUpgrade(hammerPrefab, "Impugna il Martello!", () => { hammerUnlocked = true;
                totalDamageBonus += 5f;
                currentWeapon.SetDamage(totalDamageBonus);
                totalRotationSpeedBonus += 0.10f; // Accumuliamo il bonus percentuale
                currentWeapon.SetRotationSpeed(totalRotationSpeedBonus); // Usiamo un nuovo metodo per la velocità
            });
            } else if (currentPoints >= 30 && !ironSwordUnlocked) {
            PresentWeaponUpgrade(ironSwordPrefab, "Forgia una Spada di Ferro!", () => { ironSwordUnlocked = true;
                totalDamageBonus += 2f;
                currentWeapon.SetDamage(totalDamageBonus);
                totalRotationSpeedBonus += 0.5f; // Accumuliamo il bonus percentuale
                currentWeapon.SetRotationSpeed(totalRotationSpeedBonus); // Usiamo un nuovo metodo per la velocità
            });


            } else {
            // Se non è il momento di un cambio arma, offre potenziamenti casuali
            PresentRandomUpgrades();
        }
    }

    void PresentRandomUpgrades() {
        // Controlla se abbiamo abbastanza potenziamenti unici da cui pescare
        int upgradesToPresent = Mathf.Min(3, masterUpgradeList.Count);
        if (upgradesToPresent <= 0) {
            Debug.LogWarning("Nessun potenziamento disponibile da presentare!");
            ResumeGame(); // Non possiamo mostrare nulla, quindi riprendiamo il gioco
            return;
        }

        // Pesca 3 (o meno, se non ce ne sono abbastanza) potenziamenti UNICI dalla lista master
        var randomUpgrades = masterUpgradeList.OrderBy(x => Random.value).Take(upgradesToPresent).ToList();

        // Configura i bottoni con i potenziamenti pescati
        ConfigureButton(upgradeButton1, randomUpgrades[0]);

        if (upgradesToPresent > 1) {
            ConfigureButton(upgradeButton2, randomUpgrades[1]);
        } else {
            upgradeButton2.gameObject.SetActive(false);
        }

        if (upgradesToPresent > 2) {
            ConfigureButton(upgradeButton3, randomUpgrades[2]);
        } else {
            upgradeButton3.gameObject.SetActive(false);
        }
    }

    // LevelUpManager.cs

    void PresentWeaponUpgrade(GameObject weaponPrefab, string description, System.Action onComplete) {
        // Configura il primo bottone per il cambio arma
        upgradeButton1.GetComponentInChildren<TextMeshProUGUI>().text = description;

        upgradeButton1.onClick.RemoveAllListeners();
        upgradeButton1.onClick.AddListener(() => {
            SpawnWeapon(weaponPrefab);
            onComplete?.Invoke();
            ResumeGame();
        });
        upgradeButton1.gameObject.SetActive(true);

        // Nasconde gli altri bottoni
        upgradeButton2.gameObject.SetActive(false);
        upgradeButton3.gameObject.SetActive(false);
    }

    void ConfigureButton(Button button, Upgrade upgrade) {
        // Funzione helper per non ripetere codice
        button.gameObject.SetActive(true);
    
        button.GetComponentInChildren<TextMeshProUGUI>().text = $"{upgrade.name}\n<size=45>{upgrade.description}</size>";

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SelectUpgrade(upgrade));
    }

    void SelectUpgrade(Upgrade upgrade) {
        upgrade.applyEffect();

        if (masterUpgradeList.Contains(upgrade)) {
            masterUpgradeList.Remove(upgrade);
        }
        
        ResumeGame();
    }

    void ResumeGame() {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}