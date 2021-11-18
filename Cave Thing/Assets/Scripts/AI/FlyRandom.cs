using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlyRandom : MonoBehaviour
{
    public float speed = 0;
    public float turningRadius = 0;

    public float minDecideTime, maxDecideTime;
    public float xWeight, yWeight = 0;
    public float eyeSight = 0;
    public float maxHeight = 0;

    public string[] tagsToAvoid;

    private Vector3 desiredDir;
    private Vector3 currentDir;
    private float timeToDecide = 1;
    private float timeSinceLastDecision = 0;

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
        timeSinceLastDecision += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        decideToTurn();
        updateCurrentDir();
        checkSurroundings();
        checkHeight();
        normalizeDesiredDir();
        move();
        drawDebugLines();
    }

    private void decideToTurn()
    {
        float changeDirection = Random.Range(0.0f, 1001.0f);
        if (timeSinceLastDecision >= timeToDecide)
            chooseDir();        
    }

    private void chooseDir()
    {
        float delX = Random.Range(-xWeight, xWeight);
        float dely = Random.Range(-yWeight, yWeight);
        desiredDir = new Vector3(delX, dely, 0);
        timeSinceLastDecision = 0;
        timeToDecide = Random.Range(minDecideTime, maxDecideTime);
    }

    private void updateCurrentDir()
    {
        if (currentDir.y > desiredDir.y)
            currentDir = new Vector3(currentDir.x, currentDir.y - turningRadius * Time.deltaTime, currentDir.z);
        else if(currentDir.y < desiredDir.y)
            currentDir = new Vector3(currentDir.x, currentDir.y + turningRadius * Time.deltaTime, currentDir.z);

        if (currentDir.x > desiredDir.x)
            currentDir = new Vector3(currentDir.x - turningRadius * Time.deltaTime, currentDir.y, currentDir.z);
        else if (currentDir.x < desiredDir.x)
            currentDir = new Vector3(currentDir.x + turningRadius * Time.deltaTime, currentDir.y, currentDir.z);
        //currentDir = currentDir.normalized;
    }

    private void move()
    {
        Vector2 newPos = transform.position + currentDir * speed * Time.deltaTime;
        rb.MovePosition(newPos);
    }

    private void updateSpriteScale()
    {
        if (currentDir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3( 1, 1, 1);
    }

    private void drawDebugLines()
    {
        Debug.DrawLine(transform.position, transform.position + desiredDir, Color.white);
        Debug.DrawLine(transform.position, transform.position + currentDir, Color.red);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * maxHeight, Color.green);

        Debug.DrawLine(transform.position, transform.position + desiredDir    * eyeSight, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.left  * eyeSight, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.up    * eyeSight, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.right * eyeSight, Color.blue);
        Debug.DrawLine(transform.position, transform.position + Vector3.down  * eyeSight, Color.blue);
    }

    private void checkHeight()
    {
        bool tooHigh = true;
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.down, maxHeight);
        foreach(RaycastHit2D h in hit)
        {
            if (h.collider.CompareTag("Ground"))
                tooHigh = false;
        }

        Debug.Log(tooHigh);
        if (tooHigh)
            correctHeight();
    }

    private bool checkDirection(Vector2 direction)
    {
        RaycastHit2D[] hit  = Physics2D.RaycastAll(transform.position, direction, eyeSight);
        foreach (RaycastHit2D h in hit)
            if (tagsToAvoid.Contains(h.collider.tag))
                return true;

        return false;
    }

    private void correctHeight()
    {
        if(desiredDir.y > 0)
            desiredDir = new Vector3(desiredDir.x, -desiredDir.y, 0);
    }

    private void checkSurroundings()
    {
        if (checkDirection(desiredDir))
        {
            chooseDir();
            Debug.Log("eye");
        }

        if (checkDirection(Vector2.left))
        {
            desiredDir = new Vector3(desiredDir.x + turningRadius * Time.deltaTime, desiredDir.y, 0);
            Debug.Log("left");
        }

        if (checkDirection(Vector2.up))
        {
            desiredDir = new Vector3(desiredDir.x, desiredDir.y - turningRadius * Time.deltaTime, 0);
            Debug.Log("up");
        }

        if (checkDirection(Vector2.right))
        {
            desiredDir = new Vector3(desiredDir.x - turningRadius * Time.deltaTime, desiredDir.y, 0);
            Debug.Log("right");
        }

        if (checkDirection(Vector2.down))
        {
            desiredDir = new Vector3(desiredDir.x, desiredDir.y + turningRadius * Time.deltaTime, 0);
            Debug.Log("down");
        }
    }
    private void normalizeDesiredDir()
    {
        desiredDir = desiredDir.normalized;
    }
}
