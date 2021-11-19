using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameobjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToBeSpawned;
    public bool deleteExcessObjects;
    public int numToBeSpawned = 0;
    public int numSpawned = 0;

    public float minSpawnTime, maxSpawnTime;
    private float spawnThreshold = 0; //    how long until the next object spawns

    private float spawnTimer = 0;
    private List<spawnedObject> objectList;

    private void Awake()
    {
        objectList = new List<spawnedObject>();

        for (int x = 0; x < numToBeSpawned; x++)
        {
            spawnedObject temp = new spawnedObject(Instantiate(objectToBeSpawned, transform.position, Quaternion.identity), transform);
            objectList.Add(temp);
            objectList[x].obj.transform.parent = transform;
            objectList[x].obj.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        updateLifetimes();
        checkLiving();
    }

    private void spawnObject()
    {
        foreach(spawnedObject spwn in objectList)
        {
            if (!spwn.getEnabled())
            {
                spwn.enable();
                numSpawned++;
                break;
            }
        }
    }

    private void destroyObject()
    {
        spawnedObject largest = new spawnedObject(objectList[0].obj, transform);
        foreach (spawnedObject spawn in objectList)
        {
            if (spawn.lifeTime > largest.lifeTime)
                largest = spawn;
        }
        numSpawned--;
        largest.disable();
    }

    void updateTimer()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnThreshold)
        {
            if (numSpawned < numToBeSpawned)
                spawnObject();
            else if(deleteExcessObjects)
                destroyObject();

            spawnTimer = 0;
            spawnThreshold = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    void updateLifetimes()
    {
        foreach (spawnedObject obj in objectList)
            obj.update();
    }

    public void checkLiving()
    {
        int numLiving = 0;
        foreach (spawnedObject spawn in objectList)
        {
            if (spawn.obj.activeSelf)
                numLiving++;
        }

        numSpawned = numLiving;
    }
}

class spawnedObject
{
    private Transform transform;
    public GameObject obj;
    public float lifeTime;

    public spawnedObject(GameObject obj, Transform transform)
    {
        this.transform = transform;
        this.obj = obj;
        lifeTime = 0;
    }

    public void enable()
    {
        obj.SetActive(true);
    }

    public void disable()
    {
        obj.transform.position = transform.position;
        obj.SetActive(false);
        lifeTime = 0;
    }

    public void update()
    {
        if (obj.activeSelf)
            lifeTime += Time.deltaTime;
        else
            lifeTime = 0;
    }

    public bool getEnabled()
    {
        return obj.activeSelf;
    }
}

