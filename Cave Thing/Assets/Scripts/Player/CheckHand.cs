using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHand : MonoBehaviour
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
        updateHand();
    }
    
    private void updateHand()// if the wall sensor is touching a wall and the "hand" is not touching a wall then turn the "hand" solid    private void updateHand()
    {
        if (playerMovement.getTouchingLeftWall() && !handTouchingWall)
        {
            hand.isTrigger = false;
            playerMovement.setGrabbingLedge(true);
        }
        else if (!hand.isTrigger)
        {
            hand.isTrigger = true;
            playerMovement.setGrabbingLedge(false);
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
