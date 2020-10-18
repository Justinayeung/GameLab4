using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;

    [Header ("Variables")]
    [SerializeField, Range(0, 100)] private float timeofDay;
    public float time;

    public void Update() {
        if(preset == null) { //Check if preset is assigned
            return;
        }

        if(Application.isPlaying) { //If application is playing, update time and lighting
            timeofDay += Time.deltaTime;
            timeofDay %= time; //Clamp between 0-24
            UpdateLighting(timeofDay / time);
        } else {
            UpdateLighting(timeofDay / time);
        }
    }

    /// <summary>
    /// Method for changing lighting settings based on time of day, evaluates preset variables based on the time
    /// </summary>
    /// <param name="timePercent"></param>
    public void UpdateLighting(float timePercent) {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if(directionalLight != null) { //Changing directional light rotation
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }

    public void OnValidate() {
        if(directionalLight != null) { //Check if directional light is assigned
            return;
        }

        if(RenderSettings.sun != null) {
            directionalLight = RenderSettings.sun;
        } else {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(Light light in lights) {
                if (light.type == LightType.Directional) {
                    directionalLight = light;
                    return;
                }
            }
        }
    }
}
