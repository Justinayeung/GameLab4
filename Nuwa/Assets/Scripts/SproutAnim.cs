using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutAnim : MonoBehaviour
{
    public Animator flower;

    private void Awake() {
        flower = GetComponent<Animator>();
    }

    public void Sprout() { 
        flower.SetBool("Sprout", true);
    }
}
