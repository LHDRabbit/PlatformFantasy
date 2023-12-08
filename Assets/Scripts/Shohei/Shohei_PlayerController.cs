using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shohei_PlayerController: MonoBehaviour
{
    [SerializeField] private float jumpForce = 4f;
    [SerializeField] private float speed = 3f;

    [SerializeField] private Rigidbody2D player;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        player.velocity = new Vector2(horizontalInput * speed, player.velocity.y);

        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            player.AddForce(player.transform.position * jumpForce, ForceMode2D.Impulse);
        }
    }
}
