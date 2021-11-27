using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pincher_Logic : MonoBehaviour
{
    public int raysToShoot;
    public float rayLength;
    public string[] canPinch;

    private GameObject checkWallPos;
    private AudioSource source;
    private Vector2 normal;

    // Start is called before the first frame update
    void Start()
    {
        checkWallPos = transform.parent.gameObject;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        getNormal();
        updateRotation();
        Debug.DrawRay(transform.position, normal, Color.red);
    }

    private Vector3 getNormal()
    {
        float angle = 0;
        Vector3 wall = checkWallPos.transform.position;

        //  shoot rays
        for (int i = 0; i < raysToShoot; i++)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);

            angle += 2 * Mathf.PI / raysToShoot;

            Vector3 dir = new Vector3(wall.x + x, wall.y + y, 0);
            Vector3 directionTo = (dir - wall);
            RaycastHit2D hit = Physics2D.Raycast(wall, directionTo, rayLength);

            if (hit)
            {
                if (canPinch.Contains(hit.transform.tag))
                    normal = hit.normal;
            }

            //Debug.DrawLine(rest, directionTo * eyeSight + rest, Color.blue);
            Debug.DrawRay(wall, directionTo * rayLength, Color.blue);
        }

        return normal;
    }

    private void updateRotation()
    {
        float magicNumber = 1000;
        transform.up = magicNumber * normal - (Vector2)transform.position;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        source.Play();
    }
}
