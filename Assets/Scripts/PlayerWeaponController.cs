using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

    [SerializeField] GameObject player;

    // For Rotation
    private Vector3 mousePosition;
    private Transform target; //Assign to the object you want to rotate
    private Vector3 weaponPosition;
    private float angle;
    private bool facingRight = true;

    // For Shooting
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletSpawnLocation;
    [SerializeField] float shotsPerSecond = 4;
    private float time = 0.0f;
    private float shotPeriod;
    private Vector3 projectileDirection;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
        bulletSpawnLocation = gameObject.transform.Find("ShootingPoint").gameObject;
        facingRight = player.GetComponent<PlayerController>().facingRight;
    }
	
	// Update is called once per frame
	void Update () {
        RotateWeapon();
        ShootWeapon();
	}

    private void RotateWeapon() {
        mousePosition = Input.mousePosition;
        mousePosition.z = -10.00f; //The distance between the camera and object
        weaponPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        mousePosition.x = mousePosition.x - weaponPosition.x;
        mousePosition.y = mousePosition.y - weaponPosition.y;

        angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        facingRight = player.GetComponent<PlayerController>().facingRight;
        if (!facingRight) {
            angle -= 180;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void ShootWeapon() {
        if (Input.GetMouseButton(0)) {
            Debug.Log("Pressed primary button.");
            time += Time.deltaTime;
            shotPeriod = 1 / shotsPerSecond;
            if (time >= shotPeriod) {
                time = time - shotPeriod;
                GameObject projectile = (GameObject)Instantiate(bullet, bulletSpawnLocation.transform.position, Quaternion.identity);

                projectile.GetComponent<PlayerBulletController>().targetPosition = mousePosition;
            }
        }
    }
}
