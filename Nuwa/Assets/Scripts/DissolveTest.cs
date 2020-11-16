using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveTest : MonoBehaviour
{
    Material material;
    bool isDissolving = false;
    float scale;
    
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            isDissolving = true;
        }

        if (isDissolving) {
            scale -= Time.deltaTime;
            if(scale <= 0f) {
                scale = 0;
                isDissolving = false;
            }
        }

        material.SetFloat("_Scale", scale);
    }
}
