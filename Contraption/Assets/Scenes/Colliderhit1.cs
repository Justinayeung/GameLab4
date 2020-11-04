using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliderhit1 : MonoBehaviour
{
    AudioSource aud;
    public AudioClip card;
    public float timer;

    private void Start() {
        aud = GetComponent<AudioSource>();
    }

    private void Update() {
        timer = Time.fixedTime;
    }

    private void OnCollisionEnter(Collision other) {
        if(timer >= 2) {
            if(other.collider.CompareTag("card")) {
                aud.PlayOneShot(card);
            }
        }
    }
}
