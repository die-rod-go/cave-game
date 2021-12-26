using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowLightTemp : MonoBehaviour
{
    //THIS IS JSUT FOR TESTING DO NOT ACTUALLY USE
    [SerializeField] GameObject flare;
    [SerializeField] GameObject glowStick;
    [SerializeField] float throwStength;
    [SerializeField] float xOffset, yOffset;
    [SerializeField] Camera cam;
    [SerializeField] float falloff;

    private Vector2 direction;
    private float strength;
    private Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        strength = direction.magnitude / falloff;
        offset = new Vector2(xOffset, yOffset);

        if (Input.GetMouseButtonDown(0))
            throwFlare();
        if (Input.GetMouseButtonDown(1))
            throwGlowStick();

        Debug.DrawRay((Vector2)transform.position + offset, direction * strength, Color.white);
    }

    void throwFlare()
    {
        GameObject thrownFlare = Instantiate(flare, (Vector2)transform.position + offset, Quaternion.identity);
        thrownFlare.GetComponent<Rigidbody2D>().velocity = direction * strength;
    }

    void throwGlowStick()
    {
        GameObject thrownFlare = Instantiate(glowStick, (Vector2)transform.position + offset, Quaternion.identity);
        thrownFlare.GetComponent<Rigidbody2D>().velocity = direction * strength;
    }
}
