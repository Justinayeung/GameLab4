using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    [Header("References")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public AudioSource noMoreHealth;

    public GameObject youLose;
    public bool lost;
    bool once = false;

    void Start() {
        youLose.SetActive(false);
        lost = false;
    }

    void Update() {
        if(health == 0) {
            lost = true;
            if(!once) { 
                StartCoroutine(PlaySound());
            }
            youLose.SetActive(true);
        }

        if(health > numOfHearts) {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++) {
            if(i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            if (i<numOfHearts) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

    IEnumerator PlaySound() {
        noMoreHealth.Play();
        yield return new WaitForSeconds(0.1f);
        once = true;
        noMoreHealth.Stop();
    }
}
