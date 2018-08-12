using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public bool hasDied = false;

    [SerializeField] public Stat health;

    private void Awake() {
        health.Initialize();
    }
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
        health.CurrentVal -= 10;
    }

    private void CheckIfDead() {
        if (health.CurrentVal <= 0) {
            hasDied = true;
        }
    }
    private void DeathProcess() {
        FindObjectOfType<GameManager>().EndGame();
    }
}
