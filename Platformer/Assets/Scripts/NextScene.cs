using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string sceneName;
    public Image image;
    public Animator anim;

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")) {
            StartCoroutine(FadeToNext());
        } else {
            anim.ResetTrigger("Fade");
        }
    }

    IEnumerator FadeToNext() {
        anim.SetTrigger("Fade");
        yield return new WaitUntil(() => image.color.a == 1);
        SceneManager.LoadScene(sceneName);
    }
}
