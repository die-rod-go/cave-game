using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLightState : MonoBehaviour
{
    [SerializeField] AudioClip ignite;
    [SerializeField] AudioClip burn;
    [SerializeField] string fizzleName;

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

    public void PlayIgniteSound()
    {
        if(ignite != null)
            source.clip = ignite;
    }

    public void PlayBurnSound()
    {
        if (burn != null)
        {
            source.clip = burn;
            source.loop = true;
            source.Play();
        }
    }

    public void PlayFizzleAnimation()
    {
        if (fizzleName != "")
        {
            EnableAnimator();
            animator.Play(fizzleName);
        }
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
