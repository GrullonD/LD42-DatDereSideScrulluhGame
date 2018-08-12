using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject mainCamera;

    [SerializeField] int playerSpeed = 10;
    [SerializeField] int playerJumpPower = 1250;
    [SerializeField] Rigidbody2D rigidBody;

    public bool facingRight = true;

    private bool isGrounded = false;
    [SerializeField] int MaxNumOfJumps = 3;
    private int numOfJumps = 0;
    private float jumpShake = 0.01f;
    private float jumpLength = 0.1f;

    private float moveX;

    [SerializeField] int numOfSpaceVaporizers = 5;
    public bool usingSpaceVaporizer = false;
    private float svShake = 0.1f;
    private float svLength = 2f;

    // Use this for initialization
    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        PlayerMove();
        SpaceVaporizer();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ground") {
            isGrounded = true;
            numOfJumps = 0;
            mainCamera.GetComponent<CameraShake>().Shake(jumpShake, jumpLength);
        }
    }

    private void PlayerMove()
    {
        // Controls
        moveX = Input.GetAxis("Horizontal");
        if ( (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && numOfJumps < MaxNumOfJumps) {
            Jump();
        }

        // Animations

        // Player Direction
        if (moveX < 0.0f && facingRight == true) {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == false) {
            FlipPlayer();
        }

        // Physics
        rigidBody.velocity = new Vector2(moveX * playerSpeed, rigidBody.velocity.y);
    }
    private void Jump() {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
        numOfJumps += 1;
    }
    private void FlipPlayer() {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;

        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void SpaceVaporizer() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) && usingSpaceVaporizer == false && numOfSpaceVaporizers != 0) {
            usingSpaceVaporizer = true;
            numOfSpaceVaporizers -= 1;
            StartCoroutine("ActivateSpaceVaporizer");
        }
    }
    IEnumerator ActivateSpaceVaporizer() {
        print("Space Vaporizer Vaporizing");

        // Perform Space Vaporizer Function
        Vaporizing();
        yield return new WaitForSeconds(1f);
        usingSpaceVaporizer = false;
    }
    private void Vaporizing() {
        mainCamera.GetComponent<CameraShake>().Shake(svShake, svLength);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            enemy.GetComponent<EnemyController>().Dying();
           // GameObject.Destroy(enemy);
    }
}
