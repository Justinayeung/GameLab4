using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource jumping;
    public AudioSource attack;
    public AudioSource attackHit;
    bool hitTarget;

    public void PlayJump() {
        jumping.Play();
    }

    public void PlayAttack() {
        if(hitTarget) {
            attackHit.Play();
        } else {
            attack.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy")) {
            hitTarget = true;
        } else {
            hitTarget = false;
        }
    }
}
