using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Variables")]
    public int health;
    public float speed;
    public float startDazedTime;

    [Header("References")]
    public GameObject attackTaken;

    float dazedTime;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
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
        Instantiate(attackTaken, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage taken");
    }
}
