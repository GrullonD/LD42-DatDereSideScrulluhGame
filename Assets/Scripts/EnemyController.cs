using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] GameObject Target;
    [SerializeField] float speed = 2f;

    [SerializeField] int stoppingDistance = 5;     // How far to stay away from target
    [SerializeField] float targetDistanceSpread = 2f;   // Spread of different enemies from target

    private Transform target;
    private float targetDistance;

    [SerializeField] GameObject projectile;
    [SerializeField] float StartTimeBtwShots = 2f;
    private float timeBtwShots = 2f;
    

	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Player").transform;
        targetDistance = stoppingDistance + Random.Range(-targetDistanceSpread, targetDistanceSpread);

        timeBtwShots = StartTimeBtwShots;
    }
	
	// Update is called once per frame
	void Update () {
        FollowTarget();

        if(timeBtwShots <= 0) {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = StartTimeBtwShots;
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PlayerBullet") {
            DestroyEnemy();
        }
    }

    private void FollowTarget() {
        float distanceToTarget = Vector2.Distance(transform.position, target.position);
        // If distance to target is greater than 'targetDistance' then follow
        if (distanceToTarget > targetDistance) {
            // Use actualTarget to prevent enemy moving in Y-axis
            Vector3 actualTarget = new Vector3(target.position.x, transform.position.y, target.position.z);

            // Follow target
            transform.position = Vector2.MoveTowards(transform.position, actualTarget, speed * Time.deltaTime);
        }
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }
}
