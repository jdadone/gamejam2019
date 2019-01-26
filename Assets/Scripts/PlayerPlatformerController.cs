using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    public float climbingSpeed = 0.01f;

    private bool isNearWall;
    private bool isClimbing;

    private Vector2 defaultGravity;

    // private SpriteRenderer spriteRenderer;
    // private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        defaultGravity = Physics2D.gravity;
        // spriteRenderer = GetComponent<SpriteRenderer> (); 
        // animator = GetComponent<Animator> ();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        if (isNearWall && !isClimbing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isClimbing = true;
                Physics2D.gravity = Vector2.zero;
                velocity.y = 0;
            }
        } else if (isClimbing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isClimbing = false;
                Physics2D.gravity = defaultGravity;
                targetVelocity.x = -5f * maxSpeed;
                velocity.y = 5f;
            }
        } else
        {
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }
        }

        /*
        if(move.x > 0.01f)
        {
            if(spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
            }
        } 
        else if (move.x < -0.01f)
        {
            if(spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
            }
        }

        animator.SetBool ("grounded", grounded);
        animator.SetFloat ("velocityX", Mathf.Abs (velocity.x) / maxSpeed);
        */

        if (isClimbing)
        {
            targetVelocity = Vector2.zero;
            velocity.y = Input.GetAxis("Vertical");
        } else
        {
            targetVelocity = move * maxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isNearWall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isNearWall = false;

            isClimbing = false;
            Physics2D.gravity = defaultGravity;
        }
    }
}