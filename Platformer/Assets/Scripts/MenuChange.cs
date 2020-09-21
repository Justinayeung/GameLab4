using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChange : MonoBehaviour
{
    public GameObject pTree;
    public GameObject gTree;

    void Update() {
        if(EndCondition.won) {
            gTree.SetActive(false);
            pTree.SetActive(true);
        } else {
            gTree.SetActive(true);
            pTree.SetActive(false);
        }
    }
}
