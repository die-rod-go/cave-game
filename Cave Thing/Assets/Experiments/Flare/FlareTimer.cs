using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareTimer : MonoBehaviour
{
    [SerializeField] private float burnTime;
    [SerializeField] private float lifeTime;
    private float timeAlive;

    ChangeFlareState changeState;

    // Start is called before the first frame update
    void OnEnable()
    {
        timeAlive = 0;
        if (changeState == null)
            changeState = GetComponentInChildren<ChangeFlareState>();
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        if (timeAlive > burnTime)
            fizzleFlare();
        if (timeAlive > lifeTime)
            Destroy(transform.gameObject);
    }

    void fizzleFlare()
    {
        changeState.EnableAnimator();
        changeState.PlayFizzle();
    }
}
