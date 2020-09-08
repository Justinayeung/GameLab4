using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    //Use this for initialization
    public int index;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;
    MenuButton button;

    void Start() {
        button = GameObject.FindObjectOfType<MenuButton>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (button.pressed == false) {
            if (Input.GetAxis("Vertical") != 0){
                if (!keyDown) {
                    if (Input.GetAxis("Vertical") < 0) { //Scrolling direction
                        if (index < maxIndex) {
                            index++;
                        } else {
                            index = 0;
                        }
                    } else if (Input.GetAxis("Vertical") > 0) { //Scrolling direction
                        if (index > 0) {
                            index--;
                        } else {
                            index = maxIndex;
                        }
                    }
                    keyDown = true;
                }
            } else {
                keyDown = false;
            }
        }
    }
}
