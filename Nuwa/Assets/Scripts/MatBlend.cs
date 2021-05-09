using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatBlend : MonoBehaviour
{
    // Blends between two materials

    public Material material1;
    public Material material2;
    public float duration = 2.0f;
    public Renderer mountain_01;

    void Start()
    {
        // At start, use the first material
        mountain_01.material = material1;
    }

    void Update()
    {
        // ping-pong between the materials over the duration
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        mountain_01.material.Lerp(material1, material2, lerp);
    }
}
