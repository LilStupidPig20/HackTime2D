using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerControl : MonoBehaviour
       
{
    Animator anim;
    Rigidbody2D rb;

    private bool isGrounded = false;
    public Transform groundCheck;  
    private float groundRadius = 0.9f;
    public LayerMask whatIsGround;

    private float moveInput;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        
        if (Input.GetAxis("Horizontal") == 0)
        {
            anim.SetInteger("animation_code", 1);
        } else
        {
            anim.SetInteger("animation_code", 2);
        }
        
    }

    void jump()
    {
        rb.AddForce(transform.up * 20f, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround); 
        if (!isGrounded)
            return;

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * 12f, rb.velocity.y);

        if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        else if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
