using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovement : MonoBehaviour
{
    [Header ("Variables")]
    public float speed;
    public float jumpForce;
    public float checkRadius;
    public Transform groundCheck;
    public LayerMask isGround;
    public AudioClip landingSound;

    bool onGround;
    float moveInput;
    Rigidbody2D rb;
    AudioSource audio;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && onGround) { //Jump
            rb.velocity = Vector2.up * jumpForce;
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
