using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkWhite : MonoBehaviour
{
    public GameObject Bplane;
    public GameObject Wplane;
    public bool powers;
    public bool inkPalette;

    // Start is called before the first frame update
    void Start() {
        RenderSettings.fogColor = Color.white;
        powers = false;
        Bplane.SetActive(false);
        Wplane.SetActive(true);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Bplane.SetActive(true);
            Wplane.SetActive(false);
            RenderSettings.fogColor = Color.black;
            powers = true;
        }
    }
}
