using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Update : MonoBehaviour
{
    Vector3 currentOffset;

    public float eyeOffset;
    public float eyeSpeed;
    private Worm_AI head;
    private Animator animator;
    private Vector3 initialLocalPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialLocalPosition = transform.localPosition;
        head = transform.parent.GetComponent<Worm_AI>();
        if (head == null)
            Debug.Log("so no head?");

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.Log("animator");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("distBait", head.getDistanceToBait());
    }

    private void FixedUpdate()
    {
        lookAtPlayer();
    }

    private void lookAtPlayer()
    {
        transform.localPosition = initialLocalPosition;
        Vector3 offset = head.directionToBait() * eyeOffset * eyeSpeed * Time.fixedDeltaTime;
        transform.Translate(offset, Space.World);
    }
}
