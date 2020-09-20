using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Variables")]
    public float speed;
    public float jumpForce;
    public float checkRadius;
    public float jumpTime;
    public float duration = 0.15f;
    public float magnitude = 0.4f;

    [Header ("References")]
    public Transform groundCheck;
    public LayerMask isGround;
    public AudioClip landingSound;
    public SpriteRenderer sprite;
    public Animator imageAnim;
    public CameraShake cameraShake;

    bool onGround;
    bool isJumping;
    float moveInput;
    float jumpTimeCounter;
    Rigidbody2D rb;
    AudioSource audio;
    Animator anim;
    Color hitColor = Color.red;
    Color originalColor = Color.white;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow) && onGround) { //Normal Jump
            anim.SetTrigger("Jump");
            isJumping = true;
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime; //Reset jumpTimeCounter
        }

        if(Input.GetKey(KeyCode.UpArrow) && isJumping) { //Jump Higher when holding space
            if(jumpTimeCounter > 0) { //Prevent player from jumping forever
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime; //Decrease counter
            } else {
                isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.UpArrow)) { //Setting isJumping bool to false
            isJumping = false;
        }

        if(moveInput > 0) { //Moving right
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (moveInput < 0) { //Moving left
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if(moveInput == 0) {
            anim.SetBool("Move", false);
        } else {
            anim.SetBool("Move", true);
        }
    }

    void FixedUpdate() {
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, isGround); //Checking if player is on ground
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other) {
        audio.PlayOneShot(landingSound); //Play landing audio
        if(other.gameObject.CompareTag("Enemy")) {
            StartCoroutine(ChangeColorOnHit());
            StartCoroutine(cameraShake.Shake(duration, magnitude));
            imageAnim.SetBool("Flash", true);
            playerHealth.health -= 1; 
        } else {
            imageAnim.SetBool("Flash", false);
        }
    }

    IEnumerator ChangeColorOnHit() {
        sprite.color = hitColor;
        yield return new WaitForSeconds(0.5f);
        sprite.color = originalColor;
    }
}
