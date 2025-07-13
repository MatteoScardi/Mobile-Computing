using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsNumber : MonoBehaviour
{
    
    public TextMeshProUGUI pointText;

    public float lifeTime;
    private float lifeCounter;

    public float floatSpeed = 1f;


    // Start is called before the first frame update
    void Start() {
        lifeCounter = lifeTime;
    }

    // Update is called once per frame
    void Update() {
        if (lifeCounter > 0) {
            lifeCounter -= Time.deltaTime;

            if (lifeCounter <= 0) {
                // Destroy(gameObject);
                PointsNumberController.instance.PlaceInPool(this);
            }
        }

        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }

    public void Setup(int pointsDisplay, int healDisplay) {
        lifeCounter = lifeTime;

        if (pointsDisplay > 0) {
            pointText.color = Color.yellow;
            pointText.text = "+" + pointsDisplay.ToString();
        } else {
            pointText.color = new Color(0.9686f, 0.1451f, 0.1451f);
            pointText.text = "+" + healDisplay.ToString();
        }
    }
}
