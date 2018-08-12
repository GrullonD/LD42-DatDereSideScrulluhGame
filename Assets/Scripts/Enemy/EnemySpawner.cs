using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] GameObject enemyGameObject;

    [SerializeField] float StartTimeBtwEnemies = 2.000f;
    private float timeBtwEnemies = 2.00f;

    [SerializeField] float maxSpawnSpread = 7f;
    [SerializeField] float minSpawnSpread = 3f;
    private Vector2 position;

    [SerializeField] GameObject leftEndOfMap;
    [SerializeField] GameObject rightEndOfMap;

    public int currentScore;

    // Use this for initialization
    void Start () {
        UpdateScore();
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

    public void UpdateScore() {
        currentScore = GameObject.FindWithTag("Player").GetComponent<PlayerScore>().playerScore;
        StartTimeBtwEnemies = NewSpawnTime(currentScore);
    }

    private void SpawnEnemy() {
        // Find where to spawn enemy
        float xSpawnPosition = Random.Range(transform.position.x + minSpawnSpread, transform.position.x + maxSpawnSpread);
        float sign = Random.value < 0.5f ? -1f : 1f; // select a negative or positive value
        xSpawnPosition = xSpawnPosition * sign;

        // Check if beyond edge of maps
        if( (xSpawnPosition <= leftEndOfMap.transform.position.x) || (xSpawnPosition >= rightEndOfMap.transform.position.x)) {
            xSpawnPosition = -xSpawnPosition;
        }

        // Set where to spawn enemy
        position = new Vector2(xSpawnPosition, transform.position.y);
        Instantiate(enemyGameObject, position, Quaternion.identity);
    }

    private float NewSpawnTime(int score) {

        float newSpawnTime = 0;

        if(score > 100) {
            newSpawnTime = StartTimeBtwEnemies - (StartTimeBtwEnemies/32);
        }
        else if (score > 200) {
            newSpawnTime = StartTimeBtwEnemies - StartTimeBtwEnemies / 16;
        }
        else if (score > 400) {
            newSpawnTime = StartTimeBtwEnemies - StartTimeBtwEnemies / 8;
        }
        else if (score > 800) {
            newSpawnTime = StartTimeBtwEnemies - StartTimeBtwEnemies / 4;
        }
        else if (score > 1600) {
            newSpawnTime = StartTimeBtwEnemies - StartTimeBtwEnemies / 2;
        }
        else if (score > 3200) {
            newSpawnTime = StartTimeBtwEnemies - StartTimeBtwEnemies / 2;
        }
        else {
            newSpawnTime = StartTimeBtwEnemies;
        }


        return newSpawnTime;
    }
}
