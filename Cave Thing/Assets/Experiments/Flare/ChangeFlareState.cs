using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFlareState : MonoBehaviour
{
    [SerializeField] AudioClip ignite;
    [SerializeField] AudioClip burn;
    [SerializeField] AudioClip fizzle;
    [SerializeField] LightFlicker flicker;

    private AudioSource source;
    private Animator animator;


    // Start is called before the first frame update
    void OnEnable()
    {
        if(source == null)
            source = GetComponent<AudioSource>();

        if(animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    public void PlayIgnite()
    {
        source.clip = ignite;
    }

    public void PlayBurn()
    {
        source.clip = burn;
        source.loop = true;
        source.Play();
    }

    public void PlayFizzle()
    {
        EnableAnimator();
        animator.Play("Flare_Fizzle");
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }

    public void EnableAnimator()
    {
        animator.enabled = true;
    }
}
