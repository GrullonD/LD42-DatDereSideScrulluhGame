using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    public int health = 1000;
    public bool hasDied;

	// Use this for initialization
	void Start () {
        hasDied = false;
	}
	
	// Update is called once per frame
	void Update () {
        CheckIfDead();
        if(hasDied == true) {
            DeathProcess();
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Bullet") {
            health -= 10;
        }
    }

    private void CheckIfDead() {
        if (health <= 0) {
            hasDied = true;
        }
    }
    private void DeathProcess() {
            StartCoroutine("Die");
    }
    IEnumerator Die() {
        SceneManager.LoadScene("Level1");
        yield return null;
    }
}
