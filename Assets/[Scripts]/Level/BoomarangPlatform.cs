using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BoomarangPlatform : MonoBehaviour
{
    public int platIndex = 1;
    public Transform startPos;
    public Transform endPos;

    Vector3 targetPos;
    

    public bool isActive = false;
    public bool atEnd = false;
    public bool atStart = true;

    public float speed = 2.0f;

    Character1 character1;
    Character2 character2;

    Rigidbody2D rb;
    Vector3 moveDirection;
    
    private void Awake()
    {
        character1 = GameObject.FindGameObjectWithTag("Character1").GetComponent<Character1>();
        character2 = GameObject.FindGameObjectWithTag("Character2").GetComponent<Character2>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }
    private void OnEnable()
    {
        Events.onReceiverActivated.AddListener(Activated);
        Events.onReceiverDeactivated.AddListener(Deactivated);
        Events.onPcActivated.AddListener(Activated);
        Events.onPcDeactivated.AddListener(Deactivated);
    }
    private void OnDisable()
    {
        Events.onReceiverActivated.RemoveListener(Activated);
        Events.onReceiverDeactivated.RemoveListener(Deactivated);
        Events.onPcActivated.RemoveListener(Activated);
        Events.onPcDeactivated.RemoveListener(Deactivated);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }
    private void FixedUpdate()
    {
        if (!atEnd && isActive)
        {
            targetPos = endPos.position;
            DirectionCalculate();
            MoveForward();
        }
        else if (!isActive && !atStart)
        {
            targetPos = startPos.position;
            DirectionCalculate();
            MoveBackward();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        
    }
    void DirectionCalculate()
    {
        moveDirection = (targetPos - transform.position).normalized;
    }
    public void MoveForward()
    {
        atStart = false;
        if (Vector2.Distance(transform.position, endPos.position) < 0.1f)
        {
            atEnd = true;
        }
        rb.velocity = moveDirection * speed;
        //transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void MoveBackward()
    {
        atEnd = false;
        if (Vector2.Distance(transform.position, startPos.position) < 0.1f)
        {
            atStart = true;
        }
        rb.velocity = moveDirection * speed;
        //transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Character1"))
        {
            character1.isOnPlatform = true;
            character1.platformRB = rb;
        }
        if (collision.CompareTag("Character2"))
        {
            character2.isOnPlatform = true;
            character2.platformRB = rb;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Character1"))
        {
            character1.isOnPlatform = false;
        }
        if (collision.CompareTag("Character2"))
        {
            character2.isOnPlatform = false;
        }
    }



    public void Activated(int index)
    {
        if (platIndex == index)
        {
            //currentWaypointIndex = currentWaypointIndex + 1;
            atStart = false;
            
            isActive = true;
            
        }
    }

    public void Deactivated(int index)
    {
        if (platIndex == index)
        {
            //currentWaypointIndex = currentWaypointIndex - 1;
            
            isActive = false;
            atEnd = false;
        }
    }
}
