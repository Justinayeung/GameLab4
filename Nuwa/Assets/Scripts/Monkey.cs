using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    public Animator anim;
    public GameObject recipeCircle;

    private void Start() {
        recipeCircle.SetActive(false);
    }

    // Update is called once per frame
    public void FreeMonkey() {
        StartCoroutine(playMonkey());
    }

    IEnumerator playMonkey() {
        yield return new WaitForSeconds(2.3f);
        anim.SetBool("CageOpen", true);
        StaticClass.monkeyFree = true;
        StartCoroutine(StartUIElement());
    }

    IEnumerator StartUIElement() {
        recipeCircle.SetActive(true);
        yield return new WaitForSeconds(4f);
        recipeCircle.SetActive(false);
    }
}
