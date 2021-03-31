using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftAgain : MonoBehaviour
{
    public GameObject brushAndScroll;
    public BoxCollider boxCollider;

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            boxCollider.enabled = false;
            StartCoroutine(StartUIElement());
        }
    }

    IEnumerator StartUIElement() {
        brushAndScroll.SetActive(true);
        yield return new WaitForSeconds(4f);
        brushAndScroll.SetActive(false);
    }
}
