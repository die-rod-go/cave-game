using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    //  walking sounds
    public AudioClip[] runDirtSounds;
    private AudioClip lastClip;

    //  jumping sounds
    public AudioClip jumpSound;

    private AudioSource playerSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        playerSoundSource = GetComponent<AudioSource>();
        lastClip = runDirtSounds[Random.Range(0, runDirtSounds.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void playFootstepSound()
    {
        do
        {
            playerSoundSource.clip = runDirtSounds[Random.Range(0, runDirtSounds.Length)];
        } while (lastClip == playerSoundSource.clip);
        lastClip = playerSoundSource.clip;
        playerSoundSource.Play();
    }

    public void playJumpSound()
    {
        playerSoundSource.clip = jumpSound;
        playerSoundSource.Play();
    }
}
