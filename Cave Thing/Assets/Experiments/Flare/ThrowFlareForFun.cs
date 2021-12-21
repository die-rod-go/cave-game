using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFlareForFun : MonoBehaviour
{
    //THIS IS JSUT FOR TESTING DO NOT ACTUALLY USE
    [SerializeField] GameObject flare;
    [SerializeField] float throwStength;
    [SerializeField] float xOffset, yOffset;
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            throwFlare();
    }

    void throwFlare()
    {
        Vector2 offset = new Vector2(xOffset, yOffset);
        Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float strength = direction.magnitude / 3;

        GameObject thrownFlare = Instantiate(flare, (Vector2)transform.position + offset, Quaternion.identity);
        thrownFlare.GetComponent<Rigidbody2D>().velocity = direction * strength;
    }
}
