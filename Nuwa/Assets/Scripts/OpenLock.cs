using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLock : MonoBehaviour
{
    public Animator Lock;
    public Animator Cage;

    public void Start() {
        Lock.SetBool("Open", false);
        Cage.SetBool("Open", false);
    }

    public void OpeningLock() {
        Lock.SetBool("Open", true);
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor() {
        yield return new WaitForSeconds(1.3f);
        Cage.SetBool("Open", true);
    }
}
