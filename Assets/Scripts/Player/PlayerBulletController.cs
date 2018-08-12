using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour {

    [SerializeField] float speed = 7f;
    [SerializeField] float startDestructionTime = 5f;
    private float destructionTime = 5f;

    public Vector3 targetPosition;
    private Vector3 projectileDirection;

    // Use this for initialization
    void Start() {

        projectileDirection = (targetPosition - transform.position).normalized;

        destructionTime = startDestructionTime;
    }

    // Update is called once per frame
    void Update() {
        MoveProjectile();
        DestroyProjectileProcess();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
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
