using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeftHand : MonoBehaviour
{
    public bool handTouchingWall;
    public PlayerMovement playerMovement;
    public Collider2D hand;

    // Start is called before the first frame update
    void Start()
    {
        handTouchingWall = false;
        hand.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.getTouchingLeftWall() && !handTouchingWall)
        {
            hand.isTrigger = false;
            playerMovement.setGrabbingLeft(true);
        }
        else if(!hand.isTrigger)
        {
            hand.isTrigger = true;
            playerMovement.setGrabbingLeft(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        handTouchingWall = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        handTouchingWall = false;
    }

    public void setIsTrigger(bool isTrigger)
    {
        hand.isTrigger = isTrigger;
    }

    public void setHandTouchingWall(bool touching)
    {
        handTouchingWall = touching;
    }
}
