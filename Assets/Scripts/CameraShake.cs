using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    [SerializeField] float shakeAmount = 0.1f;
    [SerializeField] float shakeLength = 2f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}


    public void Shake(float amount, float length) {
        shakeAmount = amount;
        shakeLength = length;
        InvokeRepeating("DoShake", 0, 0.01f);
        Invoke("StopShake", shakeLength);
    }
    public void DoShake() {

        Vector3 cameraPosition = gameObject.transform.position;
        float offsetX;
        float offsetY;
        float offsetZ;
        if (shakeAmount > 0) {
            offsetX = Random.value * shakeAmount * 2 - shakeAmount;
            offsetY = Random.value * shakeAmount * 2 - shakeAmount;
            offsetZ = Random.value * shakeAmount * 2 - shakeAmount;

            cameraPosition.x += offsetX;
            cameraPosition.y += offsetY;
            cameraPosition.z += offsetZ;

            gameObject.transform.position = cameraPosition;
        }
    }
    public void StopShake() {
        CancelInvoke("DoShake");
        gameObject.transform.localPosition = Vector3.zero;
    }
}
