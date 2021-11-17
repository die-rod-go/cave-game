using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRandom : MonoBehaviour
{
    public float speed = 0;
    public int deviation = 0;
    public float turningRadius = 0;
    public float xWeight, yWeight = 0;

    private Vector3 desiredDir;
    private Vector3 currentDir;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDir = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        updateSpriteScale();
    }

    private void FixedUpdate()
    {
        chooseDir();
        updateCurrentDir();
        move();
        drawDebugLine();
    }

    private void chooseDir()
    {
        float changeDirection = Random.Range(0, 100);

        if (changeDirection > deviation)
        {

            float delX = Random.Range(-xWeight, xWeight);
            float dely = Random.Range(-yWeight, yWeight);
            desiredDir = new Vector3(delX, dely, 0);
            desiredDir = desiredDir.normalized;            
        }
    }

    private void updateCurrentDir()
    {
        if (currentDir.y > desiredDir.y)
            currentDir = new Vector3(currentDir.x, currentDir.y - turningRadius, currentDir.z);
        else if(currentDir.y < desiredDir.y)
            currentDir = new Vector3(currentDir.x, currentDir.y + turningRadius, currentDir.z);

        if (currentDir.x > desiredDir.x)
            currentDir = new Vector3(currentDir.x - turningRadius, currentDir.y, currentDir.z);
        else if (currentDir.x < desiredDir.x)
            currentDir = new Vector3(currentDir.x + turningRadius, currentDir.y, currentDir.z);
        currentDir = currentDir.normalized;
    }

    private void move()
    {
        Vector2 newPos = transform.position + currentDir * Time.deltaTime;
        rb.MovePosition(newPos);
    }

    private void updateSpriteScale()
    {
        if (currentDir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3( 1, 1, 1);
    }

    private void drawDebugLine()
    {
        Debug.DrawLine(transform.position, transform.position + desiredDir, Color.white);
        Debug.DrawLine(transform.position, transform.position + currentDir, Color.red);
    }
}
