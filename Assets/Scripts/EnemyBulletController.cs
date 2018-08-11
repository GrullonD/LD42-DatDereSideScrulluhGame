using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour {

    [SerializeField] float speed = 5f;
    [SerializeField] float startDestructionTime = 5f;
    private float destructionTime = 5f;

    private Transform player;
    private Vector2 targetPosition;
    private Vector3 projectileDirection;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").transform;
        targetPosition = new Vector2(player.position.x, player.position.y);

        projectileDirection = ((Vector3)targetPosition - transform.position).normalized;

        destructionTime = startDestructionTime;
    }

    // Update is called once per frame
    void Update () {
        MoveProjectile();
        DestroyProjectileProcess();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            DestroyProjectile();
        }
    }

    private void MoveProjectile() {
        transform.position += projectileDirection * speed * Time.deltaTime;
    }

    private void DestroyProjectileProcess() {
        if (destructionTime <= 0) {
            DestroyProjectile();
            destructionTime = startDestructionTime;
        }
        else {
            destructionTime -= Time.deltaTime;
        }
    }

    private void DestroyProjectile() {
        Destroy(gameObject);
    }

}
