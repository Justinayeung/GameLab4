using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] Animator animator;
	[SerializeField] AnimatorFunctions animatorFunctions;
	[SerializeField] int thisIndex;
	bool pressed;

    // Update is called once per frame
    void Update()
    {
		if(menuButtonController.index == thisIndex)
		{
			animator.SetBool ("selected", true);
			if(Input.GetAxis ("Submit") == 1){
				animator.SetBool ("pressed", true);
				pressed = true;
			}else if (animator.GetBool ("pressed")){
				animator.SetBool ("pressed", false);
				animatorFunctions.disableOnce = true;
			}
		}else{
			animator.SetBool ("selected", false);
		}

		//Play
		if(pressed && thisIndex == 0) {
			SceneManager.LoadScene("MainGame");
        }

		//Instructions
		if(pressed && thisIndex == 1) {
			SceneManager.LoadScene("Instructions");
        }

		//Quit
		if(pressed && thisIndex == 2) {
			Application.Quit();
        }
    }
}
