using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{

    public int expValue;
    private bool movingToPlayer;
    public float moveSpeed;
    public float healValue;

    public float timeBetweenChecks = 0.2f;
    private float checkCounter;

    public ExperienceLevelController exp;
    private PlayerMovement player;

    // Start is called before the first frame update
    void Start() {
        player = PlayerHealth.instance.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToPlayer) {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        } else {
            checkCounter -= Time.deltaTime;

            if (checkCounter <= 0) {
                checkCounter = timeBetweenChecks;

                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange) {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            exp.GetExp(expValue);
            PlayerHealth.instance.healDamage(healValue);

            if (PlayerHealth.instance.currentHealth < 100)        
                PointsNumberController.instance.SpawnDamage(expValue, healValue, transform.position);
            else { 
                if (PlayerHealth.instance.currentHealth == 100 && expValue != 0)
                    PointsNumberController.instance.SpawnDamage(expValue, 0, transform.position);
            }

            Destroy(gameObject);
        }
    }

  

}
