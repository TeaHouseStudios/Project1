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
    public BoxCollider2D playerFeet;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
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

    }
}
