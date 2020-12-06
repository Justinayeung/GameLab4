using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera brushCamera;

    public Cinemachine.CinemachineVirtualCamera virtualCam;
    public Renderer drawingRenderer;
    public bool isDrawing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            virtualCam.enabled = false;
            setDrawing(!isDrawing);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            virtualCam.enabled = true;
            setDrawing(!isDrawing);
        }
    }

    public void setDrawing(bool state) {
        if (state == true) {
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Default"));
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Interactables"));
        } else {
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Default");
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Interactables");
        }

        isDrawing = state;
        virtualCam.enabled = !state;
        drawingRenderer.enabled = state;
       // drawingRenderer.transform.GetChild(0).gameObject.SetActive(state);
        if(state == false) {
            FindObjectOfType<DrawManager>().TryRecognize();
        }
    }
}
