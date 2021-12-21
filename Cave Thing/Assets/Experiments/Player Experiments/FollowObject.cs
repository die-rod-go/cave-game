using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float xOffset, yOffset;
    // Start is called before the first frame update

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(objectToFollow.position.x + xOffset, objectToFollow.position.y + yOffset, -1.0f);
    }
}
