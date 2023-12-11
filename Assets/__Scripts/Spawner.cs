using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private int numSpawns = 1;
    public GameObject enemy;
    public int spawnAmountLow = 3;
    public int spawnAmountHigh = 6;
    public Vector3 position = Vector3.zero;
    public float spawnSpace = 0.1f;
    public bool vertical = true;

    private void OnTriggerEnter2D(Collider2D collision) {
        float spawns = Random.Range(spawnAmountLow, spawnAmountHigh);

        if (numSpawns > 0) {
            while (spawns > 0) {
                Instantiate(enemy, position, Quaternion.identity);
                if (vertical) {position.y -= spawnSpace;}
                else {position.x -= spawnSpace;}
                spawns--;
            }
        
        }
        numSpawns--;
    }
}
