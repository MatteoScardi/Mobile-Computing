using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomSpawner : MonoBehaviour {
    [System.Serializable]
    public class SpawnSettings {
        public GameObject prefab;       // Prefab da generare
        public int numberOfObjects;     // Numero di oggetti da generare per questo prefab
        public float spawnInterval;     // Intervallo tra ogni spawn per questo prefab
    }

    [Header("Spawn Configuration")]
    public List<SpawnSettings> spawnSettingsList; // Lista di prefab e dei loro parametri
    public Transform minPoint;                    // Punto minimo per lo spawn
    public Transform maxPoint;                    // Punto massimo per lo spawn

    void Start() {
        foreach (var settings in spawnSettingsList) {
            StartCoroutine(SpawnObjectsCoroutine(settings));
        }
    }

    IEnumerator SpawnObjectsCoroutine(SpawnSettings settings) {
        if (settings.prefab == null || minPoint == null || maxPoint == null) {
            Debug.LogWarning("Assicurati di configurare il prefab e i punti min/max.");
            yield break;
        }

        for (int i = 0; i < settings.numberOfObjects; i++) {
            // Calcola una posizione casuale tra minPoint e maxPoint
            Vector3 randomPosition = new Vector3(
                Random.Range(minPoint.position.x, maxPoint.position.x),
                Random.Range(minPoint.position.y, maxPoint.position.y),
                Random.Range(minPoint.position.z, maxPoint.position.z)
            );

            // Genera l'oggetto nella posizione casuale
            Instantiate(settings.prefab, randomPosition, Quaternion.identity);

            // Aspetta lo spawnInterval specifico prima di generare il prossimo oggetto
            yield return new WaitForSeconds(settings.spawnInterval);
        }
    }
}
