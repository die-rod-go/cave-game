using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body_Audio : MonoBehaviour
{
    public float speedToMaxVolume;
    private AudioSource source;
    private Vector2 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float volume = getSpeed() / speedToMaxVolume;
        source.volume = volume;
        //Debug.Log(getSpeed());
        lastPos = transform.position;
    }

    private float getSpeed()
    {
        return ((Vector2)transform.position - lastPos).magnitude / Time.deltaTime;
    }
}
