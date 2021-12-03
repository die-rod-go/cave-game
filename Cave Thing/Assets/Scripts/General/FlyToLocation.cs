using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyToLocation : MonoBehaviour
{
    public float speed = 0;
    public float turningRadius;
    public float eyeSight = 0;
    public float xWeight, yWeight = 0;
    public float updateFrequency = 0;

    public float arrivalThreshold = 0;

    private Vector3 startLocation;
    private Vector3 desiredDir;
    private Vector3 currentDir;

    private float timeSinceLast = 0;
    private Rigidbody2D rb;
    public string[] tagsToAvoid;

    private void Awake()
    {
        startLocation = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        updateCurrentDir();
        checkSurroundings();
        updateDirection();
        normalizeDesiredDir();
        move();
        updateSpriteScale();
        drawDebugLines();
    }

    private bool checkDirection(Vector2 direction)
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, eyeSight);
        foreach (RaycastHit2D h in hit)
            if (tagsToAvoid.Contains(h.collider.tag))
                return true;

        return false;
    }

    private void checkSurroundings()
    {
        if (checkDirection(desiredDir))
            chooseDir();

        if (checkDirection(Vector2.left))
            desiredDir = new Vector2(desiredDir.x + turningRadius * Time.deltaTime, desiredDir.y);

        if (checkDirection(Vector2.up))
            desiredDir = new Vector2(desiredDir.x, desiredDir.y - turningRadius * Time.deltaTime);

        if (checkDirection(Vector2.right))
            desiredDir = new Vector2(desiredDir.x - turningRadius * Time.deltaTime, desiredDir.y);

        if (checkDirection(Vector2.down))
            desiredDir = new Vector2(desiredDir.x, desiredDir.y + turningRadius * Time.deltaTime);
    }

    private void chooseDir()
    {
        float delX = Random.Range(-xWeight, xWeight);
        float dely = Random.Range(-yWeight, yWeight);
        desiredDir = new Vector3(delX, dely, 0);
    }

    void updateDirection()
    {
        timeSinceLast += Time.deltaTime;
        if (timeSinceLast >= updateFrequency)
        {
            desiredDir = startLocation - transform.position;
            timeSinceLast = 0;
        }
    }

    private void normalizeDesiredDir()
    {
        desiredDir = desiredDir.normalized;
    }

    private void updateCurrentDir()
    {
        if (currentDir.y > desiredDir.y)
            currentDir = new Vector3(currentDir.x, currentDir.y - turningRadius * Time.deltaTime);
        else if (currentDir.y < desiredDir.y)
            currentDir = new Vector3(currentDir.x, currentDir.y + turningRadius * Time.deltaTime);

        if (currentDir.x > desiredDir.x)
            currentDir = new Vector3(currentDir.x - turningRadius * Time.deltaTime, currentDir.y);
        else if (currentDir.x < desiredDir.x)
            currentDir = new Vector3(currentDir.x + turningRadius * Time.deltaTime, currentDir.y);
    }

    private void drawDebugLines()
    {
        Debug.DrawLine(transform.position, transform.position + desiredDir, Color.white);
        Debug.DrawLine(transform.position, transform.position + currentDir, Color.red);

        Debug.DrawLine(transform.position, transform.position + desiredDir * eyeSight, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.left * eyeSight, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.up * eyeSight, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.right * eyeSight, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * eyeSight, Color.blue);
    }

    private void move()
    {
        Vector2 newPos = transform.position + (currentDir * speed * Time.deltaTime);
        rb.MovePosition(newPos);
    }

    private void updateSpriteScale()
    {
        if (currentDir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3( 1, 1, 1);
    }

    public bool checkIfArrived()
    {
        Vector3 distance = startLocation - transform.position;
        return distance.magnitude <= arrivalThreshold;
    }
}
