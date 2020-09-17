using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Variables")]
    public float speed;
    public float jumpForce;
    public float checkRadius;
    public float jumpTime;

    [Header ("References")]
    public Transform groundCheck;
    public LayerMask isGround;
    public AudioClip landingSound;

    bool onGround;
    bool isJumping;
    float moveInput;
    float jumpTimeCounter;
    Rigidbody2D rb;
    AudioSource audio;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && onGround) { //Normal Jump
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime; //Reset jumpTimeCounter
        }

        if(Input.GetKey(KeyCode.Space) && isJumping) { //Jump Higher when holding space
            if(jumpTimeCounter > 0) { //Prevent player from jumping forever
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime; //Decrease counter
            } else {
                isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)) { //Setting isJumping bool to false
            isJumping = false;
        }

        if(moveInput > 0) { //Moving right
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (moveInput < 0) { //Moving left
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void FixedUpdate() {
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround); //Checking if player is on ground
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other) {
        audio.PlayOneShot(landingSound); //Play landing audio
    }
}
