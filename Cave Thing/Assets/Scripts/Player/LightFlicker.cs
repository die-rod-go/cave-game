using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Range(0.0f, 5.0f)]
    public float minBrightness, maxBrightness;

    [Range(0.0f, 5.0f)]
    public float brightnessVariation; //    how quickly the brightness will change

    private Light2D light;

    private void Awake()
    {
        light = GetComponent<Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        light.intensity = Random.Range(minBrightness, maxBrightness);
    }

    // Update is called once per frame
    void Update()
    {
        int direction = Random.Range(-1, 2);
        float currentBrightness = light.intensity;

        currentBrightness += brightnessVariation * direction * Time.deltaTime;
        if (currentBrightness <= maxBrightness && currentBrightness >= minBrightness)
            light.intensity = currentBrightness;
    }
}
