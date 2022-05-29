using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Range(0.0f, 3.0f)]
    public float minBrightness, maxBrightness;

    [Range(0.0f, 3.0f)]
    public float brightnessVariation; //    how quickly the brightness will change
    private UnityEngine.Rendering.Universal.Light2D mlight;

    private void Awake()
    {
        mlight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mlight.intensity = Random.Range(minBrightness, maxBrightness);
    }

    // Update is called once per frame
    void Update()
    {
        int direction = Random.Range(-1, 2);
        float currentBrightness = mlight.intensity;

        currentBrightness += brightnessVariation * direction * Time.deltaTime;
        if (currentBrightness <= maxBrightness && currentBrightness >= minBrightness)
            mlight.intensity = currentBrightness;
    }
}
