using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    [Header("Variables")]
    public bool canMove;
    public bool isGrounded;
    public CharacterController controller;
    public Animator anim;
    public Vector3 playerVelocity;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue;
    public float turnSpeed;

    [Header("Ground Variables")]
    public GameObject groundChecker;
    public float GroundDistance;
    public LayerMask Ground;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(initialWaitTime());
    }

    // Update is called once per frame
    void Update() {
        if (canMove) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 move = new Vector3(moveHorizontal, 0, moveVertical);
            controller.Move(move * Time.deltaTime * playerSpeed);

            //if (move != Vector3.zero) {
            //    gameObject.transform.forward = move;
            //}

            isGrounded = Physics.CheckSphere(groundChecker.transform.position, GroundDistance, Ground, QueryTriggerInteraction.Ignore);
            if (isGrounded && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            }

            // Changes the height position of the player.
            if (Input.GetButtonDown("Jump") && isGrounded) {
                anim.SetBool("Jump", true);
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            } else {
                anim.SetBool("Jump", false);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

            if (moveHorizontal > 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), turnSpeed * Time.deltaTime);
            } else if (moveHorizontal < 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), turnSpeed * Time.deltaTime);
            }

            if (moveVertical > 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), turnSpeed * Time.deltaTime);
            } else if (moveVertical < 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), turnSpeed * Time.deltaTime);
            }

            if (moveHorizontal != 0 || moveVertical != 0) {
                anim.SetBool("Idle", false);
            } else {
                anim.SetBool("Idle", true);
            }
        } else {
            anim.SetBool("Idle", true);
        }
    }

    IEnumerator initialWaitTime() { 
        canMove = false;
        yield return new WaitForSeconds(3.5f);
        canMove = true;
    }
}
