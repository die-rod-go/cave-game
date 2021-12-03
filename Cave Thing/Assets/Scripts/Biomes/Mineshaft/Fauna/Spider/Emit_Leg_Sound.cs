using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Emit_Leg_Sound : MonoBehaviour
{

    public string[] makeSoundOn;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(makeSoundOn.Contains(collision.tag))
            source.Play();
    }
}
