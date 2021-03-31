using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using PDollarGestureRecognizer;
using Cinemachine;
using UnityEditor;

public class DrawManager : MonoBehaviour 
{
    private Vector3 loc;
    private GameManager gameManager;
    public LayerMask layerMask;
    public LayerMask layerMaskKey;
    public LayerMask layerMaskPaths;

    [Header("References")]
	public Transform gestureOnScreenPrefab;
    public Transform spherePrefab;
    public Transform keyPrefab;
	public Transform lightOrb;
	public GameObject path1;
	public GameObject keyDrawPath;

	[Header("Particle References")]
    public Transform pillar1Particle;
    public Transform pillar2Particle;
    public Transform pillar3Particle;
    public Transform cageParticle;

	[Header("UI References")]
	public GameObject KeyRecipe;
	public GameObject PathRecipe;

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
		KeyRecipe.SetActive(false);
		PathRecipe.SetActive(false);
		path1.SetActive(false);
		keyDrawPath.SetActive(false);
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
			ClearVertexCount();
            ClearLine();
        }

        recognized = true;
        Gesture candidate = new Gesture(points.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());

        if(gestureResult.Score < 0.60f) {
			ClearVertexCount();
            ClearLine();
            return;
        }
		//Transform b = Instantiate(spherePrefab, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
		//b.DOScale(0, 0.2f).From().SetEase(Ease.OutBack);
		
		//First Puzzle
		if(gestureResult.GestureClass == "KeyTip") {
			if(!StaticClass.monkeyKey) {
				RaycastHit hit = new RaycastHit();
				if (Physics.SphereCast(gestureLinesRenderer[0].bounds.center, 10, Camera.main.transform.forward, out hit, 25, layerMask)) {
					//Debug.Log("Key3");
					//Debug.Log(hit.transform.name);
					if (hit.collider.CompareTag("Key3")) {
						Transform b = Instantiate(pillar3Particle, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
						Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
						StaticClass.pillar3Activated = true;
					}
				}
				ClearVertexCount();
			}
		}

		if(gestureResult.GestureClass == "Line") {
			if(!StaticClass.monkeyKey && !StaticClass.pillar2Activated) {
				RaycastHit hit = new RaycastHit();
				if (Physics.SphereCast(gestureLinesRenderer[0].bounds.center, 10, Camera.main.transform.forward, out hit, 25, layerMaskKey)) {
					//Debug.Log("Key2");
					//Debug.Log(hit.transform.name);
					if (hit.collider.CompareTag("Key2")) {
						Transform b = Instantiate(pillar2Particle, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
						StartCoroutine(PathUI());
						Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
						StaticClass.pillar2Activated = true;
					}
				}
				ClearVertexCount();
			}
        }

		if(gestureResult.GestureClass == "Line") {
			RaycastHit hit = new RaycastHit();
			if (Physics.SphereCast(gestureLinesRenderer[0].bounds.center, 10, Camera.main.transform.forward, out hit, 25, layerMaskPaths)) {
				//Debug.Log("Path1");
				//Debug.Log(hit.transform.name);
				if (hit.collider.CompareTag("CreatePath1")) {
					path1.SetActive(true);
					Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
				}
			}
			ClearVertexCount();
        }

        if(gestureResult.GestureClass == "Circle") {
            if (StaticClass.monkeyFree) {
				Transform b = Instantiate(lightOrb, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
				b.DOScale(0, 0.2f).From().SetEase(Ease.OutBack);
            }

			if(!StaticClass.monkeyKey && !StaticClass.pillar1Activated) {
				RaycastHit hit = new RaycastHit();
				if (Physics.SphereCast(gestureLinesRenderer[0].bounds.center, 10, Camera.main.transform.forward, out hit, 25, layerMask)) {
					//Debug.Log("Key1");
					//Debug.Log(hit.transform.name);
					if (hit.collider.CompareTag("Key1")) {
						Transform b = Instantiate(pillar1Particle, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
						Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
						StaticClass.pillar1Activated = true;
					}
				}
				ClearVertexCount();
			}
		}

		if(StaticClass.pillar1Activated && StaticClass.pillar2Activated && StaticClass.pillar3Activated && !StaticClass.monkeyKey){
			keyDrawPath.SetActive(true);
            if (gestureResult.GestureClass == "Key") {
                RaycastHit hit = new RaycastHit();
                if (Physics.SphereCast(gestureLinesRenderer[0].bounds.center, 10, Camera.main.transform.forward, out hit, 25, layerMaskKey)) {
                    //Debug.Log("Key");
                    //Debug.Log(hit.transform.name);
                    if (hit.collider.CompareTag("Key2")) {
                        Transform b = Instantiate(cageParticle, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
                        StartCoroutine(KeyUI());
                        StaticClass.monkeyKey = true;
                        Debug.Log("Key Obtained");
                        Debug.Log(StaticClass.monkeyKey);
                    }
                }
                ClearVertexCount();
            }
        }

		if(StaticClass.monkeyKey) {
				if(gestureResult.GestureClass == "Key") {
					RaycastHit hit = new RaycastHit();
					if (Physics.SphereCast(gestureLinesRenderer[0].bounds.center, 10, Camera.main.transform.forward, out hit, 20, layerMask)) {
						Debug.Log("Key");
						Debug.Log(hit.transform.name);
						if (hit.collider.CompareTag("Cage")) {
							hit.collider.GetComponent<OpenLock>().OpeningLock();
							hit.collider.GetComponent<Monkey>().FreeMonkey();
							Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
						}
						
						if(hit.collider.CompareTag("PathLock")) { 
							hit.collider.GetComponent<OpenPathTesting>().OpeningPath();
							Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
						}
					}
					ClearVertexCount();
				}
			}
    }

    public void ClearVertexCount() {
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

	IEnumerator KeyUI() {
        KeyRecipe.SetActive(true);
        yield return new WaitForSeconds(4f);
        KeyRecipe.SetActive(false);
    }

	IEnumerator PathUI() {
        PathRecipe.SetActive(true);
        yield return new WaitForSeconds(4f);
        PathRecipe.SetActive(false);
    }
}
