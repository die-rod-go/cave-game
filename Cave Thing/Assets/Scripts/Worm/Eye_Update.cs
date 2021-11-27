using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Update : MonoBehaviour
{
    Vector3 currentOffset;

    public float eyeOffset;
    public float eyeSpeed;
    public GameObject bait;
    private Animator animator;
    private Vector3 initialLocalPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialLocalPosition = transform.localPosition;

        animator = GetComponent<Animator>();
        if (animator == null)
            Debug.Log("animator");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("distBait", distanceToBait());
    }

    private void FixedUpdate()
    {
        lookAtPlayer();
    }

    private void lookAtPlayer()
    {
        transform.localPosition = initialLocalPosition;
        Vector3 offset = directionToBait() * eyeOffset * eyeSpeed * Time.fixedDeltaTime;
        transform.Translate(offset, Space.World);
    }

    private float distanceToBait()
    {
        return (bait.transform.position - transform.position).magnitude;
    }

    private Vector3 directionToBait()
    {
        return (bait.transform.position - transform.position).normalized;
    }
}
