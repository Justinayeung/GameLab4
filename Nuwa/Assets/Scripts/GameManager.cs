using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Brush References")]
    public Transform brush;
    public Camera brushCamera;

    [Header("Rendering References and Variables")]
    public CinemachineVirtualCamera virtualCam;
    public Renderer drawingRenderer;
    public bool isDrawing;

    // Start is called before the first frame update
    void Start() {
        if(brushCamera != null) {
            brushCamera.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 temp = Input.mousePosition;
        temp.z = 0.4f;
        if (isDrawing) {
            brush.position = Vector3.Lerp(brush.position, Camera.main.ScreenToWorldPoint(temp), 0.5f);
            ClampBrushPosition(brush);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            virtualCam.enabled = false;
            setDrawing(!isDrawing);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            virtualCam.enabled = true;
            setDrawing(!isDrawing);
        }
    }

    void ClampBrushPosition(Transform obj) {
        Vector3 pos = Camera.main.WorldToViewportPoint(obj.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        obj.position = Camera.main.ViewportToWorldPoint(pos);
    }

    public void setDrawing(bool state) {
        if (state == true) {
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Default"));
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Interactables"));
            brush.DOLocalMoveY(0.17f, .3f).SetUpdate(true).From();
        } else {
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Default");
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Interactables");
        }

        isDrawing = state;
        virtualCam.enabled = !state;
        drawingRenderer.enabled = state;
        brushCamera.gameObject.SetActive(state);

        float effectAmount = isDrawing ? 1 : 0;
        drawingRenderer.material.DOFloat(effectAmount, "SepiaAmount", .5f).SetUpdate(true);

        if(state == false) {
            FindObjectOfType<DrawManager>().TryRecognize();
        }
    }
}
