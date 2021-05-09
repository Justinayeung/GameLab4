using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingFishes : MonoBehaviour
{
    public GameObject Fish1;
    public GameObject Fish2;
    public GameObject Fish3;

    private void Start() {
        Fish1.SetActive(false);
        Fish2.SetActive(false);
        Fish3.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Fish1.SetActive(true);
            Fish2.SetActive(true);
            Fish3.SetActive(true);
        }
    }
}
