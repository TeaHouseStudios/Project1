using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 1f;
    [Header("Unity")]
    Rigidbody2D rb;
    FiniteStateMachine fsm;

    [Header("FOV")]
    public float viewDistance;
    public float fovAngle;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    GameObject character1;
    GameObject character2;
    bool canSeeCharacter1 = false;
    bool canSeeCharacter2 = false;
    


    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        fsm = new FiniteStateMachine();

        character1 = GameObject.FindGameObjectWithTag("Character1");
        character2 = GameObject.FindGameObjectWithTag("Character2");

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
            CheckForPlayer();
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

    private void CheckForPlayer()
    {
        Vector2 toCharacter1 = character1.transform.position - transform.position;
        Vector2 toCharacter2 = character2.transform.position - transform.position;

        // Check if player is within FOV angle
        if (Vector2.Angle(transform.right, toCharacter1) < fovAngle * 0.5f)
        {
            if (toCharacter1.magnitude < viewDistance)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, toCharacter1.normalized, viewDistance, playerLayer | obstacleLayer);
                // Check if player is the first thing hit
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Character1Body"))
                {
                    Debug.Log("Character 1 Detected!");
                }
            }
        }
        if (Vector2.Angle(transform.right, toCharacter2) < fovAngle * 0.5f)
        {
            if (toCharacter2.magnitude < viewDistance)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, toCharacter2.normalized, viewDistance, playerLayer | obstacleLayer);
                // Check if player is the first thing hit
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Character2Body"))
                {
                    Debug.Log("Character 2 Detected!");
                }
            }
        }
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

    private void OnDrawGizmos()
    {
        // Draw the FOV as a debug gizmo
        Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, fovAngle * 0.5f) * transform.right * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -fovAngle * 0.5f) * transform.right * viewDistance);
    }
}
