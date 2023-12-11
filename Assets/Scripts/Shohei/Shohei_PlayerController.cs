using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shohei_PlayerController: MonoBehaviour
{
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private Rigidbody2D player;

    public SpriteRenderer playerRenderer;
    public float inputPlayer;

    public LayerMask groundLayer;
    private bool isOnGround;
    public Transform groundPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.5f;
    public float jumpTimecounter;
    private bool isJumpting;


    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move charactor left and right
        float horizontalInput = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(horizontalInput * playerSpeed, player.velocity.y);


        // Flip the face of charactor 
        inputPlayer = Input.GetAxisRaw("Horizontal");
        if (inputPlayer < 0)
        {
            playerRenderer.flipX = true;
        } else if (inputPlayer > 0)
        {
            playerRenderer.flipX = false;
        }

        isOnGround = Physics2D.OverlapCircle(groundPosition.position, groundCheckCircle, groundLayer);

        if (Input.GetButtonDown("Jump") && isOnGround ==true)
        {
            isJumpting = true;
            jumpTimecounter = jumpTime;
            player.velocity = Vector2.up * jumpForce;
            //player.AddForce(player.transform.position * jumpForce, ForceMode2D.Impulse);
        }         

        if (Input.GetButton("Jump") && isJumpting == true)
        {
            if(jumpTimecounter > 0) 
            {
                player.velocity = Vector2.up * jumpForce;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(player);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            bool groundCheck = true;
        }
    }
}
