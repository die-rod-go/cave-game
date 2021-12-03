using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Update_Segment : MonoBehaviour
{
    public float adjustmentSpeed;
    public float adjustmentIterations;
    public float segmentDistance;
    public float threshold;
    public Worm_AI head;
    public GameObject parentSegment;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 30;
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        lastPos = transform.position;
        if (distanceToParent() > segmentDistance + threshold)
            moveTowardsParent();
        updateSegmentRotation();
    }

    public float distanceToParent()
    {
        return (parentSegment.transform.position - transform.position).magnitude;
    }

    private Vector3 directionToParent()
    {
        Vector3 direction =  parentSegment.transform.position - transform.position;
        return direction.normalized;
    }

    
    private void moveTowardsParent()
    {
        Vector3 trans;
        for (int x = 0; x < adjustmentIterations; x++)
        {
            trans = directionToParent() * distanceToParent() * adjustmentSpeed * Time.fixedDeltaTime;
            transform.Translate(trans, Space.World);
            if (distanceToParent() < segmentDistance)
                break;
        }
    }

    private void updateSegmentRotation()
    {
        float magicNumber = 1000;
        transform.up = magicNumber * directionToParent() - transform.position;
    }

    private float getSpeed()
    {
        float speed = (transform.position - lastPos).magnitude / Time.deltaTime;
        Debug.Log(speed);
        return speed;
    }
}
