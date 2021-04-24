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

	//[Header("Particle References")]
 //   public Transform pillar1Particle;
 //   public Transform pillar2Particle;
 //   public Transform pillar3Particle;
 //   public Transform cageParticle;

	//[Header("UI References")]
	//public GameObject KeyRecipe;
	//public GameObject PathRecipe;

	[SerializeField]
	private List<Gesture> gestures = new List<Gesture>();

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
		//KeyRecipe.SetActive(false);
		//PathRecipe.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
		platform = Application.platform;
		drawArea = new Rect(0, 0, Screen.width, Screen.height);

		//Load pre-made gestures
		//TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		//foreach (TextAsset gestureXml in gesturesXml)
		//	trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

		//Load user custom gestures
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (string filePath in filePaths)
			trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
	
		//Load gestures
		TextAsset[] gestureFiles = Resources.LoadAll<TextAsset>("Gestures/");
		foreach (TextAsset gestureFile in gestureFiles)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureFile.text));

		StartCoroutine(LoadXmlFiles("DashLeft.xml"));
		StartCoroutine(LoadXmlFiles("DashRight.xml"));
		StartCoroutine(LoadXmlFiles("Fire.xml"));
		StartCoroutine(LoadXmlFiles("FireRight.xml"));
		StartCoroutine(LoadXmlFiles("Hat.xml"));
		StartCoroutine(LoadXmlFiles("Line.xml"));
		StartCoroutine(LoadXmlFiles("LineDown.xml"));
		StartCoroutine(LoadXmlFiles("Water1.xml"));
		StartCoroutine(LoadXmlFiles("Water2.xml"));
		StartCoroutine(LoadXmlFiles("Water3.xml"));
		StartCoroutine(LoadXmlFiles("Water4.xml"));
		StartCoroutine(LoadXmlFiles("Water5.xml"));

		string[] files = Directory.GetFiles(Application.dataPath, ".xml");
		foreach(string file in files)
				trainingSet.Add(GestureIO.ReadGestureFromFile(file));
	}

	IEnumerator LoadXmlFiles(string FileName) {
		gestures = new List<Gesture>();
		string folderPath = System.IO.Path.Combine(Application.streamingAssetsPath, FileName);
		yield return gestures;
		gestures.Add(GestureIO.ReadGestureFromFile(folderPath));
		//Debug.Log(folderPath);
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

        if(gestureResult.Score < 0.50f) {
			ClearVertexCount();
            ClearLine();
            return;
        }
        //Transform b = Instantiate(spherePrefab, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
        //b.DOScale(0, 0.2f).From().SetEase(Ease.OutBack);

        //Writing Earth
        if (!StaticClass.earth) {
			if(gestureResult.GestureClass == "Line" || gestureResult.GestureClass == "Hat" || gestureResult.GestureClass == "DashLeft") { 
				RaycastHit hit = new RaycastHit();
				if (Physics.SphereCast(gestureLinesRenderer[0].bounds.center, 3, Camera.main.transform.forward, out hit, 50, layerMask)) {
					if (hit.collider.CompareTag("EarthStroke1")) {
						//Transform b = Instantiate(pillar3Particle, gestureLinesRenderer[0].bounds.center, Quaternion.identity);
						//Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
						//StaticClass.pillar3Activated = true;
						StaticClass.earth1 = true;
						//Debug.Log(StaticClass.earth1);
					}

                    if (StaticClass.earth1 && StaticClass.earth2) {
						if(hit.collider.CompareTag("EarthStroke3")) { 
							StaticClass.earth3 = true;
							//Debug.Log(StaticClass.earth3);
						}
					}
				}
				ClearVertexCount();
			}

            if (StaticClass.earth1) {
				if(gestureResult.GestureClass == "LineDown" || gestureResult.GestureClass == "Water2" || gestureResult.GestureClass == "Fire") {
					RaycastHit hit = new RaycastHit();
					if (Physics.SphereCast(gestureLinesRenderer[0].bounds.center, 3, Camera.main.transform.forward, out hit, 50, layerMask)) { 
						if(hit.collider.CompareTag("EarthStroke2")) {
							StaticClass.earth2 = true;
							//Debug.Log(StaticClass.earth2);
						}
					}
					ClearVertexCount();
				}
			}
		}


		//Writing Water
		if(StaticClass.earth && !StaticClass.water) { 
			if(gestureResult.GestureClass == "LineDown" || gestureResult.GestureClass == "Water2" || gestureResult.GestureClass == "Fire") {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray, out hit)) {
					if(hit.collider.CompareTag("Water1")) {
                        StaticClass.water1 = true;
                    }
                }
				ClearVertexCount();
            }

			if(StaticClass.water1) {
				if(gestureResult.GestureClass == "Fire" || gestureResult.GestureClass == "Water1") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Water2")) {
							StaticClass.water2 = true;
						}
					}
					ClearVertexCount();
				}
            }

			if(StaticClass.water1 && StaticClass.water2) {
				if(gestureResult.GestureClass == "Water3" || gestureResult.GestureClass == "DashLeft") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Water3")) {
							StaticClass.water3 = true;
						}
					}
					ClearVertexCount();
				}
            }

			if(StaticClass.water1 && StaticClass.water2 && StaticClass.water3) {
				if(gestureResult.GestureClass == "Water4" || gestureResult.GestureClass == "DashRight" || gestureResult.GestureClass == "FireRight") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Water4")) {
							StaticClass.water4 = true;
						}
					}
					ClearVertexCount();
				}
            }
		}

		//Writing Wood
		if(StaticClass.earth && StaticClass.water && !StaticClass.wood) {
			//Debug.Log(gestureResult.GestureClass);
			if(gestureResult.GestureClass == "Line" || gestureResult.GestureClass == "Hat" || gestureResult.GestureClass == "DashLeft") { 
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray, out hit)) {
					if(hit.collider.CompareTag("Wood1")) {
						StaticClass.wood1 = true;
					}
				}
				ClearVertexCount();
			}

			if(StaticClass.wood1) {
				if(gestureResult.GestureClass == "LineDown" || gestureResult.GestureClass == "Water2" || gestureResult.GestureClass == "Fire") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Wood2")) {
							StaticClass.wood2 = true;
						}
					}
					ClearVertexCount();
				}
            }

			if(StaticClass.wood1 && StaticClass.wood2) {
				if(gestureResult.GestureClass == "Water3" || gestureResult.GestureClass == "DashLeft") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Wood3")) {
							StaticClass.wood3 = true;
						}
					}
					ClearVertexCount();
				}
            }

			if(StaticClass.wood1 && StaticClass.wood2 && StaticClass.wood3) {
				if(gestureResult.GestureClass == "Water4" || gestureResult.GestureClass == "DashRight" || gestureResult.GestureClass == "FireRight") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Wood4")) {
							StaticClass.wood4 = true;
						}
					}
					ClearVertexCount();
				}
            }

        }

		//Writing Fire
		if(StaticClass.earth && StaticClass.water && StaticClass.wood && !StaticClass.fire) {
			//Debug.Log(gestureResult.GestureClass);
			if(gestureResult.GestureClass == "Water4" || gestureResult.GestureClass == "DashRight" || gestureResult.GestureClass == "Fire" || gestureResult.GestureClass == "FireRight") {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray, out hit)) {
					if(hit.collider.CompareTag("Fire1")) {
						StaticClass.fire1 = true;
					}
				}
				ClearVertexCount();
			}

            if (StaticClass.fire1) {
				if(gestureResult.GestureClass == "Water3" || gestureResult.GestureClass == "DashLeft" || gestureResult.GestureClass == "Fire") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Fire2")) {
							StaticClass.fire2 = true;
						}
					}
					ClearVertexCount();
				}
            }

			if(StaticClass.fire1 && StaticClass.fire2) {
				if(gestureResult.GestureClass == "Water3" || gestureResult.GestureClass == "DathLeft" || gestureResult.GestureClass == "Fire"|| gestureResult.GestureClass == "LineDown" || gestureResult.GestureClass == "Water2") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Fire3")) {
							StaticClass.fire3 = true;
						}
					}
					ClearVertexCount();
				}
            }

			if(StaticClass.fire1 && StaticClass.fire2 && StaticClass.fire3) {
				if(gestureResult.GestureClass == "Water4" || gestureResult.GestureClass == "DashRight" || gestureResult.GestureClass == "FireRight") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Fire4")) {
							StaticClass.fire4 = true;
						}
					}
					ClearVertexCount();
				}
            }
        }

		//Writing Metal
		if(StaticClass.earth && StaticClass.water && StaticClass.wood && StaticClass.fire && !StaticClass.metal) {
			if(gestureResult.GestureClass == "Water3" || gestureResult.GestureClass == "DashLeft" || gestureResult.GestureClass == "Hat") {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit = new RaycastHit();
				if(Physics.Raycast(ray, out hit)) {
					if(hit.collider.CompareTag("Metal1")) {
						StaticClass.metal1 = true;
					}
				}
				ClearVertexCount();
			}

            if (StaticClass.metal1) {
				if(gestureResult.GestureClass == "Water4" || gestureResult.GestureClass == "DashRight" || gestureResult.GestureClass == "Hat" || gestureResult.GestureClass == "FireRight") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Metal2")) {
							StaticClass.metal2 = true;
						}
					}
					ClearVertexCount();
				}
			}

			if (StaticClass.metal1 && StaticClass.metal2) {
				if(gestureResult.GestureClass == "Line" || gestureResult.GestureClass == "Hat" || gestureResult.GestureClass == "DashLeft") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Metal3")) {
							StaticClass.metal3 = true;
						}
					}
					ClearVertexCount();
				}
			}

			if (StaticClass.metal1 && StaticClass.metal2 && StaticClass.metal3) {
				if(gestureResult.GestureClass == "Line" || gestureResult.GestureClass == "Hat" || gestureResult.GestureClass == "DashLeft") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Metal4")) {
							StaticClass.metal4 = true;
						}
					}
					ClearVertexCount();
				}
			}

			if (StaticClass.metal1 && StaticClass.metal2 && StaticClass.metal3 && StaticClass.metal4) {
				if(gestureResult.GestureClass == "LineDown" || gestureResult.GestureClass == "Water2") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Metal5")) {
							StaticClass.metal5 = true;
						}
					}
					ClearVertexCount();
				}
			}

			if (StaticClass.metal1 && StaticClass.metal2 && StaticClass.metal3 && StaticClass.metal4 && StaticClass.metal5) {
				if(gestureResult.GestureClass == "Water4" || gestureResult.GestureClass == "DashRight") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Metal6")) {
							StaticClass.metal6 = true;
						}
					}
					ClearVertexCount();
				}
			}

			if (StaticClass.metal1 && StaticClass.metal2 && StaticClass.metal3 && StaticClass.metal4 && StaticClass.metal5 && StaticClass.metal6) {
				if(gestureResult.GestureClass == "Water3" || gestureResult.GestureClass == "DashLeft") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Metal7")) {
							StaticClass.metal7 = true;
						}
					}
					ClearVertexCount();
				}
			}

			if (StaticClass.metal1 && StaticClass.metal2 && StaticClass.metal3 && StaticClass.metal4 && StaticClass.metal5 && StaticClass.metal6 && StaticClass.metal7) {
				if(gestureResult.GestureClass == "Line" || gestureResult.GestureClass == "Hat" || gestureResult.GestureClass == "DashLeft") {
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit = new RaycastHit();
					if(Physics.Raycast(ray, out hit)) {
						if(hit.collider.CompareTag("Metal8")) {
							StaticClass.metal8 = true;
						}
					}
					ClearVertexCount();
				}
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

	//IEnumerator KeyUI() {
 //       KeyRecipe.SetActive(true);
 //       yield return new WaitForSeconds(4f);
 //       KeyRecipe.SetActive(false);
 //   }

	//IEnumerator PathUI() {
 //       PathRecipe.SetActive(true);
 //       yield return new WaitForSeconds(4f);
 //       PathRecipe.SetActive(false);
 //   }
}
