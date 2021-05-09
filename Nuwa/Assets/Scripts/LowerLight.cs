using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerLight : MonoBehaviour
{
    public Light lightToFade;
    public bool fadeIn;
    public float duration = 5f;
    public float min = 0;
    public float max = 1;
    bool once = false;

    IEnumerator fadeInAndOut(Light lightToFade, bool fadeIn, float duration) {
        float counter = 0f;

        //Set Values depending on if fadeIn or fadeOut
        float a, b;

        if (fadeIn) {
            a = min;
            b = max;
        } else {
            a = max;
            b = min;
        }

        float currentIntensity = lightToFade.intensity;

        while (counter < duration) {
            counter += Time.deltaTime;

            lightToFade.intensity = Mathf.Lerp(a, b, counter / duration);
            once = true;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (!once) {
                StartCoroutine(fadeInAndOut(lightToFade ,fadeIn, duration));
            }
        }
    }
}
