using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollowPath : MonoBehaviour
{
    public string pathName;
    public float time;

    // Start is called before the first frame update
    void Start() {
        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath(pathName), "easetype", iTween.EaseType.easeInOutSine, "time", time));
        Delete();
    }

    void Delete() { 
        Destroy(this.gameObject, 2.1f);
    }
}
