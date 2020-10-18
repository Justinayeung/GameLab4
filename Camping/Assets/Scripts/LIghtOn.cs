using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIghtOn : MonoBehaviour
{
    public GameObject Light1;
    public GameObject Light2;
    public GameObject Light3;
    public GameObject Light4;
    public GameObject Light5;
    public AudioSource Lighton;

    public void Start() {
        Light1.SetActive(false);
        Light2.SetActive(false);
        Light3.SetActive(false);
        Light4.SetActive(false);
        Light5.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            StartCoroutine(LightTurnOn());
        }
    }

    IEnumerator LightTurnOn() {
        Lighton.Play();
        Light1.SetActive(true);
        Light2.SetActive(true);
        Light3.SetActive(true);
        Light4.SetActive(true);
        Light5.SetActive(true);
        yield return new WaitForSeconds(10f);
        Light1.SetActive(false);
        Light2.SetActive(false);
        Light3.SetActive(false);
        Light4.SetActive(false);
        Light5.SetActive(false);
    }
}
