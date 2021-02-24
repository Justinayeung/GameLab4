using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    public Animator anim;

    // Update is called once per frame
    public void FreeMonkey() {
        StartCoroutine(playMonkey());
    }

    IEnumerator playMonkey() {
        yield return new WaitForSeconds(2.3f);
        anim.SetBool("CageOpen", true);
    }
}
