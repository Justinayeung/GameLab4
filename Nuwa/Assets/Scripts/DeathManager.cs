using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeathManager : MonoBehaviour
{
    public Animator DeathFade;
    public Transform respawnPoint;
    public CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start() {
    }

    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Death")) {
            StartCoroutine(DeathAndRespawn());
        }

        if(other.CompareTag("RespawnPoint")) {
            respawnPoint = other.gameObject.transform;
        }
    }

    IEnumerator DeathAndRespawn() {
        cam.Follow = null;
        DeathFade.SetBool("FadeToBlack", true);
        yield return new WaitForSeconds(2.0f);
        transform.position = respawnPoint.position;
        cam.Follow = transform;
        yield return new WaitForSeconds(0.3f);
        DeathFade.SetBool("FadeToBlack", false);
    }
}
