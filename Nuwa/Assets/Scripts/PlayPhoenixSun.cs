using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPhoenixSun : MonoBehaviour
{
    public Animator phoenix;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            phoenix.SetBool("Play", true);
        }
    }
}
