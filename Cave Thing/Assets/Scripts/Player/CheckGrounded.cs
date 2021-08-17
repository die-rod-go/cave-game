using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour
{

    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.setGrounded(true);
        playerMovement.setGrabbingLeft(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement.setGrounded(false);
    }
}
