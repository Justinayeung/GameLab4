using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListener_Start : MonoBehaviour
{
    [SerializeField] private float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeSound() {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
 
        while(elapsedTime < delay) {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 1, elapsedTime / delay);
            yield return null;
        }
    }
}
