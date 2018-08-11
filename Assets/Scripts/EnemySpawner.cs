using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject enemyGameObject;

    [SerializeField] float StartTimeBtwEnemies = 2f;
    private float timeBtwEnemies = 2f;

    [SerializeField] float maxSpawnSpread = 7f;
    [SerializeField] float minSpawnSpread = 3f;
    private Vector2 position;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timeBtwEnemies <= 0) {
            SpawnEnemy();
            timeBtwEnemies = StartTimeBtwEnemies;
        }
        else {
            timeBtwEnemies -= Time.deltaTime;
        }
    }

    private void SpawnEnemy() {
        // Find where to spawn enemy
        float xSpawnPosition = Random.Range(transform.position.x + minSpawnSpread, transform.position.x + maxSpawnSpread);
        float sign = Random.value < 0.5f ? -1f : 1f; // select a negative or positive value
        xSpawnPosition = xSpawnPosition * sign;

        // Set where to spawn enemy
        position = new Vector2(xSpawnPosition, transform.position.y);
        Instantiate(enemyGameObject, position, Quaternion.identity);
    }
}
