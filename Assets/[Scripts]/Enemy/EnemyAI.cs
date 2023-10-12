using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 1f;
    [Header("Unity")]
    Rigidbody2D rb;
    FiniteStateMachine fsm;


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        fsm = new FiniteStateMachine();

        var patrolingState = fsm.CreateState("Patroling");
        var investigatingState = fsm.CreateState("Investigating");
        var engagingState = fsm.CreateState("Engaging");
        var baitedState = fsm.CreateState("Baited");

        patrolingState.onEnter = delegate
        {
            Debug.Log("ENTER PATROL STATE");
        };
        patrolingState.onFrame = delegate
        {
            if (IsFacingRight())
            {
                rb.velocity = new Vector2(moveSpeed, 0f);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, 0f);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        fsm.Update();
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    void Flip()
    {
        Vector3 currentScale = this.gameObject.transform.localScale;
        currentScale.x *= -1;
        this.gameObject.transform.localScale = currentScale;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Flip();
        }
    }
}
