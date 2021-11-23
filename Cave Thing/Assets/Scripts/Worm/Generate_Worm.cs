using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate_Worm : MonoBehaviour
{
    [Header("Head Parameters")]
    public string[] canTunnel;
    public float turnSpeed;
    public float minSpeed, maxSpeed;
    public float minDistance, maxDistance;
    [Range(0.0f, 5.0f)]
    public float speedRamp;
    [Range(0.0f, 20.0f)]
    public float turnSpeedRamp;
    public float gravity;
    public float distToGrav;
    public string baitName;
    private GameObject bait;

    [Header("Segment Parameters")]
    public float adjustmentSpeed;
    public int adjustmentIterations;
    public float segmentDistance;
    public float threshold;

    [Space(10)]     
    public GameObject headObject;
    public GameObject bodyObject;
    public GameObject tailObject;

    [Space(10)] 
    public int minLength, maxLength;
    public bool debug;

    private int length;
    private GameObject headSegment;
    private List<GameObject> bodySegments;
    private GameObject tailSegment;

    // Start is called before the first frame update
    void Start()
    {
        bait = GameObject.Find(baitName);
        bodySegments = new List<GameObject>();
        length = Random.Range(minLength, maxLength);
        createWorm();
    }

    private void FixedUpdate()
    {
        if (debug)
            debugUpdate();
    }

    private void createWorm()
    {
        insantiateHead();
        setHeadParameters();
        insantiateSegments();
        instantiateTail();
    }

    private void insantiateHead()
    {
        headSegment = Instantiate(headObject, transform.position, Quaternion.identity);
        headSegment.transform.parent = transform;
    }

    private void setHeadParameters()
    {
        Worm_AI headAi = headSegment.GetComponent<Worm_AI>();
        headAi.canTunnel = canTunnel;
        headAi.turnSpeed = turnSpeed;
        headAi.minSpeed = minSpeed;
        headAi.maxSpeed = maxSpeed;
        headAi.minDistance = minDistance;
        headAi.maxDistance = maxDistance;
        headAi.speedRamp = speedRamp;
        headAi.turnSpeedRamp = turnSpeedRamp;
        headAi.gravity = gravity;
        headAi.distToGrav = distToGrav;
        headAi.bait = bait;
    }

    private void insantiateSegments()
    {
        for (int j = 0; j < length; j++)
            instantiateBodySegment(j);

        tetherSegments();
    }

    private void instantiateBodySegment(int num)
    {
        Vector3 offset = new Vector3(0, num * -segmentDistance * 1.8f, 0);

        GameObject tempBody = Instantiate(bodyObject, transform.position + offset, Quaternion.identity);
        tempBody.transform.parent = transform;
        bodySegments.Add(tempBody);
    }

    private void tetherSegments()
    {
        Update_Segment temp;
        GameObject prevSegment = null;
        int counter = 0;

        foreach(GameObject segment in bodySegments)
        {
            temp = segment.GetComponent<Update_Segment>();
            segment.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0 - counter;
            if (prevSegment == null)
                temp.parentSegment = headSegment.transform.Find("Head").gameObject;
            else
            {
                temp.parentSegment = prevSegment.transform.Find("Body").gameObject;
            }
            setSegmentParameters(temp);
            prevSegment = segment;
            counter++;
        }
    }

    private void setSegmentParameters(Update_Segment segment)
    {
        segment.adjustmentIterations = adjustmentIterations;
        segment.adjustmentSpeed = adjustmentSpeed;
        segment.segmentDistance = segmentDistance;
        segment.threshold = threshold;
        segment.head = headSegment.GetComponent<Worm_AI>();
    }

    private void instantiateTail()
    {
        Update_Segment temp;
        Vector3 offset = new Vector3(0, bodySegments.Count * -segmentDistance, 0);
        tailSegment = Instantiate(tailObject, transform.position + offset, Quaternion.identity);
        tailSegment.transform.parent = transform;

        temp = tailSegment.GetComponent<Update_Segment>();
        temp.parentSegment = bodySegments[bodySegments.Count - 1].transform.Find("Body").gameObject;
        setSegmentParameters(temp);
        temp.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 0 - length;
    }
    public Vector3 getClosestSegmentPosition()
    {
        Vector3 pos = new Vector3();

        foreach(GameObject segment in bodySegments)
        {
            float distance = (segment.transform.position - bait.transform.position).magnitude;
            float distanceToPos = (pos - bait.transform.position).magnitude;

            if (distance < distanceToPos)
                pos = segment.transform.position;
        }

        return pos;
    }

    private void debugUpdate()
    {
        setHeadParameters();
        foreach (GameObject segment in bodySegments)
            setSegmentParameters(segment.GetComponent<Update_Segment>());

        setSegmentParameters(tailSegment.GetComponent<Update_Segment>());
    }
}
