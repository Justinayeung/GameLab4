using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StaticClass: MonoBehaviour
{
    public static bool monkeyKey = false; //If player has obtained the key

    //If the pillar particles are triggered
    public static bool pillar1Activated = false;
    public static bool pillar2Activated = false;
    public static bool pillar3Activated = false;

    public static bool monkeyFree = false; //If monkey is free (cage opened)

    public static bool brushObtained = false;

    public static bool earth = false; //If earth written
    public static bool earth1 = false;
    public static bool earth2 = false;
    public static bool earth3 = false;

    public static bool water = false; //If water written
    public static bool water1 = false;
    public static bool water2 = false;
    public static bool water3 = false;
    public static bool water4 = false;

    public static bool wood = false; //If wood written
    public static bool wood1 = false;
    public static bool wood2 = false;
    public static bool wood3 = false;
    public static bool wood4 = false;

    public static bool fire = false; //If fire written
    public static bool fire1 = false;
    public static bool fire2 = false;
    public static bool fire3 = false;
    public static bool fire4 = false;

    public static bool metal = false; //If metal written
    public static bool metal1 = false;
    public static bool metal2 = false;
    public static bool metal3 = false;
    public static bool metal4 = false;
    public static bool metal5 = false;
    public static bool metal6 = false;
    public static bool metal7 = false;
    public static bool metal8 = false;

    public AudioSource _Drum;
    public CinemachineVirtualCamera ogCam;
    public CinemachineVirtualCamera vCam1;
    public CinemachineVirtualCamera vCam2;
    public CinemachineVirtualCamera vCam3;
    public CinemachineVirtualCamera vCam4;
    public CinemachineVirtualCamera vCam5;
    public float timer;
    public float particleSpawnRate;
    public Animator earthText;
    public Animator waterText;
    public Animator woodText;
    public Animator fireText;
    public Animator metalText;
    bool earthOnce = false;
    bool waterOnce = false;
    bool woodOnce = false;
    bool fireOnce = false;
    bool metalOnce = false;

    [Header("Earth References")]
    public Animator earth1Anim;
    public Animator earth2Anim;
    public Animator earth3Anim;
    public BoxCollider earth1Col;
    public BoxCollider earth2Col;
    public BoxCollider earth3Col;
    public Animator Statue;
    public Animator StatueCircle;
    public MeshRenderer earthCircle;
    public Material unlitYellow;
    public Transform earth1Pos;
    public Transform earth2Pos;
    public Transform earth3Pos;
    public Transform earth1Particle;
    public Transform earth2Particle;
    public Transform earth3Particle;
    public GameObject earthCalLight;
    public Animator earthSymbol;
    public Animator mountain_01;
    public Animator mountain_02;

    [Header("Water References")]
    public Animator water1Anim;
    public Animator water2Anim;
    public Animator water3Anim;
    public Animator water4Anim;
    public BoxCollider water1Col;
    public BoxCollider water2Col;
    public BoxCollider water3Col;
    public BoxCollider water4Col;
    public GameObject waterCalLight;
    public GameObject waterCal;
    public MeshRenderer waterCircle;
    public Material unlitBlack;
    public Transform water1Pos;
    public Transform water2Pos;
    public Transform water3Pos;
    public Transform water4Pos;
    public Transform water1Particle;
    public Transform water2Particle;
    public Transform water3Particle;
    public Transform water4Particle;
    public Animator waterSymbol;
    public GameObject waterCube;
    public Renderer waterShader;
    public Animator rockWaterAnim;

    [Header("Wood References")]
    public Animator wood1Anim;
    public Animator wood2Anim;
    public Animator wood3Anim;
    public Animator wood4Anim;
    public BoxCollider wood1Col;
    public BoxCollider wood2Col;
    public BoxCollider wood3Col;
    public BoxCollider wood4Col;
    public GameObject woodCalLight;
    public GameObject woodCal;
    public MeshRenderer woodCircle;
    public Material unlitBlue;
    public Animator woodSymbol;
    public Transform wood1Pos;
    public Transform wood2Pos;
    public Transform wood3Pos;
    public Transform wood4Pos;
    public Transform wood1Particle;
    public Transform wood2Particle;
    public Transform wood3Particle;
    public Transform wood4Particle;
    public Animator Tree_01;

    [Header("Fire References")]
    public Animator fire1Anim;
    public Animator fire2Anim;
    public Animator fire3Anim;
    public Animator fire4Anim;
    public BoxCollider fire1Col;
    public BoxCollider fire2Col;
    public BoxCollider fire3Col;
    public BoxCollider fire4Col;
    public GameObject fireCalLight;
    public GameObject fireCal;
    public MeshRenderer fireCircle;
    public Material unlitRed;
    public Animator fireSymbol;
    public Transform fire1Pos;
    public Transform fire2Pos;
    public Transform fire3Pos;
    public Transform fire4Pos;
    public Transform fire1Particle;
    public Transform fire2Particle;
    public Transform fire3Particle;
    public Transform fired4Particle;

    [Header("Metal References")]
    public Animator metal1Anim;
    public Animator metal2Anim;
    public Animator metal3Anim;
    public Animator metal4Anim;
    public Animator metal5Anim;
    public Animator metal6Anim;
    public Animator metal7Anim;
    public Animator metal8Anim;
    public BoxCollider metal1Col;
    public BoxCollider metal2Col;
    public BoxCollider metal3Col;
    public BoxCollider metal4Col;
    public BoxCollider metal5Col;
    public BoxCollider metal6Col;
    public BoxCollider metal7Col;
    public BoxCollider metal8Col;
    public GameObject metalCalLight;
    public GameObject metalCal;
    public MeshRenderer metalCircle;
    public Material unlitWhite;
    public Animator metalSymbol;
    public Transform metal1Pos;
    public Transform metal2Pos;
    public Transform metal3Pos;
    public Transform metal4Pos;
    public Transform metal5Pos;
    public Transform metal6Pos;
    public Transform metal7Pos;
    public Transform metal8Pos;
    public Transform metal1Particle;
    public Transform metal2Particle;
    public Transform metal3Particle;
    public Transform metal4Particle;
    public Transform metal5Particle;
    public Transform metal6Particle;
    public Transform metal7Particle;
    public Transform metal8Particle;
    public Animator fadedMetal;

    private void Start() {
        //Camera priorities
        ogCam.Priority = 1;
        vCam1.Priority = 0;

        //Calligraphy SetActives
        waterCalLight.SetActive(false);
        waterCal.SetActive(false);
        woodCalLight.SetActive(false);
        woodCal.SetActive(false);
        fireCalLight.SetActive(false);
        fireCal.SetActive(false);
        metalCalLight.SetActive(false);
        metalCal.SetActive(false);

        waterCube.SetActive(false);
        waterShader = waterCube.GetComponent<Renderer>();
        rockWaterAnim.SetBool("Play", false);

        mountain_01.SetBool("Play", false);
        mountain_02.SetBool("Play", false);

        fadedMetal.SetBool("Play", false);

        earthText.SetBool("Play", false);
        waterText.SetBool("Play", false);
        woodText.SetBool("Play", false);
        fireText.SetBool("Play", false);
        metalText.SetBool("Play", false);
    }

    private void Update() {
        drawingEarth();
        drawingWater();
        drawingWood();
        drawingFire();
        drawingMetal();
    }

    /// <summary>
    /// Earth triggers and animations
    /// </summary>
    public void drawingEarth() {
        if (earth1 && earth2 && earth3) { //Checking if all strokes were drawn
            earth = true;
        }

        if (brushObtained && !earth1) {
            spawnParticle(earth1Particle, earth1Pos);
        }

        if (earth1) {
            earth1Anim.SetBool("Play", true);
            earth1Col.enabled = false;
            earth2Col.enabled = true;
        } else {
            earth1Col.enabled = true;
            earth2Col.enabled = false;
            earth3Col.enabled = false;
        }

        if(earth1 && !earth2) { 
            spawnParticle(earth2Particle, earth2Pos);
        }

        if (earth2) {
            earth2Anim.SetBool("Play", true);
            earth2Col.enabled = false;
            earth3Col.enabled = true;
        }

        if(earth1 && earth2 && !earth3) {
            spawnParticle(earth3Particle, earth3Pos);
        }

        if (earth3) {
            earth3Anim.SetBool("Play", true);
        }

        if(earth) { //If earth has been fully written
            if (!earthOnce) {
                StartCoroutine(earthWritten());
            }
        }

    }

    IEnumerator earthWritten() {
        ogCam.Priority = 0;
        vCam1.Priority = 1;
        earthCalLight.SetActive(false);
        yield return new WaitForSeconds(2f);
        earthText.SetBool("Play", true);
        yield return new WaitForSeconds(1f);
        mountain_01.SetBool("Play", true);
        yield return new WaitForSeconds(0.2f);
        mountain_02.SetBool("Play", true);
        yield return new WaitForSeconds(1.1f);
        Statue.SetBool("Play", true);
        yield return new WaitForSeconds(1f);
        StatueCircle.SetBool("Play", true);
        yield return new WaitForSeconds(3f);
        earthSymbol.SetBool("Play", true);
        yield return new WaitForSeconds(1.2f);
        earthCircle.material = unlitYellow;
        yield return new WaitForSeconds(0.2f);
        earthText.SetBool("Play", false);
        yield return new WaitForSeconds(1f);
        vCam1.Priority = 0;
        ogCam.Priority = 1;
        waterCalLight.SetActive(true);
        waterCal.SetActive(true);
        earthOnce = true;
    }

    /// <summary>
    /// Water triggers and animations
    /// </summary>
    public void drawingWater() { 
        if (water1 && water2 && water3 && water4) { //Checking if all strokes were drawn
            water = true;
        }

        if (!water1 && earth) {
            spawnParticle(water1Particle, water1Pos);
        }

        if (water1) {
            water1Anim.SetBool("Play", true);
            water1Col.enabled = false;
            water2Col.enabled = true;
        } else {
            water1Col.enabled = true;
            water2Col.enabled = false;
            water3Col.enabled = false;
            water4Col.enabled = false;
        }

        if(water1 && !water2) {
            spawnParticle(water2Particle, water2Pos);
        }

        if (water2) {
            water2Anim.SetBool("Play", true);
            water2Col.enabled = false;
            water3Col.enabled = true;
        }

        if (water1 && water2 && !water3) {
            spawnParticle(water3Particle, water3Pos);
        }

        if (water3) {
            water3Anim.SetBool("Play", true);
            water3Col.enabled = false;
            water4Col.enabled = true;
        }

        if (water1 && water2 && water3 && !water4) {
            spawnParticle(water4Particle, water4Pos);
        }

        if (water4) {
            water4Anim.SetBool("Play", true);
        }

        if (water) { //If water has been fully written
            if (!waterOnce) {
                StartCoroutine(waterWritten());
            }

            if (waterOnce) {
                StartCoroutine(waterShaderMove());
            }
        }
    }

    IEnumerator waterWritten() { 
        ogCam.Priority = 0;
        vCam2.Priority = 1;
        waterCalLight.SetActive(false);
        yield return new WaitForSeconds(2f);
        waterText.SetBool("Play", true);
        yield return new WaitForSeconds(1f);
        waterCube.SetActive(true);
        waterShader.material.SetFloat("_WaveSpeed", 0f);
        waterShader.material.SetFloat("_WaveScale", 0f);
        yield return new WaitForSeconds(1f);
        rockWaterAnim.SetBool("Play", true);
        yield return new WaitForSeconds(2f);
        waterSymbol.SetBool("Play", true);
        yield return new WaitForSeconds(2f);
        waterCircle.material = unlitBlack;
        yield return new WaitForSeconds(0.2f);
        waterText.SetBool("Play", false);
        yield return new WaitForSeconds(1f);
        vCam2.Priority = 0;
        ogCam.Priority = 1;
        woodCalLight.SetActive(true);
        woodCal.SetActive(true);
        waterOnce = true;
    }

    IEnumerator waterShaderMove() {
        yield return new WaitForSeconds(3.5f);
        waterShader.material.SetFloat("_WaveSpeed", 1f);
        waterShader.material.SetFloat("_WaveScale", 1.7f);
    }

    /// <summary>
    /// Wood triggers and animations
    /// </summary>
    public void drawingWood() { 
        if (wood1 && wood2 && wood3 && wood4) { //Checking if all strokes were drawn
            wood = true;
        }

        if (water && !wood1) {
            spawnParticle(wood1Particle, wood1Pos);
        }

        if (wood1) {
            wood1Anim.SetBool("Play", true);
            wood1Col.enabled = false;
            wood2Col.enabled = true;
        } else {
            wood1Col.enabled = true;
            wood2Col.enabled = false;
            wood3Col.enabled = false;
            wood4Col.enabled = false;
        }

        if (wood1 && !wood2) {
            spawnParticle(wood2Particle, wood2Pos);
        }

        if (wood2) {
            wood2Anim.SetBool("Play", true);
            wood2Col.enabled = false;
            wood3Col.enabled = true;
        }

        if (wood1 && wood2 && !wood3) {
            spawnParticle(wood3Particle, wood3Pos);
        }

        if (wood3) {
            wood3Anim.SetBool("Play", true);
            wood3Col.enabled = false;
            wood4Col.enabled = true;
        }

        if (wood1 && wood2 && wood3 && !wood4) {
            spawnParticle(wood4Particle, wood4Pos);
        }

        if (wood4) {
            wood4Anim.SetBool("Play", true);
        }

        if (wood) { //If wood has been fully written
            if (!woodOnce) {
                StartCoroutine(woodWritten());
            }
        }
    }

    IEnumerator woodWritten() { 
        ogCam.Priority = 0;
        vCam3.Priority = 1;
        woodCalLight.SetActive(false);
        yield return new WaitForSeconds(2f);
        woodText.SetBool("Play", true);
        yield return new WaitForSeconds(1f);
        Tree_01.SetBool("Play", true);
        yield return new WaitForSeconds(4.2f);
        woodSymbol.SetBool("Play", true);
        yield return new WaitForSeconds(1.4f);
        woodCircle.material = unlitBlue;
        yield return new WaitForSeconds(0.2f);
        woodText.SetBool("Play", false);
        yield return new WaitForSeconds(1f);
        vCam3.Priority = 0;
        ogCam.Priority = 1;
        fireCalLight.SetActive(true);
        fireCal.SetActive(true);
        woodOnce = true;
    }

    /// <summary>
    /// Fire triggers and animations
    /// </summary>
    public void drawingFire() { 
        if (fire1 && fire2 && fire3 && fire4) { //Checking if all strokes were drawn
            fire = true;
        }

        if (wood && !fire1) {
            spawnParticle(fire1Particle, fire1Pos);
        }

        if (fire1) {
            fire1Anim.SetBool("Play", true);
            fire1Col.enabled = false;
            fire2Col.enabled = true;
        } else {
            fire1Col.enabled = true;
            fire2Col.enabled = false;
            fire3Col.enabled = false;
            fire4Col.enabled = false;
        }

        if (fire1 && !fire2) { 
            spawnParticle(fire2Particle, fire2Pos);
        }

        if (fire2) {
            fire2Anim.SetBool("Play", true);
            fire2Col.enabled = false;
            fire3Col.enabled = true;
        }

        if (fire1 && fire2 && !fire3) {
            spawnParticle(fire3Particle, fire3Pos);
        }

        if (fire3) {
            fire3Anim.SetBool("Play", true);
            fire3Col.enabled = false;
            fire4Col.enabled = true;
        }

        if (fire1 && fire2 && fire3 && !fire4) {
            spawnParticle(fired4Particle, fire4Pos);
        }

        if (fire4) {
            fire4Anim.SetBool("Play", true);
        }

        if (fire) { //If fire has been fully written
            if (!fireOnce) {
                StartCoroutine(fireWritten());
            }
        }
    }

    IEnumerator fireWritten() { 
        ogCam.Priority = 0;
        vCam4.Priority = 1;
        fireCalLight.SetActive(false);
        yield return new WaitForSeconds(2f);
        fireText.SetBool("Play", true);
        yield return new WaitForSeconds(1f);
        fireSymbol.SetBool("Play", true);
        yield return new WaitForSeconds(1.4f);
        fireCircle.material = unlitRed;
        yield return new WaitForSeconds(0.2f);
        fireText.SetBool("Play", false);
        yield return new WaitForSeconds(1f);
        vCam4.Priority = 0;
        ogCam.Priority = 1;
        metalCalLight.SetActive(true);
        metalCal.SetActive(true);
        fireOnce = true;
    }

    /// <summary>
    /// Metal triggers and animations
    /// </summary>
    public void drawingMetal() {
        if (metal1 && metal2 && metal3 && metal4 && metal5 && metal6 && metal7 && metal8) { //Checking if all strokes were drawn
            metal = true;
        }

        if (fire && !metal1) {
            fadedMetal.SetBool("Play", true);
            StartCoroutine(waitSpawnParticle());
        }

        if (metal1) {
            metal1Anim.SetBool("Play", true);
            metal1Col.enabled = false;
            metal2Col.enabled = true;
        } else {
            metal1Col.enabled = true;
            metal2Col.enabled = false;
            metal3Col.enabled = false;
            metal4Col.enabled = false;
            metal5Col.enabled = false;
            metal6Col.enabled = false;
            metal7Col.enabled = false;
            metal8Col.enabled = false;
        }

        if (metal1 && !metal2) {
            spawnParticle(metal2Particle, metal2Pos);
        }

        if (metal2) {
            metal2Anim.SetBool("Play", true);
            metal2Col.enabled = false;
            metal3Col.enabled = true;
        }

        if (metal1 && metal2 && !metal3) {
            spawnParticle(metal3Particle, metal3Pos);
        }

        if (metal3) {
            metal3Anim.SetBool("Play", true);
            metal3Col.enabled = false;
            metal4Col.enabled = true;
        }

        if (metal1 && metal2 && metal3 && !metal4) {
            spawnParticle(metal4Particle, metal4Pos);
        }

        if (metal4) {
            metal4Anim.SetBool("Play", true);
            metal4Col.enabled = false;
            metal5Col.enabled = true;
        }

        if (metal1 && metal2 && metal3 && metal4 && !metal5) {
            spawnParticle(metal5Particle, metal5Pos);
        }

        if (metal5) {
            metal5Anim.SetBool("Play", true);
            metal5Col.enabled = false;
            metal6Col.enabled = true;
        }

        if (metal1 && metal2 && metal3 && metal4 && metal5 && !metal6) {
            spawnParticle(metal6Particle, metal6Pos);
        }

        if (metal6) {
            metal6Anim.SetBool("Play", true);
            metal6Col.enabled = false;
            metal7Col.enabled = true;
        }

        if (metal1 && metal2 && metal3 && metal4 && metal5 && metal6 && !metal7) {
            spawnParticle(metal7Particle, metal7Pos);
        }

        if (metal7) {
            metal7Anim.SetBool("Play", true);
            metal7Col.enabled = false;
            metal8Col.enabled = true;
        }

        if (metal1 && metal2 && metal3 && metal4 && metal5 && metal6 && metal7 && !metal8) {
            spawnParticle(metal8Particle, metal8Pos);
        }

        if (metal8) {
            metal8Anim.SetBool("Play", true);
        }

        if (metal) { //If metal has been fully written
            if (!metalOnce) {
                StartCoroutine(metalWritten());
            }
        }
    }

    IEnumerator waitSpawnParticle() { 
        yield return new WaitForSeconds(4f);
        spawnParticle(metal1Particle, metal1Pos);
    }

    IEnumerator metalWritten() { 
        ogCam.Priority = 0;
        vCam5.Priority = 1;
        metalCalLight.SetActive(false);
        yield return new WaitForSeconds(2f);
        metalText.SetBool("Play", true);
        yield return new WaitForSeconds(1f);
        metalSymbol.SetBool("Play", true);
        yield return new WaitForSeconds(2f);
        metalCircle.material = unlitWhite;
        yield return new WaitForSeconds(0.2f);
        metalText.SetBool("Play", false);
        yield return new WaitForSeconds(1f);
        vCam5.Priority = 0;
        ogCam.Priority = 1;
        metalOnce = true;
    }

    /// <summary>
    /// Spawn particle everything x seconds
    /// </summary>
    /// <param name="particle"></param>
    /// <param name="pos"></param>
    void spawnParticle(Transform particle, Transform pos) {
        if (timer <= 0) { 
            Transform b = Instantiate(particle, pos.position, Quaternion.identity);
            timer = particleSpawnRate;
        }
        timer -= Time.deltaTime;
    }
}
