using UnityEngine;

public class InvisibleWall : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        // Controlla se il GameObject che ha colpito il muro ha il tag "Player"
        if (!collision.gameObject.CompareTag("Player")) {
            // Ignora la collisione con tutti gli oggetti che non sono il Player
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        } else {
            // Blocca il Player (opzionale: aggiungi effetti o logica qui)
            //Debug.Log("Player bloccato dal muro invisibile!");
        }
    }
}
