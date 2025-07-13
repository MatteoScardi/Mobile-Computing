using UnityEngine;

public class WeaponController : MonoBehaviour {
    // --- MODIFICA QUI ---
    // Rendiamo il pivotPoint pubblico e lo assegniamo dall'Inspector del prefab.
    // Non è più necessario che sia il LevelUpManager a impostarlo.
    public Transform pivotPoint;

    // Lo script EnemyDamager viene ancora trovato automaticamente
    private EnemyDamager damager;
    private float baseDamage;
    private float baseRotationSpeed;

    [Header("Stats")]
    public float rotationSpeed = 100f;

    void Awake() {
        damager = GetComponentInChildren<EnemyDamager>();
        if (damager == null) { /*...*/ }
        if (pivotPoint == null) { /*...*/ }

        // Memorizziamo i valori iniziali dell'arma
        if (damager != null) {
            baseDamage = damager.damageAmount;
        }
        baseRotationSpeed = this.rotationSpeed;
    }

    void Update() {
        // La logica di rotazione rimane la stessa, ma ora dipende
        // dal pivotPoint che abbiamo impostato nel prefab.
        if (pivotPoint != null) {
            transform.RotateAround(pivotPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    // Le funzioni per i potenziamenti non cambiano
    public void IncreaseDamage(float amount) {
        if (damager != null) {
            damager.damageAmount += amount;
        }
    }

    public void IncreaseRotationSpeed(float percentage) {
        rotationSpeed *= (1 + percentage);
    }

    public void SetDamage(float damageBonus) {
        if (damager != null) {
            damager.damageAmount = baseDamage + damageBonus;
            Debug.Log($"Danno impostato a: {damager.damageAmount} (Base: {baseDamage}, Bonus: {damageBonus})");
        }
    }

    public void SetRotationSpeed(float rotationSpeedBonusPercent) {
        this.rotationSpeed = baseRotationSpeed * (1 + rotationSpeedBonusPercent);
        Debug.Log($"Velocità rotazione impostata a: {this.rotationSpeed} (Base: {baseRotationSpeed}, Bonus: {rotationSpeedBonusPercent * 100}%)");
    }
}