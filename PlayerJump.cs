using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
  
    [Header("Jump Details")]
    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;

    [Header("Ground Details")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float rad0Circle;
    public bool grounded;

    [Header("Components")]
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpTimeCounter = jumpTime;
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position,rad0Circle,whatIsGround);

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
        }

        if(Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {

            jumpTimeCounter = 0;
            stoppedJumping = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, rad0Circle);
    }
}
