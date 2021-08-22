using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

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
    private Animator myAnimator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
    }

    //myAnimator.SetBool("falling", true);
    //myAnimator.SetBool("falling", false);

    //myAnimator.SetTrigger("jump");
    //myAnimator.ResetTrigger("jump);


    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, rad0Circle, whatIsGround);

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            myAnimator.ResetTrigger("jump");
            myAnimator.SetBool("falling", false);
        }


        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            stoppedJumping = false;
            myAnimator.SetTrigger("jump");
        }

        if (Input.GetButton("Jump") && !stoppedJumping && (jumpTimeCounter > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
            myAnimator.SetTrigger("jump");
        }

        if (Input.GetButtonUp("Jump"))
        {

            jumpTimeCounter = 0;
            stoppedJumping = true;
            myAnimator.SetBool("falling", true);
            myAnimator.ResetTrigger("jump");
        }

        if (rb.velocity.y < 0)
        {

            myAnimator.SetBool("falling", true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, rad0Circle);
    }

    private void FixedUpdate()
    {
        HandleLayers();
    }
    private void HandleLayers()
    {
        if (!grounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
