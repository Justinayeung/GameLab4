using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    public int health;
    public float speed;
    public float rayDistance;
    public float startDazedTime;

    [Header("References")]
    public GameObject attackTakenParticles;
    public Transform groundDetection;

    float dazedTime;
    bool movingLeft = true;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance);
        if(groundInfo.collider == false) { //If ray hasn't collided with anything
            if(movingLeft) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingLeft = false;
            } else {
                transform.eulerAngles = new Vector3(0, -0, 0);
                movingLeft = true;
            }
        }

        if(dazedTime <= 0) {
            speed = 5;
        } else {
            speed = 0;
            dazedTime -= Time.deltaTime;
        }

        if(health <= 0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) {
        //Play hurt sounds
        dazedTime = startDazedTime;
        Instantiate(attackTakenParticles, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage taken");
    }
}
