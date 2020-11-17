using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTest : MonoBehaviour
{
    Material material;
    bool painting = false;
    float scale = 16;
    
    // Start is called before the first frame update
    void Start() {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) {
            painting = true;
        } else {
            painting = false;
        }

        if (painting) {
            scale -= Time.deltaTime;
            if(scale <= 0f) {
                scale = 0;
                painting = false;
            }
        }

        material.SetFloat("_Scale", scale);
    }
}
