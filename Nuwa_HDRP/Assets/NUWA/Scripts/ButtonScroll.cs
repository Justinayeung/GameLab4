using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScroll : MonoBehaviour
{
    public int typeOfStone;

    [Header("References")]
    public Sprite YellowStone;
    public Sprite RedStone;
    public Sprite BlueStone;
    public Sprite BlackStone;
    public Sprite WhiteStone;

    SpriteRenderer sr;
    Animator anim;
    bool canSwitch;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        typeOfStone = 0;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(fading());
            typeOfStone++;
            if(typeOfStone > 4) {
                typeOfStone = 0;
            }
        }
        StoneSwitch();
    }

    void StoneSwitch() {
        switch(typeOfStone) {
            case 4: //Yellow Stone
                if(canSwitch) { 
                    sr.sprite = YellowStone;
                }
                break;
            case 3: //Red Stone
                if(canSwitch) {
                    sr.sprite = RedStone;
                }
                break;
            case 2: //Blue Stone
                if(canSwitch) {
                    sr.sprite = BlueStone;
                }
                break;
            case 1: //Black Stone
                if(canSwitch) {
                    sr.sprite = BlackStone; 
                }
                break;
            default: //White Stone
                if(canSwitch) {
                    sr.sprite = WhiteStone;
                }
                break;
        }
    }

    IEnumerator fading() {
        canSwitch = false;
        anim.SetBool("FadetoNext", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("FadetoNext", false);
    }

    public void CallSwitch() {
        canSwitch = true;
    }
}
