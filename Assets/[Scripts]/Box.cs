using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterMovement;

public class Box : MonoBehaviour
{
    public int boxIndex = 1;
    public bool canBeDropped = true;
    public Rigidbody2D rb;

    public bool isOnPlatform;
    public Rigidbody2D platformRB;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.parent != null)
        {
            rb.velocity = Vector2.zero;
        }
        
    }

    private void FixedUpdate()
    {
        if (isOnPlatform)
        {
            
                rb.velocity = new Vector2(platformRB.velocity.x, rb.velocity.y);
            
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BlowUpwards>() == null)
        {
            canBeDropped = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBeDropped = true;
    }
}
