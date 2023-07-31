using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    private GameObject character;
    public float moveSpeed = 8f;
    public float jumpHeight = 16f;

    private float horizontal;

    bool facingRight = true;

    public bool isOnPlatform;
    public Rigidbody2D platformRB;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public Transform interactDetect;
    public float rayDistance = 2;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        character = this.gameObject;
    }

    private void OnEnable()
    {
        Events.onSwitch.AddListener(ResetMovement);
    }
    private void OnDisable()
    {
        Events.onSwitch.RemoveListener(ResetMovement);
    }

    void ResetMovement(int newCharacter)
    {
        if (newCharacter == 2)
        {
            horizontal = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentCharacter == 1)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            InteractRaycast();

            if (horizontal > 0 && !facingRight)
            {
                Flip();
            }

            if (horizontal < 0 && facingRight)
            {
                Flip();
            }

            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
               
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                
            }
        }
        //CHECK FOR JUMPING FOR ANIMATION
        if (rb.velocity.y > 0.01)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

    }

    public void InteractRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(interactDetect.position, Vector2.right * transform.localScale, rayDistance);
        GameObject pc;
        if (hit.collider != null && hit.collider.tag == "Interactable")
        {
            pc = hit.collider.gameObject;
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.currentCharacter == 1)
            {
                //HANDLE PC INPUT
                //Debug.Log("PC ACTIVATED");
                pc.GetComponent<PC>().Activate();
            }

        }
    }

    private void FixedUpdate()
    {
        float targetSpeed = horizontal * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(targetSpeed));

        if (isOnPlatform)
        {
            rb.velocity = new Vector2(targetSpeed + platformRB.velocity.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }


    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
    }

    private void Flip()
    {
        Vector3 currentScale = this.gameObject.transform.localScale;
        currentScale.x *= -1;
        this.gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            Door door = collision.gameObject.GetComponent<Door>();
            if (door.correctCharacter == character)
            {
                door.Entered = true;
                Debug.Log("DOOR");
                character.SetActive(false);
                GameManager.Instance.SwitchCharacter();
                GameManager.Instance.numCharactersBeatenLevel++;
            }
        }
    }
}
