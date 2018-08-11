using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    public float timer = 0f;
    public int playerScore = 0;
    [SerializeField] GameObject scoreUI;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
    }

    public void UpdateScore() {
        scoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);
    }
}
