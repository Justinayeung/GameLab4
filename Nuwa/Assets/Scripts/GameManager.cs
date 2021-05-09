using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Brush/Ink References")]
    public Transform brush;
    public Camera brushCamera;
    public Transform inkSprite;

    [Header("Rendering References and Variables")]
    public CinemachineVirtualCamera virtualCam;
    public Renderer drawingRenderer;
    public bool isDrawing;

    DrawManager draw;
    //UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter character;
    public PlayerMoveScript character;

    // Start is called before the first frame update
    void Start() {
        //Set brush camera
        Cursor.visible = false;
        draw = FindObjectOfType<DrawManager>();
        //character = FindObjectOfType<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        character = FindObjectOfType<PlayerMoveScript>();
        if(brushCamera != null) {
            brushCamera.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        //if (Input.GetMouseButtonDown(1)) { // Hide and lock cursor when right mouse button pressed
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        //if (Input.GetMouseButtonUp(1)) { // Unlock and show cursor when right mouse button released
        //    //Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}
        Vector3 temp = Input.mousePosition;
        temp.z = 0.4f;
        if (isDrawing) {
            inkSprite.transform.position = Vector3.Lerp(brush.position, Camera.main.ScreenToWorldPoint(temp), 0.5f);
            ClampBrushPosition(inkSprite);
            //Vector2 cursorPos = Camera.main.ScreenToWorldPoint(temp);
            //inkSprite.transform.position = cursorPos;
        }

        if (StaticClass.brushObtained) { 
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) {
                virtualCam.enabled = false;
                isDrawing = true;
                setDrawing(isDrawing);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) {
                virtualCam.enabled = true;
                isDrawing = false;
                setDrawing(isDrawing);
                draw.TryRecognize();
            }
        }
    }

    void ClampBrushPosition(Transform obj) {
        Vector3 pos = Camera.main.WorldToViewportPoint(obj.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        obj.position = Camera.main.ViewportToWorldPoint(pos);
    }

    public void setDrawing(bool state) {
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        if (state == true) {
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Default"));
            Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Interactables"));
            //inkSprite.DOLocalMoveY(0.17f, .3f).SetUpdate(true).From();
            inkSprite.DOScale(0, 0.2f).From().SetEase(Ease.OutBack);
        } else {
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Default");
            Camera.main.cullingMask |= 1 << LayerMask.NameToLayer("Interactables");
        }

        isDrawing = state;
        character.canMove = !state;
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
