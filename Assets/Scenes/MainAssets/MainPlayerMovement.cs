using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

/// <summary>
/// the PlayerMovement script needs some cleanup and some tweaks to work with the animator component, which seems to be a cleaner, easier way to make animations work.
/// Lesleigh has rigged up a bunch of animations into the animator, and is currently attempting to tie all of them together.
/// </summary>
public class MainPlayerMovement : MonoBehaviour
{
    private float horizontal;
    //playerSpeed refers to the speed the player moves, speed refers to the component in the animator to determine movement


    public Animator animator;

    public float speed;
    public float jumpForce;


    public Rigidbody2D playerRB;
    public float input;
    public SpriteRenderer playerSpriteRenderer;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    private bool isJumping;





    void Update()
    {
        //Horizontal movement and flipping
        input = Input.GetAxisRaw("Horizontal");
        if (input < 0)
        {
            playerSpriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            playerSpriteRenderer.flipX = false;
        }

        //Groundchecking
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);

        if (isGrounded == true && Input.GetButton("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRB.velocity = Vector2.up * jumpForce;

            animator.SetBool("IsJumping", false);
            animator.SetBool("IsGrounded", true);
        }
        if (isGrounded == true)
        {
            Debug.Log("The player is grounded!");
        }


        //Jump Stopper, isJumping
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                playerRB.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;

                animator.SetBool("IsJumping", true);

            }
            else
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }

        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }


    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(input * speed, playerRB.velocity.y);
    }




    //void Update()
    //{
    //    HorMovement();
    //    GroundCheck();
    //    Jumping();
    //}

    //private void HorMovement()
    //{

    //    //Horizontal movement and flipping
    //    input = Input.GetAxisRaw("Horizontal");




    //}

    //public void Jumping()
    //{

    //    if (isGrounded == true && Input.GetButton("Jump"))
    //    {
    //        isJumping = true;
    //        jumpTimeCounter = jumpTime;
    //        playerRB.velocity = Vector2.up * jumpForce;

    //        animator.SetBool("IsJumping", false);
    //        animator.SetBool("IsGrounded", true);
    //    }

    //    //Jump Stopper, isJumping
    //    if (Input.GetButton("Jump") && isJumping)
    //    {
    //        if (jumpTimeCounter > 0)
    //        {
    //            playerRB.velocity = Vector2.up * jumpForce;
    //            jumpTimeCounter -= Time.deltaTime;

    //            animator.SetBool("IsJumping", true);

    //        }
    //        else
    //        {
    //            isJumping = false;
    //            animator.SetBool("IsJumping", false);
    //        }

    //    }

    //    if (Input.GetButtonUp("Jump"))
    //    {
    //        isJumping = false;
    //        animator.SetBool("IsJumping", false);
    //    }
    //}

    //public void GroundCheck()
    //{
    //    //Groundchecking
    //    isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);


    //}

    //private void FixedUpdate()
    //{
    //    playerRB.velocity = new Vector2(input * speed, playerRB.velocity.y);
    //}







}