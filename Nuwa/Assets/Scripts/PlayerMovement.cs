using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Variables")]
    public float speed;
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float rotationSpeed;
    public float maxSpeed = 0.1f;
    public bool canJump;
    public bool canMove;

    [Header("References")]
    public Transform player;

    Rigidbody rb;
    float smoothSpeed;
    Animator anim;
    CapsuleCollider Capsule;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		Capsule = GetComponent<CapsuleCollider>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update() {
        if(canMove) { 
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * speed, rb.velocity.y, moveVertical * speed);
            rb.velocity = movement;
            anim.SetBool("Idle", false);
        } else {
            anim.SetBool("Idle", true);
        }

        //If using input then rotate towards direction
        //if(moveHorizontal != 0 && moveVertical != 0) {
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        //    canMove = true;
        //} else {
        //    canMove = false;
        //}

        //Jump
        if (canJump) {
            if (Input.GetButtonDown("Jump")) {
                rb.velocity = Vector3.up * jumpVelocity;
            }
        }

        //Faster Falling
        if (rb.velocity.y > 0) { //If vertical motion is less then 0 (falling)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime; //Apply fall multiplier
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) { //If jumping and if button not held = low jump
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Ground")) {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            canJump = false;
        }
    }
}
