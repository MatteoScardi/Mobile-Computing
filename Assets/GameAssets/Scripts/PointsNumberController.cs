using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsNumberController : MonoBehaviour
{
    public static PointsNumberController instance;

    private void Awake() {
        instance = this;
    }



    public PointsNumber numberToSpawn;
    public Transform numberCanvas;

    private List<PointsNumber> numberPool = new List<PointsNumber>();


    public void SpawnDamage(float expValue, float healValue, Vector3 location) {
        int roundedExp = Mathf.RoundToInt(expValue);
        int roundedHeal = Mathf.RoundToInt(healValue);

        //PointsNumber newDamage = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);

        PointsNumber newDamage = GetFromPool();

        newDamage.Setup(roundedExp, roundedHeal);
        newDamage.gameObject.SetActive(true);

        newDamage.transform.position = location;
    }

    public PointsNumber GetFromPool() {
        PointsNumber numberToOutput = null;

        if (numberPool.Count == 0) {
            numberToOutput = Instantiate(numberToSpawn, numberCanvas);
        } else {
            numberToOutput = numberPool[0];
            numberPool.RemoveAt(0);
        }

        return numberToOutput;
    }

    public void PlaceInPool(PointsNumber numberToPlace) {
        numberToPlace.gameObject.SetActive(false);

        numberPool.Add(numberToPlace);
    }
}
