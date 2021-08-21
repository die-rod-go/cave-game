using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpParticleEmitter : MonoBehaviour
{
    public PlayerMovement player;
    public ParticleSystem dust;
    private bool flippedCorrect;

    // Start is called before the first frame update
    void Start()
    {
        flippedCorrect = player.getFacingLeft();
    }

    // Update is called once per frame
    void Update()
    {
        updateFacing();
        jumpParticles();
    }

    void jumpParticles()
    {      
        if (player.getJumpingNormal())
        {
            updateFacing();
            dust.Emit(1);
            player.setJumpingNormal(false);
        }
        else
        {
            //updateFacing();
            dust.Stop();
        }            
    }

    void updateFacing()
    {
        if (flippedCorrect != player.getFacingLeft())
        {
            Vector3 localScale = dust.transform.localScale;
            localScale.x *= -1;
            flippedCorrect = player.getFacingLeft();

            dust.transform.localScale = localScale;
        }
    }
}
