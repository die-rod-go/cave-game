using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    private AudioSource playerSoundSource;
    // Start is called before the first frame update
    void Start()
    {
        playerSoundSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void playSound()
    {
        playerSoundSource.Play();
    }
}
