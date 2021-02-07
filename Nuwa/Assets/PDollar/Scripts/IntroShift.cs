using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroShift : MonoBehaviour
{
    public GameObject ShiftUI;
    public Animator shiftAnim;
    public GameObject SwipeUI;
    GameManager draw;

    // Start is called before the first frame update
    void Start() {
        SwipeUI.SetActive(false);
        draw = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
            shiftAnim.SetBool("Fade", true);
        }

        if(draw.isDrawing == true) {
            ShiftUI.SetActive(false);
            SwipeUI.SetActive(true);
        }
    }
}
