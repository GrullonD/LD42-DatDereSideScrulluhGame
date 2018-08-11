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
        DeathProcess();
	}

    private void CheckIfDead() {
        if (health <= 0) {
            hasDied = true;
        }
    }
    private void DeathProcess() {
        if (hasDied == true) {
            StartCoroutine("Die");
        }
    }
    IEnumerator Die() {
        SceneManager.LoadScene("Level1");
        yield return null;
    }
}
