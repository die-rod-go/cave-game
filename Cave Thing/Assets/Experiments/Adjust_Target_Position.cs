using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjust_Target_Position : MonoBehaviour
{

    public int raysToShoot = 30;
    public float maxDistance;
    public float eyeSight;
    private GameObject restingPosition;
    private Vector3 lastPos;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        restingPosition = transform.parent.Find("Resting_Position").gameObject;
        lastPos = getClosest();
        transform.position = lastPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (outOfBounds())
        {
            lastPos = getClosest();
        }

        transform.position = lastPos;
    }

    private bool outOfBounds()
    {
        return (lastPos - transform.position).magnitude > maxDistance;
    }

    private Vector3 getClosest()
    {
        Vector3 rest = restingPosition.transform.position;
        float angle = 0;
        Vector3 closest = lastPos;

        //  shoot rays
        for(int i = 0; i < raysToShoot; i++)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);

            angle += 2 * Mathf.PI / raysToShoot;

            Vector3 dir = new Vector3(rest.x + x, rest.y + y, 0);
            Vector3 directionTo = (dir - rest);
            RaycastHit2D hit = Physics2D.Raycast(rest, directionTo, eyeSight);

            if (hit)
                Debug.Log("Null");

            //  check if closest
            if ((hit.point - (Vector2)rest).magnitude < (closest - rest).magnitude)
                closest = hit.point;

            //Debug.DrawLine(rest, directionTo * eyeSight + rest, Color.blue);
            //Debug.DrawRay(rest, directionTo * eyeSight, Color.blue);
        }

        return closest;
    }
}
