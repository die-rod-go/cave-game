using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //  attributes that affect how the player moves
    public float speed;
    public float airSpeed;
    public float maxAirSpeed;
    public float jumpForce;
    public float horizLedgeJumpForce;
    public float vertLedgeJumpForce;
    public float fallMultiplier;    

    //  variables describing the players state
    private bool canMoveHoriz;
    //public float accelerationModifier;
    private bool grounded;
    private bool touchingWall;
    bool grabbingLedge;
    bool restingOnLedge;    //  whether the hand has actually touched the ledge
    public bool facingLeft;

    //  jump shit
    private float jumpTimer;
    public float jumpRefreshTime;
    bool jumpHeld;

    //  input
    private float horizInput;
    private float horizInputRaw;
    private float vertInput;
    private float vertInputRaw;

    //  outside resources
    public CheckHand handScript;
    public Rigidbody2D rb;
    public GameObject Sensors;
    public Animator playerAnimator;

    //  Start is called before the first frame update
    void Start()
    {
        canMoveHoriz = true;
        touchingWall = false;
        horizInput = 0;
        jumpTimer = 0;
        jumpHeld = false;
        grabbingLedge = false;
        facingLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        //  get input
        vertInput = Input.GetAxis("Vertical");
        vertInputRaw = Input.GetAxisRaw("Vertical");

        horizInput = Input.GetAxis("Horizontal");
        horizInputRaw = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jumpTimer = 0;

        jumpTimer += Time.deltaTime;
        jumpHeld = Input.GetButton("Jump");

        updateFacing();
        updateAnimationVriables();
    }

    private void FixedUpdate()
    {
        moveLeftRight();
        jump();
    }

    //  flip sensors and colliders depending on the direction the player is facing
    private void updateFacing()
    {
        if (rb.velocity.x > 0.1)
        {
            facingLeft = false;
            Sensors.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (rb.velocity.x < -0.1)
        {
            facingLeft = true;
            Sensors.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void jump()
    {
        bool jumpPressed = false;

        if (jumpTimer < jumpRefreshTime)
            jumpPressed = true;

        jumpTimer = 999;

        if (jumpPressed)
        {
            if (!grabbingLedge)
            {
                if (grounded)
                {
                    //  if not grabbing anything and on the ground - jump normally
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, jumpForce));
                }
            }
            else
            {
                //  if pressing down, jumping and grabbing a ledge then drop through ledge
                if (vertInputRaw < 0)
                {
                    if (grabbingLedge)
                    {
                        handScript.setIsTrigger(true);
                        handScript.setHandTouchingWall(true);
                        canMoveHoriz = true;
                    }
                }
                else//  if grabbing ledge and jumping then jump off ledge
                {
                    rb.velocity = new Vector2(horizInput * horizLedgeJumpForce, Mathf.Max(rb.velocity.y, vertLedgeJumpForce));
                }
            }
        }

        //  if not holding jump, fall faster
        if (rb.velocity.y < -0 || !jumpHeld)
            rb.velocity += new Vector2(0, 0 - fallMultiplier);
    }
  
    private void moveLeftRight()
    {
        //  this stuff is still being worked on
        float targetSpeed = horizInput * speed;
        //float acceleration = accelerationModifier * targetSpeed;

        if (canMoveHoriz)
        {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }
    }

    private void updateAnimationVriables()
    {
        playerAnimator.SetFloat("horizVel", rb.velocity.x);
        playerAnimator.SetFloat("verticalVel", rb.velocity.y);

        playerAnimator.SetBool("facingLeft", facingLeft);
        playerAnimator.SetBool("grounded", grounded);
        playerAnimator.SetBool("grabbingLedge", grabbingLedge);
    }

    //  GETTERS AND SETTERS
    public void setGrounded(bool grounded)
    {
        this.grounded = grounded;
    }

    public void setTouchingLeftWall(bool touching)
    {
        touchingWall = touching;
    }
  
    public bool getTouchingLeftWall()
    {
        return touchingWall;
    }

    public void setCanMoveHoriz(bool canMoveHoriz)
    {
        this.canMoveHoriz = canMoveHoriz;
    }

    public void setGrabbingLedge(bool grabbingLeft)
    {
        this.grabbingLedge = grabbingLeft;
        canMoveHoriz = !grabbingLeft;
    }
}
