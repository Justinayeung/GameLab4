using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleScript : MonoBehaviour
{
    public float duration;
    public Vector3 startScale;
    public Vector3 scaleTo;
    bool isScaling = false;

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(scaleOverTime(transform, scaleTo, duration));
    }

    IEnumerator scaleOverTime(Transform objectToScale, Vector3 toScale, float duration) { 
        if(isScaling) { //Making sure there is only one instance of the function running
            yield break; //exit is still running
        }
        isScaling = true;

        float counter = 0;

        Vector3 startScaleSize = startScale;

        while(counter < duration) {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScale, counter/duration);
            yield return null;
        }

        isScaling = false;
    }
}
