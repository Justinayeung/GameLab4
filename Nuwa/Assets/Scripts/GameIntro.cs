using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    public Animator orb;
    public GameObject player;
    public AudioSource chime;

    // Start is called before the first frame update
    void Start()
    {
        player.SetActive(false);
        StartCoroutine(OrbAnim());
    }

    IEnumerator OrbAnim() {
        yield return new WaitForSeconds(1f);
        orb.SetBool("Play", true);
        yield return new WaitForSeconds(2.5f);
        chime.Play();
        yield return new WaitForSeconds(0.5f);
        player.SetActive(true);
    }
}
