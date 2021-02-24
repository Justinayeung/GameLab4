using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpBrush : MonoBehaviour
{
    public GameObject brushAndScroll;
    public Animator anim;
    public bool brushObtained;
    public SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start() {
        brushObtained = false;
        brushAndScroll.SetActive(false);
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            anim.SetBool("Obtained", true);
            sphereCollider.enabled = false;
            StartCoroutine(StartUIElement());
            brushObtained = true;
        }
    }

    IEnumerator StartUIElement() {
        brushAndScroll.SetActive(true);
        yield return new WaitForSeconds(6f);
        brushAndScroll.SetActive(false);
    }
}
