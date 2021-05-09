using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public Animator tree;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            tree.SetBool("Play", true);
        }
    }
}
