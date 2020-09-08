using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] MenuButtonController mBController;
    public bool disableOnce;

    /// <summary>
    /// Function play sound when going to another button
    /// </summary>
    /// <param name="whichSound"></param>
    void PlaySound(AudioClip whichSound) {
        if (!disableOnce) {
            mBController.audioSource.PlayOneShot(whichSound);
        } else {
            disableOnce = false;
        }
    }
}
