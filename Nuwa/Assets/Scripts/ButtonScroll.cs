using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScroll : MonoBehaviour
{
    [Header("Stone Sprites")]
    public Sprite YellowStone;
    public Sprite RedStone;
    public Sprite BlueStone;
    public Sprite BlackStone;
    public Sprite WhiteStone;

    [Header("Cursor Sprites")]
    public Texture2D OriginalCursor;
    public Texture2D YellowCursor;
    public Texture2D RedCursor;
    public Texture2D BlueCursor;
    public Texture2D BlackCursor;
    public Texture2D WhiteCursor;

    [Header("Variables")]
    public int typeOfStone;
    public bool yellowStone;
    public bool redStone;
    public bool blueStone;
    public bool blackStone;
    public bool whiteStone;

    SpriteRenderer sr;
    Animator anim;
    bool canSwitch;

    void Start() {
        Cursor.SetCursor(OriginalCursor, Vector2.zero, CursorMode.ForceSoftware);
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
                    yellowStone = true; redStone = false; blueStone = false; blackStone = false; whiteStone = false;
                }
                break;
            case 3: //Red Stone
                if(canSwitch) {
                    sr.sprite = RedStone;
                    yellowStone = false; redStone = true; blueStone = false; blackStone = false; whiteStone = false;
                }
                break;
            case 2: //Blue Stone
                if(canSwitch) {
                    sr.sprite = BlueStone;
                    yellowStone = false; redStone = false; blueStone = true; blackStone = false; whiteStone = false;
                }
                break;
            case 1: //Black Stone
                if(canSwitch) {
                    sr.sprite = BlackStone;
                    yellowStone = false; redStone = false; blueStone = false; blackStone = true; whiteStone = false;
                }
                break;
            default: //White Stone
                if(canSwitch) {
                    sr.sprite = WhiteStone;
                    yellowStone = false; redStone = false; blueStone = false; blackStone = false; whiteStone = true;
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

    public void checkingCursor() {
        if (yellowStone) {
            Cursor.SetCursor(YellowCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(redStone) {
            Cursor.SetCursor(RedCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(blueStone) {
            Cursor.SetCursor(BlueCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(blackStone) {
            Cursor.SetCursor(BlackCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(whiteStone) {
            Cursor.SetCursor(WhiteCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
