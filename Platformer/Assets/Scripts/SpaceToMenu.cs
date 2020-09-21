using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceToMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) && EndCondition.won) {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
