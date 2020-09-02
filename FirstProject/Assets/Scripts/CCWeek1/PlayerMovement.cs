using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Variables")]
    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask isGround;
    public int extraJumpValue;
    public float jumpTime;
    public AudioClip bounceSound;

    int extraJumps;
    float moveInput;
    Rigidbody2D rb;
    //bool facingRight = true;
    bool isGrounded;
    float jumpTimeCounter;
    bool isJumping;
    AudioSource bounceAudio;

    // Start is called before the first frame update
    void Start() {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        bounceAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (isGrounded == true) {
            extraJumps = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if (Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true) {
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true) {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
        }

        if (moveInput > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private void FixedUpdate() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround);
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //if (facingRight == false && moveInput > 0) {
        //    Flip();
        //} else if (facingRight == true && moveInput < 0) {
        //    Flip();
        //}
    }

    //void Flip() {
    //    facingRight = !facingRight;
    //    Vector3 Scaler = transform.localScale;
    //    Scaler.x *= -1;
    //    transform.localScale = Scaler;
    //}

    private void OnCollisionEnter2D(Collision2D collision) {
        bounceAudio.PlayOneShot(bounceSound);
    }
}
