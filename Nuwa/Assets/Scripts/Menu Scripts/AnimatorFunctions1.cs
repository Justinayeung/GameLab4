using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions1 : MonoBehaviour
{
	[SerializeField] MenuButtonController1 menuButtonController;
	public bool disableOnce;

	void PlaySound(AudioClip whichSound){
		if(!disableOnce){
			menuButtonController.audioSource.PlayOneShot (whichSound);
		}else{
			disableOnce = false;
		}
	}
}	
