using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTimer : MonoBehaviour
{
    [SerializeField] private float burnTime;
    [SerializeField] private float lifeTime;
    private float timeAlive;

    ChangeLightState changeState;

    // Start is called before the first frame update
    void OnEnable()
    {
        timeAlive = 0;
        if (changeState == null)
            changeState = GetComponentInChildren<ChangeLightState>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive > burnTime)
            fizzleLight();
        if (timeAlive > lifeTime)
            Destroy(transform.gameObject);
    }

    void fizzleLight()
    {
        changeState.EnableAnimator();
        changeState.PlayFizzleAnimation();
    }
}
