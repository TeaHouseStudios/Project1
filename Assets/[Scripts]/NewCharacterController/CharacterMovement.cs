using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.TextCore.Text;

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
    public AudioSource footstepsSound;
    public bool hasEnteredDoor = false;
    public bool isDead = false;
    public GameObject activeIndicator;

    [Header("Movement")]
    [SerializeField] private float speed = 5;
    public float maxSpeed = 10f;
    bool facingRight = true;
    public bool isOnPlatform;
    public Rigidbody2D platformRB;
    public float horizontal;
    public float airControlMultiplier = 0.2f;


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
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentState = CharacterState.isGrounded;
        vecGravity = new Vector2 (0, -Physics2D.gravity.y);
    }

    private void OnEnable()
    {
        Events.onSwitch.AddListener(OnSwitch);
    }

    private void OnDisable()
    {
        Events.onSwitch.RemoveListener(OnSwitch);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterIndex != GameManager.Instance.currentCharacter)
        {
            horizontal = 0;
        }
        Movement();
        Animation();
    }

    void OnSwitch(int activeChar)
    {
        if (activeChar == characterIndex)
        {
            activeIndicator.gameObject.SetActive(true);
        }
        else
        {
            activeIndicator.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        float targetSpeed = horizontal * speed;
        animator.SetFloat("Speed", Mathf.Abs(targetSpeed));

        if (isOnPlatform)
        {
            if (currentState == CharacterState.isGrounded)
            {
                rb.velocity = new Vector2(targetSpeed + platformRB.velocity.x, rb.velocity.y);
            }
            else
            {
                // Using a lerp for a smoother transition while in air
                float velocityX = Mathf.Lerp(rb.velocity.x, targetSpeed + platformRB.velocity.x, 0.1f);
                rb.velocity = new Vector2(velocityX, rb.velocity.y);
            }
        }
        else
        {
            if (currentState == CharacterState.isGrounded)
            {
                rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
            }
            else
            {
                // Using a lerp for a smoother transition while in air
                float velocityX = Mathf.Lerp(rb.velocity.x, targetSpeed, 0.1f);
                rb.velocity = new Vector2(velocityX, rb.velocity.y);
            }
        }

        // Clamp the velocity to a maximum value
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
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
            horizontal = Input.GetAxisRaw("Horizontal");

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
            //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            //jump
            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f)
            {
                Jump();
            }

            // Allow for a bit of air control
            if (rb.velocity.y != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x + horizontal * airControlMultiplier, rb.velocity.y);
            }

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }

            //FOOTSTEP AUDIO
            if (horizontal != 0 && currentState == CharacterState.isGrounded)
            {
                footstepsSound.enabled = true;
            }
            else
            {
                footstepsSound.enabled = false;
            }
        }
        if (rb.velocity.y == 0 && currentState != CharacterState.isGrounded)
        {
            currentState = CharacterState.isGrounded;
        }

        //CHECK FOR JUMPING FOR ANIMATION
        if (rb.velocity.y > 0.01 && currentState != CharacterState.isGrounded)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }


    }

    void Jump()
    {
        SoundManager.Instance.Play("jump");
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

    public void KillCharacter()
    {
        if (!isDead)
        {
            isDead = true;
            this.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            if (characterIndex == 1)
            {
                GameManager.Instance.character1Deaths++;
            }
            else
            {
                GameManager.Instance.character2Deaths++;
            }
            GameManager.Instance.RespawnCharacter(characterIndex);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == playerFeet)
        {
            //LANDING
            ChangeState(CharacterState.isGrounded);
        }

        if (collision.gameObject.CompareTag("Hazard"))
        {

            KillCharacter();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && collision.otherCollider == playerFeet && currentState != CharacterState.isJumping)
        {
            
            ChangeState(CharacterState.isFalling);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door" && !hasEnteredDoor)
        {
            Door door = collision.gameObject.GetComponent<Door>();
            if (door.doorIndex == characterIndex)
            {
                hasEnteredDoor = true;
                door.Entered = true;
                Debug.Log("DOOR");
                gameObject.SetActive(false);
                GameManager.Instance.SwitchCharacter();
                GameManager.Instance.numCharactersBeatenLevel++;
                
            }
        }
    }


}
