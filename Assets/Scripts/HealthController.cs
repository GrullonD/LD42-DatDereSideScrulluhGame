using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField] GameObject player;
    private PlayerHealth playerHealth;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(gameObject);
            playerHealth.health.CurrentVal = playerHealth.health.MaxVal;
        }
    }
}
