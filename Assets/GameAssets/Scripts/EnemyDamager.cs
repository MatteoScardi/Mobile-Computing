using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour {
    public float damageAmount;
    public float lifeTime = -1f; // <-- Imposta -1 come valore di default per "infinito"
    public float growSpeed = 4f;
    private Vector3 targetSize;
    public bool destroyParent;
    public bool shouldKnockBack;

    void Start() {
        targetSize = transform.localScale;
        if (growSpeed > 0) // Se c'è una velocità di crescita, parte da zero
        {
            transform.localScale = Vector3.zero;
        }
    }

    void Update() {
        if (transform.localScale != targetSize) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);
        }

        // Solo se lifeTime è stato impostato a un valore positivo, inizia il conto alla rovescia
        if (lifeTime > 0) {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0) {
                // Riduci la scala per un effetto di scomparsa
                targetSize = Vector3.zero;
                if (transform.localScale.x == 0f) {
                    if (destroyParent) {
                        Destroy(transform.parent.gameObject);
                    } else {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Enemy") {
            collision.GetComponent<EnemyMovement>().TakeDamage(damageAmount, shouldKnockBack);
        }
    }
}