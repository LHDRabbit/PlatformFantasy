using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_2 : MonoBehaviour
{
    private Camera cameraFollow_2;
    [SerializeField] private Rigidbody2D playerRigidBody;

    private Vector2 velocity;
    private float inputAxis;

    [SerializeField] private float playerSpeed = 6f;
    [SerializeField] private float maxJumpHeight = 6f;
    [SerializeField] private float maxJumptTime = 1.5f;
    [SerializeField] private float jumpForce => (2f * maxJumpHeight) / (maxJumptTime / 2f); // => allow peorperty means computing with other properties, instead of storing value
    [SerializeField] private float gravity => (2f * maxJumpHeight) / Mathf.Pow((maxJumptTime / 2f), 2);
    public bool isGrounded {  get; private set; } // Adding {get; private set;} means only this class is responsible for this variable, though other classes can read.
    public bool isJumping {  get; private set; } // Adding {get; private set;} means only this class is responsible for this variable, though other classes can read.



    // - Coding 1 - //
    public SpriteRenderer playerRenderer;
    public LayerMask groundLayer;
    public Transform groundPosition;
    public float groundCheckCircle;
    public float jumpTime = 0.5f;
    public float jumpTimecounter;
    private bool isJumpting;

    // used at CollectibleManager;
    public static int gemCount = 0;
    public static int cherryCount = 0;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();// GetComponent is a function that Unity provides to search <> the same game object that our script is attached to 
        cameraFollow_2 = Camera.main;
    }

    void Update()
    {
        HorizontalMovement();
        GroundChecker();
        CharactorFlipper();
    }

    private void HorizontalMovement() // - Move charactor left and right 
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * playerSpeed, playerSpeed * Time.deltaTime); // this fundctions as acceleration and deceleration enabled
    }

    private void GroundChecker()
    {
        isGrounded = Physics2D.OverlapCircle(groundPosition.position, groundCheckCircle, groundLayer);

        if (Input.GetButton("Jump") && isGrounded == true)
        {
            isJumpting = true;
            jumpTimecounter = jumpTime;
            playerRigidBody.velocity = Vector2.up * jumpForce;
            //player.AddForce(player.transform.position * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButton("Jump") && isJumpting == true)
        {
            if (jumpTimecounter > 0)
            {
                playerRigidBody.velocity = Vector2.up * jumpForce;
                jumpTimecounter -= Time.deltaTime;
            }
            else
            {
                isJumpting = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumpting = false;
        }
    }

    private void CharactorFlipper() // Both coding version - Flip the face of charactor 
    {
        if (inputAxis < 0)
        {
            playerRenderer.flipX = true;
        }
        else if (inputAxis > 0)
        {
            playerRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            gemCount++;
            Debug.Log("{CollectobleManager.gemCount}");
        }

        if (other.gameObject.CompareTag("Cherry"))
        {
            Destroy(other.gameObject);
            cherryCount++;
            Debug.Log("{CollectobleManager.cherryCount}");
        }
    }

    private void FixedUpdate() // coding version 2 - 
    {   
        Vector2 position = playerRigidBody.position;
        position += velocity * Time.fixedDeltaTime;

        playerRigidBody.MovePosition(position);

        // - coding version 1 - // player cannot move back to the past screen.
        Vector2 leftedge = cameraFollow_2.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightedge = cameraFollow_2.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftedge.x, rightedge.x);


    }
}

