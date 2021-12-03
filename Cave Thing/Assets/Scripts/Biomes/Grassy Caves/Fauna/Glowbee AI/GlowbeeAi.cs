using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowbeeAi : MonoBehaviour
{
    public float minLifeSpan, maxLifeSpan = 0;
    private float lifeSpan;
    private float timeAlive = 0;

    private FlyRandom random;
    private FlyToLocation location;

    private void Awake()
    {
        random = GetComponent<FlyRandom>();
        location = GetComponent<FlyToLocation>();

        timeAlive = 0;
        random.enabled = true;
        location.enabled = false;
        lifeSpan = Random.Range(minLifeSpan, maxLifeSpan);
    }

    private void OnEnable()
    {
        timeAlive = 0;
        random.enabled = true;
        location.enabled = false;
        lifeSpan = Random.Range(minLifeSpan, maxLifeSpan);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        updateMode();
        checkIfDespawn();
    }

    private void updateMode()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= lifeSpan)
        {
            location.enabled = true;
            random.enabled = false;
        }
    }

    private void checkIfDespawn()
    {
        if (location.enabled)
            if (location.checkIfArrived())
                transform.gameObject.SetActive(false);
    }
}
