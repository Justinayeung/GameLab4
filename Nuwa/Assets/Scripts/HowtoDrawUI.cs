using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoDrawUI : MonoBehaviour
{
    public GameObject UI;
    bool once;

    // Start is called before the first frame update
    void Start() {
        UI.SetActive(false);
        once = false;
    }

    // Update is called once per frame
    void Update() {
        if(StaticClass.brushObtained && Input.GetKeyDown(KeyCode.LeftShift) && once == false) {
            StartCoroutine(circleAnim());
        }
    }

    IEnumerator circleAnim() {
        UI.SetActive(true);
        yield return new WaitForSeconds(4f);
        UI.SetActive(false);
        once = true;
    }
}
