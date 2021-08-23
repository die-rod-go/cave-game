using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSprites : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AnimationScale;
    public PlayerMovement player;
    public float maxStretch;

    public float stretchAmount; //  how fast it reacts to stretching
    private float stretchVelocity; // current Speed of Stretch

    void Start()
    {
        maxStretch = 0;

        stretchAmount = 0;
        stretchVelocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateScale();
    }

    private void updateScale()
    {
        float currentYScale = AnimationScale.transform.localScale.y;
        Vector3 localScale = new Vector3();

        updateVelocity(currentYScale);

        localScale.y += stretchVelocity;
        localScale.x = updateScaleX();
        localScale.z = AnimationScale.transform.localScale.z;

        AnimationScale.transform.localScale = localScale;
    }

    private void updateVelocity(float currentYScale)
    {
        float desiredScale = getStretchAmount();
        if (currentYScale > desiredScale)
            stretchVelocity -= stretchAmount * Time.deltaTime;
        else if (currentYScale < desiredScale)
            stretchVelocity += stretchAmount * Time.deltaTime;

        Debug.Log("Vel: " + stretchVelocity);
        Debug.Log("Scl: " + desiredScale);
    }

    private float getStretchAmount()
    {
        float speed = player.rb.velocity.y;
        speed = Mathf.Abs(speed);
        speed = Mathf.Min(speed, maxStretch);
        speed+=1f;
        return speed;
    }

    private float updateScaleX()
    {
        float currentX;

        currentX = 1 / AnimationScale.transform.localScale.y;

        return currentX;
    }
}

