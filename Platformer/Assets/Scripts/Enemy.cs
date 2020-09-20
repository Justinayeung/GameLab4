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
    public AudioSource audio;
    public AudioClip chimes;
    public AudioClip damage;
    public AudioClip destroy;

    float dazedTime;
    bool movingLeft = true;
    Color hitColor = Color.red;
    Color originalColor = Color.white;

    // Start is called before the first frame update
    void Start() {
        anim.SetBool("Move", true);
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
            audio.PlayOneShot(chimes);
            audio.PlayOneShot(destroy);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage) {
        dazedTime = startDazedTime;
        Instantiate(attackTakenParticles, transform.position, Quaternion.identity);
        health -= damage;
        StartCoroutine(ChangeColorOnHit());
    }

    IEnumerator ChangeColorOnHit() {
        audio.PlayOneShot(chimes);
        audio.PlayOneShot(damage);
        sprite.color = hitColor;
        yield return new WaitForSeconds(0.5f);
        sprite.color = originalColor;
    }
}
