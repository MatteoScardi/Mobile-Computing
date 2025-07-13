using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator animator;
    public float pickupRange = 1.5f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 moveInput = new Vector3(0f, 0f, 0f);

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        rb.velocity = moveInput * moveSpeed;

        if (moveInput != Vector3.zero) {
            animator.SetBool("Move", true);
        } else {
            animator.SetBool("Move", false);
        }
    }

    public void IncreaseMoveSpeed(float percentage) {
        // Aumenta la velocità di una percentuale (es. 0.1f per il 10%)
        moveSpeed *= (1 + percentage);
        Debug.Log("Velocità di movimento aumentata a: " + moveSpeed);
    }
}
