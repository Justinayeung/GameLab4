using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MainMenuScript : MonoBehaviour
{
    public Animator Title;
    public Animator Black;
    public GameObject crackBG_01;
    public GameObject crackBG_02;
    public GameObject crackRockBG;
    public GameObject crackRockFront_01;
    public GameObject crackRockFront_02;
    public GameObject floatingSphere;
    public Camera mainCam;
    public string nextScene;
    public AudioSource drum;
    public AudioSource chime;
    public AudioSource rocks;
    [SerializeField] private float delay = 1f;
    public Animator spaceAnim;

    private void Awake() {
        floatingSphere.SetActive(false);
        crackBG_01.SetActive(false);
        crackBG_02.SetActive(false);
        crackRockBG.SetActive(false);
        crackRockFront_01.SetActive(false);
        crackRockFront_02.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(Begin());
        }
    }

    IEnumerator Begin() { 
        spaceAnim.SetBool("Off", true);
        drum.Play();
        Title.SetBool("Begin", true);
        yield return new WaitForSeconds(1.5f);
        mainCam.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        rocks.Play();
        crackBG_01.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        crackBG_02.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        crackRockFront_01.SetActive(true);
        crackRockFront_02.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        crackRockBG.SetActive(true);
        floatingSphere.SetActive(true);
        chime.Play();
        yield return new WaitForSeconds(2.3f);
        Black.SetBool("Play", true);
        StartCoroutine(FadeSound());
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(nextScene);
    }

    IEnumerator FadeSound() {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
 
        while(elapsedTime < delay) {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
    }
}
