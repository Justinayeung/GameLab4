using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpBrush : MonoBehaviour
{
    public GameObject brushAndScroll;
    public Animator anim;
    public SphereCollider sphereCollider;
    public Animator earthAnim;

    [Header("Light Variables and Light Reference")]
    public Light brushLight;
    public float intensityA;
    public float intensityB;
    public float smooth;

    // Start is called before the first frame update
    void Start() {
        brushAndScroll.SetActive(false);
        earthAnim.SetBool("Play", false);
    }

    private void Update() {
        if (!StaticClass.brushObtained) {
            brushLight.intensity = Mathf.Lerp(intensityA, intensityB, Time.deltaTime * smooth);
        } else {
            brushLight.intensity = Mathf.Lerp(brushLight.intensity, 0, Time.deltaTime * smooth);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            anim.SetBool("Obtained", true);
            sphereCollider.enabled = false;
            StartCoroutine(StartUIElement());
            StartCoroutine(EarthInitalAnim());
            StaticClass.brushObtained = true;
        }
    }

    IEnumerator StartUIElement() {
        brushAndScroll.SetActive(true);
        yield return new WaitForSeconds(6f);
        brushAndScroll.SetActive(false);
    }

    IEnumerator EarthInitalAnim() {
        earthAnim.SetBool("Play", true);
        yield return null;
    }
}
