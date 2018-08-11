using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public int health = 1000;
    public bool hasDied = false;
    [SerializeField] GameObject healthUI;

    // Use this for initialization
    void Start () {
        UpdateHealth();
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
            UpdateHealth();
        }
    }

    private void UpdateHealth() {
        health -= 10;
        healthUI.gameObject.GetComponent<Text>().text = ("Health: " + health);
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
