using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawn, maxSpawn;

    private Transform target;
    private GameObject player;

    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    public int checkPerFrame;
    private int enemyToCheck;

    public List<WaveInfo> waves;

    private int currentWave;
    private float waveCounter;

    public GameObject winScreen; // Trascina qui la tua UI di vittoria dall'Inspector
    private bool gameHasEnded = false;


    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn;

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) {
            target = player.GetComponent<Transform>();
        }

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 4f;
        currentWave--;
        goToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        /*spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0) {
            spawnCounter = timeToSpawn;
            GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
            spawnedEnemies.Add(newEnemy);
        }*/
        if (gameHasEnded) {
            return;
        }

        if (PlayerHealth.instance.gameObject.activeSelf) {
            if (currentWave < waves.Count) {
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0) {
                    goToNextWave();
                }

                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0) {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;
                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                    spawnedEnemies.Add(newEnemy);
                }
            } else 
              {
                // Siamo entrati qui perché tutte le ondate sono terminate (currentWave >= waves.Count)
                // Ora dobbiamo solo aspettare che i nemici rimasti muoiano.
                CheckWinCondition();
            }
        }

        transform.position = target.position;

        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget) {
            if (enemyToCheck < spawnedEnemies.Count) {
                if (spawnedEnemies[enemyToCheck] != null) {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance) {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    } else {
                        checkTarget++;
                    }
                } else {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            } else {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 SelectSpawnPoint() {
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;

        if (spawnVerticalEdge) {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > 0.5f) {
                spawnPoint.x = maxSpawn.position.x;
            } else {
                spawnPoint.x = minSpawn.position.x;
            }
        } else {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > 0.5f) {
                spawnPoint.y = maxSpawn.position.y;
            } else {
                spawnPoint.y = minSpawn.position.y;
            }
        }
        return spawnPoint;
    }


    public void goToNextWave() {
        currentWave++;

        if (currentWave < waves.Count) {
            // Se ci sono ancora ondate, imposta i contatori
            waveCounter = waves[currentWave].waveLenght;
            spawnCounter = waves[currentWave].timeBetweenSpawns;
        } else {
            // Se le ondate sono finite, non fare nulla.
            // L'Update() noterà che currentWave >= waves.Count e inizierà a controllare la vittoria.
            Debug.Log("Tutte le ondate sono terminate! In attesa della vittoria...");
        }
    }

    void CheckWinCondition() {
        // Pulisci la lista da nemici che sono stati distrutti da altre parti del codice (es. dal giocatore)
        // Usiamo un ciclo for all'indietro per rimuovere elementi da una lista in modo sicuro.
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--) {
            if (spawnedEnemies[i] == null) {
                spawnedEnemies.RemoveAt(i);
            }
        }

        // Dopo aver pulito la lista, controlla se è vuota.
        if (spawnedEnemies.Count == 0) {
            // Se non ci sono più nemici, il giocatore ha vinto!
            PlayerWon();
        }
    }

    void PlayerWon() {
        gameHasEnded = true; // Imposta il flag per non eseguire di nuovo questa logica

        Debug.Log("HAI VINTO!");

        // Attiva la schermata di vittoria
        if (winScreen != null) {
            winScreen.SetActive(true);
        }

        // Ferma il tempo per "congelare" la scena
        Time.timeScale = 0f;

        // Disattiva questo spawner per fermare ogni ulteriore logica
        this.enabled = false;
    }

  

}


[System.Serializable]
public class WaveInfo {
    public GameObject enemyToSpawn;
    public float waveLenght = 10f;
    public float timeBetweenSpawns = 1f;



}

