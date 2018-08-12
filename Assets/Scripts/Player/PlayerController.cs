using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject mainCamera;

    [SerializeField] GameObject playerSprite;

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

    [SerializeField] private Stat spaceVaporizer;
    [SerializeField] float startSVRechargeRate = 1f;
    private float svRechargeRate = 1f;
    public bool usingSpaceVaporizer = false;
    private bool spaceVaporizerCharged = true;
    private float svShake = 0.1f;
    private float svLength = 2f;


    private float Second = 1f;
    private float milliSecond = 1f;

    private void Awake() {
        spaceVaporizer.Initialize();
    }

    // Use this for initialization
    void Start() {
        mainCamera = GameObject.FindWithTag("MainCamera");
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        PlayerMove();
        SpaceVaporizer();

        if(spaceVaporizer.CurrentVal != spaceVaporizer.MaxVal) { 
            if (milliSecond <= 0) {
                RechargeVaporizer();
                milliSecond = Second;
            }
            else {
                milliSecond -= Time.deltaTime;
            }
        }
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
        Vector2 localScale = playerSprite.transform.localScale;

        localScale.x *= -1;
        playerSprite.transform.localScale = localScale;
    }

    private void SpaceVaporizer() {
        if ( (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) ) && usingSpaceVaporizer == false) {
            usingSpaceVaporizer = true;
            spaceVaporizerCharged = false;
            spaceVaporizer.CurrentVal -= spaceVaporizer.MaxVal;
            StartCoroutine("ActivateSpaceVaporizer");
        }
    }
    IEnumerator ActivateSpaceVaporizer() {
        print("Space Vaporizer Vaporizing");

        // Perform Space Vaporizer Function
        Vaporizing();
        yield return new WaitForSeconds(startSVRechargeRate);
    }
    private void Vaporizing() {
        mainCamera.GetComponent<CameraShake>().Shake(svShake, svLength);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            enemy.GetComponent<EnemyController>().Dying();
           // GameObject.Destroy(enemy);
    }
    private void RechargeVaporizer() {
        if (spaceVaporizer.CurrentVal < 0) spaceVaporizer.CurrentVal = 0;
        if(spaceVaporizer.CurrentVal <= spaceVaporizer.MaxVal) {
            spaceVaporizer.CurrentVal += (spaceVaporizer.MaxVal / startSVRechargeRate);
        }
        if(spaceVaporizer.CurrentVal == spaceVaporizer.MaxVal) usingSpaceVaporizer = false;
    }
}