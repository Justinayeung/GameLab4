using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var rayStart = new Vector2(mousePos.x, mousePos.y);
            var hit = Physics2D.Raycast(rayStart, Vector2.zero);
            if (hit.collider != null) {
                var go = hit.collider.gameObject;
                if (go.tag == "Player") {
                    Destroy(go);
                }
            }
        }
    }
}
