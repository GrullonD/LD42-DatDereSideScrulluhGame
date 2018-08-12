using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    public GameObject player; // Left public to make sure it tags right object
    [SerializeField] float xMin = -10f;
    [SerializeField] float xMax = 10f;
    [SerializeField] float yMin = 0f;
    [SerializeField] float yMax = 0f;

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }

}
