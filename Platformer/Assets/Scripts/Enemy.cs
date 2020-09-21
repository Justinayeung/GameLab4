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
    public Animator anim;
    public SpriteRenderer sprite;
    public AudioSource chimes;
    public AudioSource destroy;

    float dazedTime;
    bool movingLeft = true;
    Color hitColor = Color.red;
    Color originalColor = Color.white;
    BoxCollider2D collider;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start() {
        anim.SetBool("Move", true);
        collider = GetComponent<BoxCollider2D>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update() {
        if(playerHealth.lost == false) {
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
        }
    }

    public void TakeDamage(int damage) {
        dazedTime = startDazedTime;
        Instantiate(attackTakenParticles, transform.position, Quaternion.identity);
        health -= damage;
        StartCoroutine(ChangeColorOnHit());
    }

    IEnumerator ChangeColorOnHit() {
        chimes.Play();
        destroy.Play();
        if(health <= 0) {
            collider.enabled = false;
            sprite.enabled = false;
            StartCoroutine(Destroy());
        } else { 
            sprite.color = hitColor;
            yield return new WaitForSeconds(0.5f);
            sprite.color = originalColor;
        }
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
