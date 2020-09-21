using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCondition : MonoBehaviour
{
    public static bool won = false;

    void Awake() {
        won = false;
    }

    void Update() {
        if(GameObject.FindObjectOfType<Enemy>() == null) {
            won = true;
        }
    }
}
