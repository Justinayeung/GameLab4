using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMouseOver : MonoBehaviour
{
    ButtonScroll buttonScript;

    private void Start() {
        buttonScript = FindObjectOfType<ButtonScroll>();    
    }

    public void OnMouseOver() {
        buttonScript.checkingCursor();
    }

    public void OnMouseExit() {
        Cursor.SetCursor(buttonScript.OriginalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
