﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject {

    public float maxSpeed = 7;
    public float climbSpeed = 2;
    public float jumpTakeOffSpeed = 7;
    public float climbingSpeed = 0.01f;

    private bool isNearWall;
    private bool isClimbing;
    private bool isHover;
    private bool isNearBreakableWall;
    private bool isDead;

    private Vector2 defaultGravity;

    private State state;

    private Animator animator;

    private PlayerCollisionSide backCollision;

    private PlayerCollisionSide frontCollision;

    private GameObject sprite;
    private Quaternion rotation;
    private float rotationStart;

    public AudioClip[] soundList;

    public GameObject musicObject;
    private AudioSource musicSource;

    public GameObject walkSoundObject;
    private AudioSource walkAudioSource;
    private bool isWalking;

    public GameObject jumpSoundObject;
    private AudioSource jumpAudioSource;

    public GameObject fallSoundObject;
    private AudioSource fallAudioSource;
    
    private GameObject rockets;

    // private SpriteRenderer spriteRenderer;
    // private Animator animator;

    // Use this for initialization
    void Awake () 
    {
        defaultGravity = Physics2D.gravity;
        state = FindObjectOfType<State>();
        animator = GetComponent<Animator>();
        sprite = transform.Find("Sprite").gameObject;
        rockets = transform.Find("Rockets").gameObject;

        backCollision = transform.Find("BackCollider").GetComponent<PlayerCollisionSide>();
        frontCollision = transform.Find("FrontCollider").GetComponent<PlayerCollisionSide>();

        musicSource = musicObject.transform.GetComponent<AudioSource>();
        walkAudioSource = walkSoundObject.transform.GetComponent<AudioSource>();
        jumpAudioSource = jumpSoundObject.transform.GetComponent<AudioSource>();
        fallAudioSource = fallSoundObject.transform.GetComponent<AudioSource>();

        // spriteRenderer = GetComponent<SpriteRenderer> (); 
        // animator = GetComponent<Animator> ();
        PlayMusic();
    }

    protected override void PlayFallSound()
    {
    	fallAudioSource.clip = soundList[3];
		fallAudioSource.Play();
    }

    protected override void ComputeVelocity()
    {
        if (isDead) return;

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis ("Horizontal");

        animator.SetBool("isIddle", velocity == Vector2.zero);

        rockets.SetActive(isHover);

	    if(velocity.y == 0.0f && isHover)
        {
        	isHover = false;
			gravityModifier = 1.0f;
        }

        if(!isHover && !isNearWall && state.HasChip(ChipType.HOVER))
        {
        	if(velocity.y < 0)
        	{
        		if(Input.GetButtonDown("Jump"))
        		{
        			isHover = true;
        			gravityModifier = 0.0f;
        			velocity.y = -1.0f;
        		}
        	}
        } else if (isHover)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isHover = false;
                gravityModifier = 1.0f;
            }
        }

        if (isNearWall && !isClimbing && state.HasChip(ChipType.CLIMB) && !isHover)
        {
            if (Input.GetButtonDown("Jump") && (frontCollision.isHittingWall || backCollision.isHittingWall))
            {
                sprite.transform.localRotation = Quaternion.Euler(0, 0, frontCollision.isHittingWall ? 90 : -90);
                isClimbing = true;
                Physics2D.gravity = Vector2.zero;
                velocity.y = 0;
            }
        } else if (isClimbing)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isClimbing = false;
                sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
                Physics2D.gravity = defaultGravity;
                targetVelocity.x = -5f * maxSpeed;
                velocity.y = 5f;
            }
        } else if (!isHover && state.HasChip(ChipType.JUMP) && !isNearBreakableWall)
        {
            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y = jumpTakeOffSpeed;
                PlayJumpSound();
                // animator.SetBool("jump", true);
            }
            else if (Input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * 0.5f;
                }
            }
        }

        // Debug.Log(Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("walkingSpeed", isClimbing ? Mathf.Abs(velocity.y) : Mathf.Abs(velocity.x) / maxSpeed);

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

        if(velocity.y == 0 && Input.GetAxis("Horizontal") !=0 && !isClimbing)
    	{
    		PlayWalkSound();
    	}
    	else if(Input.GetAxis("Horizontal") == 0 || velocity.y != 0 || isClimbing)
    	{
    		StopWalkSound();
    	}

        if (isClimbing)
        {
            targetVelocity = Vector2.zero;
            velocity.y = Input.GetAxis("Vertical") * climbSpeed;
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
        } else if (collision.gameObject.tag == "Deadly")
        {
            var allChips = FindObjectsOfType<ChipController>();
            StartCoroutine(Die());
        }
    }

    IEnumerator Die ()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1);
        isDead = false;
        animator.SetBool("isDead", false);
        transform.position = state.LastChip;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isNearWall = false;
            isClimbing = false;
            sprite.transform.localRotation = Quaternion.Euler(0, 0, 0);
            Physics2D.gravity = defaultGravity;
        }

        if (collision.gameObject.tag == "BreakableWall")
        {
            isNearBreakableWall = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="BreakableWall")
        {

            isNearBreakableWall = true;
            if (Input.GetButtonDown("Jump") && state.HasChip(ChipType.FIRE))
            {
                collision.gameObject.GetComponent<BreakableWallController>().BreakWall();
                isNearBreakableWall = false;
            }
            
        }
    }

	private void PlayMusic()
    {
    	musicSource.clip = soundList[0];
		musicSource.Play();
    }

    private void StopMusic()
    {
		musicSource.Stop();
    }

    private void PlayWalkSound()
    {
    	if(!isWalking)
    	{
    		isWalking = true;
    		walkAudioSource.clip = soundList[1];
			walkAudioSource.Play();
    	}
    }

    private void StopWalkSound()
    {
    	isWalking = false;
		walkAudioSource.Stop();
    }

	private void PlayJumpSound()
    {
    	jumpAudioSource.clip = soundList[2];
		jumpAudioSource.Play();
    }
}
