using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCondition : MonoBehaviour
{
    public static bool won = false;
    public GameObject youWon;
    public GameObject firstCam;
    public GameObject secondCam;
    public Transform player;
    public Image image;
    public Image image2;
    public Animator anim;
    public Animator anim2;
    public GameObject ptree;

    void Awake() {
        won = false;
    }

    void Start() {
        youWon.SetActive(false);
        firstCam.SetActive(true);
        secondCam.SetActive(false);
        ptree.SetActive(false);
    }

    void Update() {
        if(GameObject.FindObjectOfType<Enemy>() == null) {
            StartCoroutine(FadeToNext());
            won = true;
        } else {
            anim.ResetTrigger("FadeOut");
            youWon.SetActive(false);
            firstCam.SetActive(true);
            secondCam.SetActive(false);
            ptree.SetActive(false);
        }
    }

    IEnumerator FadeToNext() {
        anim.SetTrigger("Fade");
        yield return new WaitUntil(() => image.color.a == 1);
        player.position = new Vector3(204.36f, 0.198f, 0f);
        firstCam.SetActive(false);
        secondCam.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        anim2.SetTrigger("Fade");
        yield return new WaitUntil(() => image2.color.a == 1);
        ptree.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        anim2.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        youWon.SetActive(true);
    }
}
