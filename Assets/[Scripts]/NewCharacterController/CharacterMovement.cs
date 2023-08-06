using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CharacterMovement : MonoBehaviour
{
    public enum CharacterState
    {
        isJumping,
        isFalling,
        isGrounded,
        isDead
    }

    [Header("Unity")]
    public int characterIndex;
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
    [SerializeField] private float fallMultiplier = 2f;
    [SerializeField] private float coyoteTime = 0.15f;
    [SerializeField] private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.15f;
    [SerializeField] private float jumpBufferCounter;
    Vector2 vecGravity;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentState = CharacterState.isGrounded;
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);
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
        int horizontal = Mathf.Abs((int)Input.GetAxisRaw("Horizontal"));
        // set animation speed variable to INT horizontal***
    }

    private void Movement()
    {
        if (characterIndex == GameManager.Instance.currentCharacter)
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

            if (currentState == CharacterState.isGrounded)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }
            if (Input.GetButtonDown("Jump"))
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            //MOVEMENT AND JUMPING
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            //jump
            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
            {
                Jump();
            }

            if (rb.velocity.y < 0)
            {
                
                rb.velocity -= vecGravity * fallMultiplier * Time.deltaTime;
            }
        }
        
        
    }

    void Jump()
    {
        
        jumpBufferCounter = 0f;
        coyoteTimeCounter = 0f;
        rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == playerFeet && currentState != CharacterState.isJumping)
        {
            
            ChangeState(CharacterState.isFalling);
        }
    }


}
