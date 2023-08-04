using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CharacterMovement : MonoBehaviour
{
    public enum CharacterState
    {
        isRunning,
        isJumping,
        isFalling,
        isGrounded,
        isDead
    }

    [Header("Unity")]
    public CharacterState currentState;
    public Rigidbody2D rb;
    public Animator animator;
    public CapsuleCollider2D playerBody;
    public CapsuleCollider2D playerFeet;

    [Header("Movement")]
    [SerializeField] private float speed = 5;
    bool facingRight = true;


    [Header("Jump")]
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float fallMultiplier = 2f, holdJumpMultiplier = 3.5f, maxFallSpeed = 30;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = CharacterState.isGrounded;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Animation();
    }

    private void FixedUpdate()
    {
        
    }

    private void ChangeState(CharacterState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    private void Animation()
    {

    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        //CHECK FOR FLIPPING CHARACTER
        if (currentState != CharacterState.isDead)
        {
            if (horizontal > 0 && !facingRight)
            {
                Flip();
            }

            if (horizontal < 0 && facingRight)
            {
                Flip();
            }
        }

        //MOVEMENT AND JUMPING
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        //Changes gravity so you fall faster (makes it feel less floaty when jumping)
        if (rb.velocity.y < -0.5)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            ChangeState(CharacterState.isFalling);
        }
        //if jump is not held more gravity is applied causing a shorter jump (tap jump for less height, hold for more height)
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (holdJumpMultiplier - 1) * Time.deltaTime;
        }

        //jump
        if (currentState == CharacterState.isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        //speed cap
        if (rb.velocity.y < -maxFallSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, -maxFallSpeed);
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpVelocity;
        ChangeState(CharacterState.isJumping);
    }

    private void Flip()
    {
        Vector3 currentScale = this.gameObject.transform.localScale;
        currentScale.x *= -1;
        this.gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == playerFeet)
        {
            //LANDING
            ChangeState(CharacterState.isGrounded);
        }
    }
}
