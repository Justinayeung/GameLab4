using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShardPos : MonoBehaviour
{
    [Header("To Broken Image")]
    public float durationOfPos;
    public float durationOfScale;
    public Vector3 endPos;
    public Vector3 endScale;
    public float speed;
    public bool intro;

    [Header("Full Image")]
    public Vector3 originalPos;
    public float moveSpeedToOrgPos;

    [Header("Broken Image")]
    public Vector3 moveDirection;
    public Vector3 startPos;
    public float moveDistance;
    public float moveSpeed;
    public bool clicked;

    [Header("Scale Componenets")]
    public Vector3 orgScale; 
    public float scaleToOrgTime;

    Vector3 currentPos;

    void Start() {
        intro = true;
        transform.position = originalPos;
        transform.localScale = orgScale;
        StartCoroutine(ShardBrokenPos(originalPos, endPos, durationOfPos));
        StartCoroutine(ShardBrokenScale(orgScale, endScale, durationOfScale));
    }

    void Update() {
        if(transform.position == endPos && transform.localScale == endScale) {
            intro = false;
        }

        currentPos = transform.position;

        if(!clicked) {
            if (!intro) { 
                transform.position = startPos + moveDirection * (moveDistance*Mathf.Sin(Time.time*moveSpeed));
            }
        } else {
            Vector3 startingPos = currentPos;
            Vector3 startingScale = transform.localScale;
            StartCoroutine(ScaleOverTime(startingScale, orgScale, scaleToOrgTime));
            StartCoroutine(PosOverTime(startingPos, originalPos, moveSpeedToOrgPos));
        }
    }

    public IEnumerator ScaleOverTime(Vector3 a, Vector3 b, float seconds) {
        float i = 0.0f;
        float rate = (1.0f/seconds) * seconds;
        while(i < 1.0f) {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    public IEnumerator PosOverTime(Vector3 a, Vector3 b, float seconds) {
        float i = 0.0f;
        float rate = (1.0f/seconds) * seconds;
        while(i < 1.0f) {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    public IEnumerator ShardBrokenPos(Vector3 a, Vector3 b, float seconds) {
        float i = 0.0f;
        float rate = (1.0f/seconds) * speed;
        while(i < 1.0f) {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }

    public IEnumerator ShardBrokenScale(Vector3 a, Vector3 b, float seconds) {
        float i = 0.0f;
        float rate = (1.0f/seconds) * speed;
        while(i < 1.0f) {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
