using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement: MonoBehaviour
{
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float playerSpeed = 4.5f;
    [SerializeField] private Rigidbody2D playerRigidBody;

    private Camera cameraFollow_2; // coding version 2
    private Vector2 velocity; // coding version 2
    private float inputAxis;
    public SpriteRenderer playerRenderer;
    // public float inputPlayer; // coding version 1

    public LayerMask groundLayer;
    private bool isOnGround;
    public Transform groundPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.5f;
    public float jumpTimecounter;
    private bool isJumpting;

    //public GemManager gemManager;
    public static int gemCount = 0;
    public static int cherryCount = 0;

    void Start()
    {
        // GetComponent is a function that Unity provides to search <> the same game object that our script is attached to 
        playerRigidBody = GetComponent<Rigidbody2D>();
        cameraFollow_2 = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        CharactorFlipper();
        GroundChecker();
        HorizontalMovement();
    }

    private void HorizontalMovement() // - Move charactor left and right 
    {   /// - coding version 1 -
        //inputAxis = Input.GetAxis("Horizontal");
        //playerRigidBody.velocity = new Vector2(inputAxis * playerSpeed, playerRigidBody.velocity.y);

        /// - coding version 2 -
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * playerSpeed, playerSpeed * Time.deltaTime);
    }

    private void GroundChecker()
    {
        isOnGround = Physics2D.OverlapCircle(groundPosition.position, groundCheckCircle, groundLayer);

        if (Input.GetButtonDown("Jump") && isOnGround == true)
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
        //if (collision.gameObject.CompareTag("Enemy"))
        //{
            //Destroy(player);
        //}

        //if (collision.gameObject.CompareTag("Ground"))
        //{
           // bool groundCheck = true;
        //}

        if (other.gameObject.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            gemCount = gemCount + 1;
            Debug.Log("{gemManager.gemCount}");
        }

        if (other.gameObject.CompareTag("Cherry"))
        {
            Destroy(other.gameObject);
            cherryCount++;
        }
    }

    private void FixedUpdate() // coding version 2 - 
    {   // player cannot move back to the past screen.
        Vector2 position = playerRigidBody.position;
        position += velocity * Time.deltaTime;

        Vector2 leftedge = cameraFollow_2.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightedge = cameraFollow_2.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftedge.x, rightedge.x);

        playerRigidBody.MovePosition(position);
    }
}
