using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect;

    float length;
    float startpos;

    // Start is called before the first frame update
    void Start() {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x; //Size (x) of sprite
    }

    void FixedUpdate() {
        float temp = (cam.transform.position.x * (1 - parallaxEffect)); //How far we move relative to the cam
        float dist = (cam.transform.position.x * parallaxEffect); //How far we move in world space
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z); //Move Camera

        if (temp > startpos + length) {
            startpos += length;
        } else if (temp < startpos - length) {
            startpos -= length;
        }
    }
}
