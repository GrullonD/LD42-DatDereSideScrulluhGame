using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] GameObject Target;
    [SerializeField] float speed = 2f;

    [SerializeField] int generalTargetDistance = 5;     // How far to stay away from target
    [SerializeField] float targetDistanceSpread = 2f;   // Spread of different enemies from target

    private Transform target;
    private float targetDistance;

	// Use this for initialization
	void Start () {
        target = Target.GetComponent<Transform>();
        targetDistance = generalTargetDistance + Random.Range(-targetDistanceSpread, targetDistanceSpread);
    }
	
	// Update is called once per frame
	void Update () {
        FollowTarget();
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
}
