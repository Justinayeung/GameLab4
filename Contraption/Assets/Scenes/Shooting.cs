using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    public GameObject[] chipPrefab;
    public GameObject[] die;
    public float step;
    public AudioSource aud;
    public AudioClip dieNoise;
    public AudioClip pokerChip;
    public AudioClip domino;
    Vector3 target;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)) {
            var ray = cam.ScreenPointToRay(Input.mousePosition);   
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.name == "back") {
                    target = hit.point;
                    GameObject clone;
                    Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
                    clone = Instantiate(chipPrefab[Random.Range(0, chipPrefab.Length)], transform.position, rotation);
                    clone.GetComponent<Transform>().position = Vector3.MoveTowards(transform.position, target, step);
                    aud.PlayOneShot(pokerChip);
                    Destroy(clone, 10f); 
                }

                var rig = hit.collider.GetComponent<Rigidbody>();
                if(rig != null) {
                    rig.AddForceAtPosition(ray.direction * 10f, hit.point, ForceMode.Impulse);
                } 
                
                if(hit.collider.name == "floor") {
                    target = hit.point;
                    GameObject clone;
                    Quaternion rotation = Quaternion.Euler(-90f, 0f, 0f);
                    clone = Instantiate(die[Random.Range(0, die.Length)], transform.position, rotation);
                    clone.GetComponent<Transform>().position = Vector3.MoveTowards(transform.position, target, step);
                    aud.PlayOneShot(dieNoise);
                    Destroy(clone, 10f); 
                }

                if(hit.collider.CompareTag("dice")) {
                    aud.PlayOneShot(dieNoise);
                }

                if(hit.collider.CompareTag("chip")) {
                    hit.collider.attachedRigidbody.AddForce(Vector3.up * 70f, ForceMode.Impulse);
                    aud.PlayOneShot(pokerChip);
                }

                if(hit.collider.CompareTag("dom")) {
                    aud.PlayOneShot(domino);
                }
            }
        }
    }
}
