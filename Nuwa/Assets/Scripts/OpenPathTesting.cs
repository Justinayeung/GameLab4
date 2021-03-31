using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPathTesting : MonoBehaviour
{
    public Animator Cage;

    public void Start() {
        Cage.SetBool("Open", false);
    }

    public void OpeningPath() {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor() {
        Cage.SetBool("Open", true);
        yield return null;
    }
}
