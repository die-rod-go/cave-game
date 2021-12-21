using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Adjust_Target_Position : MonoBehaviour
{
    [SerializeField] private string[] canWalk;
    [SerializeField] private float adjustmentSpeed = 10.0f;
    [SerializeField] private float minAdjustmentSpeed = 3.0f;
    [SerializeField] private float snapDistance = 10.0f;
    [SerializeField] private int raysToShoot = 30;
    [SerializeField] private float maxDistance;
    [SerializeField] private float maxError = 0.5f;
    [SerializeField] private float eyeSight;
    [SerializeField] private GameObject effector;

    private GameObject restingPosition;

    private Vector3 currentPos, lastPos;

    // Start is called before the first frame update
    void Start()
    {
        restingPosition = transform.parent.Find("Resting_Position").gameObject;
        currentPos = getClosest();
        lastPos = currentPos;
        transform.position = currentPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if (outOfBoundsRestingPosition() || outOfBoundsEffector())
            currentPos = getClosest();
    }

    void FixedUpdate()
    {
        moveToPosition();
        //transform.position = currentPos;
    }

    private bool outOfBoundsEffector()
    {
        return (effector.transform.position - transform.position).magnitude > maxError;
    }

    private bool outOfBoundsRestingPosition()
    {
        return (restingPosition.transform.position - transform.position).magnitude > maxDistance;
    }

    private Vector3 getClosest()
    {
        Vector3 rest = restingPosition.transform.position;
        float angle = 0;
        Vector3 closest = currentPos;

        //  shoot rays
        for(int i = 0; i < raysToShoot; i++)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);

            angle += 2 * Mathf.PI / raysToShoot;

            Vector3 dir = new Vector3(rest.x + x, rest.y + y, 0);
            Vector3 directionTo = (dir - rest);
            RaycastHit2D hit = Physics2D.Raycast(rest, directionTo, eyeSight);

            //  check if closest
            if (hit)
            {
                if ((hit.point - (Vector2)rest).magnitude < (closest - rest).magnitude)
                {
                    if (canWalk.Contains(hit.transform.tag))
                        closest = hit.point;
                }
            }

            //Debug.DrawLine(rest, directionTo * eyeSight + rest, Color.blue);
            Debug.DrawRay(rest, directionTo * maxDistance, Color.blue);
        }

        return closest;
    }

    private void moveToPosition()
    {
        if (distanceToCurrentPos() < snapDistance)
            transform.position = currentPos;
        else
        {
            if(distanceToCurrentPos() < 2)//    magic number
                transform.Translate(directionTocurrentPos().normalized * adjustmentSpeed * Time.fixedDeltaTime, Space.World);
            else
                transform.Translate(directionTocurrentPos() * minAdjustmentSpeed * Time.fixedDeltaTime, Space.World);
        }
    }

    private Vector3 directionTocurrentPos()
    {
        return currentPos - transform.position;
    }

    private float distanceToCurrentPos()
    {
        return (transform.position - currentPos).magnitude;
    }
}
