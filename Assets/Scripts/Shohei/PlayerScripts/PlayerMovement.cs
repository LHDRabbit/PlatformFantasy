using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D playerRb;

    public static Vector2 velocityStatic;
    private float inputAxis;
    private float input;

    public float speed = 5f;
    private float maxJumpHeight = 6f;
    private float maxJumptTime = 1f;
    [SerializeField] private float jumpForce = 8f;// => (2f * maxJumpHeight) / (maxJumptTime / 2f); // => allow peorperty means computing with other properties, instead of storing value
    //[SerializeField] private float gravity => (2f * maxJumpHeight) / Mathf.Pow((maxJumptTime / 2f), 2);
    //public bool isGrounded { get; private set; } // Adding {get; private set;} means only this class is responsible for this variable, though other classes can read.
    //public bool isJumping { get; private set; }

    //Reference of scroll never go back to the past scene 
    private Camera cameraFollow_2;
    
    // (W) Reference of sprite direction change 
    public SpriteRenderer spriteRenderer;

    // (X) Reference of counting collected Coin and Cherry in CollectibleManager;
    public static int gemCount = 0;
    public static int cherryCount = 0;

    // reference from old script - start
    public LayerMask groundLayer;
    public bool isOnGround;
    public Transform groundPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.5f;
    public float jumpTimecounter;
    public bool isJumpting; // (Q2. SpriteRendere)

    public bool isIdling;  // (Q1. SpriteRendere)
    public bool isRunning; // (Q3. SpriteRendere)
    public bool isClimbing; // (Q4. SpriteRendere)

    // reference from old script - end


    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        cameraFollow_2 = Camera.main;
        isIdling = true;
        isRunning = false;
        isClimbing = false;
    }

    void Update()
    {
        HorizontalMovement();
        SpriteDirection();
        JumpMovement();
    }

    private void HorizontalMovement()
    {
        //playerRb.velocity = new Vector2(inputAxis * speed, playerRb.velocity.y);
        input = Input.GetAxisRaw("Horizontal");
        //velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * speed, speed * Time.deltaTime);
        playerRb.velocity = new Vector2(input * speed, playerRb.velocity.y);

        // Sprite Animation - Start -
        isIdling = false;
        isRunning = true;
        isClimbing = false;
        // Sprite Animation - End -
        velocityStatic = playerRb.velocity;

        if (input == 0)
        {
            // Sprite Animation - Start -
            isIdling = true;
            isRunning = false;
            isClimbing = false;
            // Sprite Animation - End -
        }
    }

    // (W) A method that changes a facing charactor direction
    private void SpriteDirection()
    {
        if (input < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (input > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void JumpMovement()
    {
        //if (Input.GetButtonDown("Jump"))
        //{
            //playerRb.velocity = Vector2.up * jumpForce;
        //}

        isOnGround = Physics2D.OverlapCircle(groundPosition.position, groundCheckCircle, groundLayer);

        if (isOnGround == true && Input.GetButtonDown("Jump"))
        {
            // Sprite Animation - Start -
            isJumpting = true;
            isIdling = false;
            isRunning = false;
            isClimbing = false;
            // Sprite Animation - End -

            jumpTimecounter = jumpTime;
            playerRb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButtonDown("Jump") && isJumpting == true)
        {
            if (jumpTimecounter > 0)
            {
                playerRb.velocity = Vector2.up * jumpForce;
                jumpTimecounter -= Time.deltaTime;
            }
            {
                isJumpting = false;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumpting = false;
        }
    }

    // (X) A method that counts collected Coin and Cherry in CollectibleManager.
    private void OnCollisionEnter2D(Collision2D other) // (X)-a 
    {
        if (other.gameObject.CompareTag("Gem")) // (X)-1 
        {
            Destroy(other.gameObject);
            gemCount++;
            Debug.Log("{CollectobleManager.gemCount}");
        }

        if (other.gameObject.CompareTag("Cherry")) // (X)-2 
        {
            Destroy(other.gameObject);
            cherryCount++;
            Debug.Log("{CollectobleManager.cherryCount}");
        }

        
    }

    private void FixedUpdate() // player cannot move back to the past screen.
    {
        Vector2 position = playerRb.position;
        //position += velocity * Time.fixedDeltaTime;

        Vector2 leftedge = cameraFollow_2.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightedge = cameraFollow_2.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftedge.x, rightedge.x);

        //playerRb.MovePosition(position);
    }
}
