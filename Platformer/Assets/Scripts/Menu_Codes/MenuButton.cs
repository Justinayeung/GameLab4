using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] MenuButtonController mBController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions aFunctions;
    [SerializeField] int thisIndex;
    public bool pressed = false;

    void Update() {
        if (mBController.index == thisIndex) {
            animator.SetBool("selected", true); //Animation for selecting
            if (Input.GetAxis("Submit") == 1) {
                animator.SetBool("pressed", true); //Animation for pressing
                pressed = true;
            } else if (animator.GetBool("pressed")) {
                animator.SetBool("pressed", false);
                aFunctions.disableOnce = true; //Diasble is button is clicked
            }
        } else {
            animator.SetBool("selected", false);
        }

        if (pressed && thisIndex == 0) { //Start Button
            Debug.Log("Start");
            StartCoroutine(starting());
        }

        if (pressed && thisIndex == 1) { //Quit Button
            Debug.Log("Quit");
            StartCoroutine(quitting());
        }
    }

    IEnumerator starting() {
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene();
    }

    IEnumerator quitting() {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
