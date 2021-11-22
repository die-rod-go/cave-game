using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Worm_AI : MonoBehaviour
{
    public string[] canTunnel;
    public float turnSpeed;
    public float minSpeed, maxSpeed;
    public float minDistance, maxDistance;
    public float speedRamp;
    public float turnSpeedRamp;

    public float gravity;
    public float distToGrav;

    public float speed;
    private float desiredSpeed;
    public float realSpeed;

    public GameObject bait;  //  in order to get the player position
    private Vector3 desiredDir, currentDir;
    [SerializeField]
    public bool headUnderGround;

    private Vector3 prevPos;
    
    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 10;
    }
   
    // Update is called once per frame
    void Update()
    {       
        zeroZPos();
        updateHeadRoation();
        drawDebugLines();
    }

    private void FixedUpdate()
    {
        prevPos = transform.position;
        updatedesiredDir();
        turnTowardsBait();
        introduceOffset();
        moveInCurrentDir();
        updateSpeed();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canTunnel.Contains(collision.gameObject.tag))
            headUnderGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (canTunnel.Contains(collision.gameObject.tag))
            headUnderGround = false;
    }

    private void updatedesiredDir()
    {
        desiredDir = directionToBait();
    }

    public Vector3 directionToBait()
    {
        return (bait.transform.position - transform.position).normalized;
    }

    private void turnTowardsBait()
    {
        float turnRadius = Mathf.Rad2Deg * turnSpeed * Time.fixedDeltaTime;

        if (distanceToBait() < distToGrav)
            turnTowardsAngle(Vector2.down, turnRadius * gravity);
        else
            turnTowardsAngle(desiredDir, turnRadius);
    }

    private void updateSpeed()
    { 
        float distToBait;
        float percentage;
        float speedChange;

        distToBait = getDistanceToBait();
        percentage = 1 - (distToBait / (maxDistance - minDistance));
        if (percentage < 0)
            percentage = 0.01f;
        desiredSpeed = Mathf.Lerp(minSpeed, maxSpeed, percentage);

        speedChange = Mathf.Lerp(speed, desiredSpeed, percentage);
        //speed = speedChange;
        speedChange = speedChange - speed;
        speed += speedChange * speedRamp * Time.fixedDeltaTime;
    }

    private void moveInCurrentDir()
    {
        Vector3 trans;
        trans = currentDir * speed * Time.fixedDeltaTime;
        transform.Translate(trans, Space.World);
    }

    private void drawDebugLines()
    {
        Debug.DrawLine(transform.position, transform.position + desiredDir, Color.white);
        Debug.DrawLine(transform.position, transform.position + currentDir, Color.blue);
    }

    private void zeroZPos()  //  to prevent it from rotating on the 3d plane - sort of a workaround, please fix diego
    {
        Vector3 zeroZ = new Vector3(currentDir.x, currentDir.y, 0);
        Vector3 zeroZPos = new Vector3(transform.position.x, transform.position.y, 0);
        
        currentDir = zeroZ;
        transform.position = zeroZPos;
    }

    /*
     *  if the currentDir and desiredDir are at 180 degrees from 
     *  another the worm rotates about the z axis or gets stuck which we DONT WANT.
     *  this method introduces some extra rotation if it notices this 
     */
    private void introduceOffset()  
    {
        float correctionSpeed = 1.0f;
        float detect180 = 10.0F;
        float x, y;
        x = Random.Range(0.0f, 1.0f);
        y = Random.Range(0.0f, 1.0f);

        Vector3 randomDirection = new Vector3(x, y, 0).normalized;

        if (Vector2.Angle(currentDir, -desiredDir) < detect180)
        {
            turnTowardsAngle(randomDirection, correctionSpeed * Time.fixedDeltaTime);
        }

    }

    private void turnTowardsAngle(Vector3 angle, float turnRadius)
    {
        if (currentDir == null)
            currentDir = desiredDir;

        currentDir = Vector3.RotateTowards(currentDir, angle, turnRadius * Time.fixedDeltaTime, angle.magnitude);
        currentDir = currentDir.normalized;
    }

    private void updateHeadRoation()
    {
        float magicNumber = 1000;
        transform.up = magicNumber * currentDir - transform.position;
    }

    public float getDistanceToBait()//  used to update distance to bait an
    {
        return Mathf.Abs((transform.position - bait.transform.position).magnitude);
    }

    private float distanceToBait()
    {
        return (bait.transform.position - transform.position).magnitude;
    }

    public float getRealSpeed()
    {
        return Vector3.Distance(prevPos, transform.position) / Time.deltaTime;
    }
}
