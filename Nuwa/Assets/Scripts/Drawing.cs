using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    LineRenderer line;
    Vector3 mousePos;
    int currentLines = 0;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if(line == null) {
                CreateLine();
            }
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(0, mousePos);
            line.SetPosition(1, mousePos);
        } else if (Input.GetMouseButtonUp(0) && line) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(1, mousePos);
            line = null;
            currentLines++;
        } else if (Input.GetMouseButton(0) && line) {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            line.SetPosition(1, mousePos);
        }
    }

    void CreateLine() {
        line = new GameObject("Line" + currentLines).AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        line.useWorldSpace = true;
        line.numCapVertices = 50;
    }
}
