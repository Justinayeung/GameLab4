using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkColorSwitch : MonoBehaviour
{
    [Header("Ink Color Variables")]
    public int inkColor;
    public bool yellowInk;
    public bool redInk;
    public bool blueInk;
    public bool blackInk;
    public bool whiteInk;

    [Header("Cursor Sprites")]
    public Texture2D OriginalCursor;
    public Texture2D YellowCursor;
    public Texture2D RedCursor;
    public Texture2D BlueCursor;
    public Texture2D BlackCursor;
    public Texture2D WhiteCursor;

    public GameObject ArrowUI;

    InkWhite ink;

    // Start is called before the first frame update
    void Start() {
        //Set cursor
        ArrowUI.SetActive(false);
        Cursor.SetCursor(OriginalCursor, Vector2.zero, CursorMode.ForceSoftware);
        inkColor = 0;
        ink = FindObjectOfType<InkWhite>();
    }

    // Update is called once per frame
    void Update() {
        if(ink.powers && ink.inkPalette) {
            ArrowUI.SetActive(true);
        } else {
            ArrowUI.SetActive(false);
        }
    }

    void StoneSwitch() {
        switch (inkColor) {
            case 4: //Yellow Stone
                yellowInk = true; redInk = false; blueInk = false; blackInk = false; whiteInk = false;
                break;
            case 3: //Red Stone
                yellowInk = false; redInk = true; blueInk = false; blackInk = false; whiteInk = false;
                break;
            case 2: //Blue Stone
                yellowInk = false; redInk = false; blueInk = true; blackInk = false; whiteInk = false;
                break;
            case 1: //Black Stone
                yellowInk = false; redInk = false; blueInk = false; blackInk = true; whiteInk = false;
                break;
            default: //White Stone
                yellowInk = false; redInk = false; blueInk = false; blackInk = false; whiteInk = true;
                break;
        }
    }

    public void checkingCursor() {
        if (yellowInk) {
            Cursor.SetCursor(YellowCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(redInk) {
            Cursor.SetCursor(RedCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(blueInk) {
            Cursor.SetCursor(BlueCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(blackInk) {
            Cursor.SetCursor(BlackCursor, Vector2.zero, CursorMode.ForceSoftware);
        } else if(whiteInk) {
            Cursor.SetCursor(WhiteCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
    }
}
