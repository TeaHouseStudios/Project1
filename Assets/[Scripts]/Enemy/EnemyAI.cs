using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 1f;
    [Header("Unity")]
    Rigidbody2D rb;
    FiniteStateMachine fsm;
    public GameObject enemyGun;

    [Header("FOV")]
    public float viewDistance;
    public float fovAngle;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    GameObject character1;
    GameObject character2;
    bool hasSeenCharacter = false;
    bool canSeeCharacter1 = false;
    bool canSeeCharacter2 = false;
    Vector2 fovDirection;
    bool changeToEngagerunning = false;

    [Header("TARGETING")]
    public GameObject targetChar;
    public float sightTimer = 0f;
    public float minTimeBeforeEngage = 2f;
    public float returnToPatrolTimer = 0f;
    public float maxTimeWithoutSight = 5f;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        character1 = GameObject.FindGameObjectWithTag("Character1");
        character2 = GameObject.FindGameObjectWithTag("Character2");
    }

    private void Start()
    {
        enemyGun = this.gameObject.transform.GetChild(0).gameObject;

        fsm = new FiniteStateMachine();

        var patrolingState = fsm.CreateState("Patroling");
        var investigatingState = fsm.CreateState("Investigating");
        var engagingState = fsm.CreateState("Engaging");
        var baitedState = fsm.CreateState("Baited");
        var sleepingState = fsm.CreateState("Sleeping");

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
            if (hasSeenCharacter)
            {
                fsm.TransitionTo("Investigating");
            }
        };
        patrolingState.onExit = delegate
        {
            
        };
        investigatingState.onEnter = delegate
        {
            Debug.Log("ENTER INVESTIGATE STATE");
            //START Search TIMER
            

        };
        investigatingState.onFrame = delegate
        {
            CheckForPlayer();
            if (canSeeCharacter1 || canSeeCharacter2)
            {
                sightTimer += Time.deltaTime;
                if (sightTimer > minTimeBeforeEngage)
                {
                    //ATTACK PLAYER SECTION
                    if (canSeeCharacter1 && canSeeCharacter2)
                    {
                        //target which ever character is closer to the enemy
                        float dist1 = Vector3.Distance(character1.transform.position, transform.position);
                        float dist2 = Vector3.Distance(character2.transform.position, transform.position);

                        if (dist1 >= dist2)
                        {
                            enemyGun.GetComponent<Gun>().targetCharacter = character2;
                            //targetChar = character2;
                        }
                        else
                        {
                            enemyGun.GetComponent<Gun>().targetCharacter = character1;
                        }
                    }
                    else if(canSeeCharacter1){
                        enemyGun.GetComponent<Gun>().targetCharacter = character1;
                    }
                    else if (canSeeCharacter2)
                    {
                        enemyGun.GetComponent<Gun>().targetCharacter = character2;
                    }
                }
            }
            if (hasSeenCharacter)
            {
                returnToPatrolTimer += Time.deltaTime;
                if (returnToPatrolTimer > maxTimeWithoutSight)
                {
                    hasSeenCharacter = false;
                    //DIRECT UNBROKEN SIGHTLINE FOR X SECONDS
                    fsm.TransitionTo("Patroling");
                }
            }
            
        };
        investigatingState.onExit = delegate
        {

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

        // Determine the FOV direction based on whether the enemy is facing right or left
        fovDirection = IsFacingRight() ? transform.right : -transform.right;

        // Check if player is within FOV angle for Character 1
        if (Vector2.Angle(fovDirection, toCharacter1) < fovAngle * 0.5f)
        {
            if (toCharacter1.magnitude < viewDistance)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, toCharacter1.normalized, viewDistance, playerLayer | obstacleLayer);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Character1Body"))
                {
                    canSeeCharacter1 = true;
                    hasSeenCharacter = true;
                    returnToPatrolTimer = 0f;
                }
                else
                {
                    if (canSeeCharacter1) // if Character1 was previously in sight and now isn't
                    {
                        sightTimer = 0f;
                        canSeeCharacter1 = false;
                    }
                }
            }
        }
        else if (canSeeCharacter1) // if Character1 was previously in sight and now isn't
        {
            sightTimer = 0f;
            canSeeCharacter1 = false;
        }

        // Check if player is within FOV angle for Character 2
        if (Vector2.Angle(fovDirection, toCharacter2) < fovAngle * 0.5f)
        {
            if (toCharacter2.magnitude < viewDistance)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, toCharacter2.normalized, viewDistance, playerLayer | obstacleLayer);
                // Check if player is the first thing hit
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Character2Body"))
                {
                    canSeeCharacter2 = true;
                    hasSeenCharacter = true;
                    returnToPatrolTimer = 0f;
                }
                else
                {
                    if (canSeeCharacter2) // if Character2 was previously in sight and now isn't
                    {
                        sightTimer = 0f;
                        canSeeCharacter2 = false;
                    }
                }
            }
        }
        else if (canSeeCharacter2) // if Character2 was previously in sight and now isn't
        {
            sightTimer = 0f;
            canSeeCharacter2 = false;
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
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, fovAngle * 0.5f) * fovDirection * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -fovAngle * 0.5f) * fovDirection * viewDistance);
    }
}
