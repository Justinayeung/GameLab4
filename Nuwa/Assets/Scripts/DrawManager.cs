using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using PDollarGestureRecognizer;
using Cinemachine;
using UnityEditor;

using PDollarGestureRecognizer;

public class DrawManager : MonoBehaviour {
    private Vector3 loc;
    private GameManager gameManager;
    public LayerMask layerMask;

	public Transform gestureOnScreenPrefab;
    public Transform spherePrefab;

	private List<Gesture> trainingSet = new List<Gesture>();

	private List<Point> points = new List<Point>();
	private int strokeId = -1;

	private Vector3 virtualKeyPosition = Vector2.zero;
	private Rect drawArea;

	private RuntimePlatform platform;
	private int vertexCount = 0;

	private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	private LineRenderer currentGestureLineRenderer;

	//GUI
	private string message;
	private bool recognized;
	private string newGestureName = "";

	void Start () {
        gameManager = FindObjectOfType<GameManager>();
		platform = Application.platform;
		drawArea = new Rect(0, 0, Screen.width, Screen.height);

		//Load pre-made gestures
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

		//Load user custom gestures
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (string filePath in filePaths)
			trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
	}

	void Update () {
        //Exit
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }

        //Drawing
        if (!gameManager.isDrawing) {
            return;
        }

        if(gameManager.isDrawing) { 
		    if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
			    if (Input.touchCount > 0) {
				    virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			    }
		    } else {
			    if (Input.GetMouseButton(0)) {
				    virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			    }
		    }

		    if (drawArea.Contains(virtualKeyPosition)) {

			    if (Input.GetMouseButtonDown(0)) {

				    if (recognized) {

					    recognized = false;
					    strokeId = -1;

					    points.Clear();

					    foreach (LineRenderer lineRenderer in gestureLinesRenderer) {

						    lineRenderer.SetVertexCount(0);
						    Destroy(lineRenderer.gameObject);
					    }

					    gestureLinesRenderer.Clear();
				    }

				    ++strokeId;
				
				    Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
				    currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
				
				    gestureLinesRenderer.Add(currentGestureLineRenderer);
				
				    vertexCount = 0;
			    }
			
			    if (Input.GetMouseButton(0)) {
				    points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

				    currentGestureLineRenderer.SetVertexCount(++vertexCount);
				    currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
			    }
		    }
        }
	}

    public void TryRecognize() {
        if(points.Count <= 0) {
            return;
        }

        if(recognized) {
            ClearLine();
        }

        recognized = true;
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        if(gestureResult.Score < 0.75f) {
            ClearLine();
            return;
        }

        Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();

        if(gestureResult.GestureClass == "Circle") {
            Transform b = Instantiate(spherePrefab, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
            b.DOScale(0, 0.2f).From().SetEase(Ease.OutBack);

            if (recognized) {
                recognized = false;
                strokeId = -1;
                points.Clear();
                foreach(LineRenderer lineRenderer in gestureLinesRenderer) {
                    lineRenderer.SetVertexCount(0);
                    Destroy(lineRenderer.gameObject);
                }
                gestureLinesRenderer.Clear();
            }
        }
    }

    public void ClearLine() {
        recognized = false;
        strokeId = -1;
        points.Clear();
        foreach(LineRenderer lineRenderer in gestureLinesRenderer) {
            lineRenderer.positionCount = 0;
            Destroy(lineRenderer.gameObject);
        }
        gestureLinesRenderer.Clear();
    }
}
