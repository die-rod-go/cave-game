using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckGrounded : MonoBehaviour
{
    public string[] makeGrounded;
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
        if (makeGrounded.Contains(collision.tag))
        {
            playerMovement.setGrounded(true);
            playerMovement.setGrabbingLedge(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (makeGrounded.Contains(collision.tag))
            playerMovement.setGrounded(false);
    }
}
