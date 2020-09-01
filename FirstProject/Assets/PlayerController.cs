using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    bool jump = false;

    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
        }
    }

    private void FixedUpdate(){ //When physics is happening
        if (jump) {
            jump = false;
            rb.AddForce(Vector2.up * 5, ForceMode2D.Impulse); //Add an up force
        }
    }
}
