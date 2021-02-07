using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallNewspaper : MonoBehaviour
{
    public Animator Newspaper;
    // Start is called before the first frame update
    void Start()
    {
        Newspaper.SetBool("Start", false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) {
            Newspaper.SetBool("Start", true);
        }
    }
}
