using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliderhit : MonoBehaviour
{
    AudioSource aud;
    public AudioClip domino;

    private void Start() {
        aud = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("dom")) {
            aud.PlayOneShot(domino);
        }
    }
}
