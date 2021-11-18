using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedRandomizer : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float speedChange;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        animator.speed = speed;
    }

    private void Update()
    {
        int direction = Random.Range(-1, 2);
        float currentSpeed = animator.speed;

        currentSpeed += speedChange * direction *  Time.deltaTime;
        if (currentSpeed <= maxSpeed && currentSpeed >= minSpeed)
            animator.speed = currentSpeed;
    }
}
