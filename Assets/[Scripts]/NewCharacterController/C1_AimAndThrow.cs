using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1_AimAndThrow : MonoBehaviour
{
    public Vector2 direction;
    public float launchForce;
    public GameObject tpPrefab;

    public bool tpIsThrown = false;

    public GameObject pointPrefab;
    public GameObject[] points;

    public int numOfPoints;

    public GameObject thrownTeleporter = null;

    private void Start()
    {
        points = new GameObject[numOfPoints];
        for ( int i = 0; i < numOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (gameObject.GetComponentInParent<CharacterMovement>().characterIndex == GameManager.Instance.currentCharacter)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 throwPos = transform.position;
            direction = mousePos - throwPos;

            FaceMouse();

            if (tpIsThrown == false)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    for (int i = 0; i < points.Length; i++)
                    {
                        points[i].gameObject.SetActive(true);
                        points[i].transform.position = PointPosition(i * 0.1f);
                    }

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Shoot();
                        ClearPointer();
                    }
                }

                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    ClearPointer();
                }
            }

            else if (tpIsThrown == true)
            {
                if (Input.GetKeyDown(KeyCode.F) && thrownTeleporter != null)
                {
                    TeleportCharacter();
                }
            }
        }
        

        
    }

    void ClearPointer()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].gameObject.SetActive(false);
        }
    }

    void FaceMouse()
    {
        transform.right = direction;
    }

    void Shoot()
    {
        GameObject tpIns = Instantiate(tpPrefab, transform.position, transform.rotation);

        tpIns.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;

        //Events.onTeleportThrown.Invoke(tpIns);
        tpIsThrown = true;
        thrownTeleporter = tpIns;
    }

    void TeleportCharacter()
    {
        GameObject character = gameObject.transform.parent.gameObject;

        if (character != null && thrownTeleporter != null)
        {
            // Calculate the vertical offset from the character's center to its feet
            float characterHalfHeight = character.GetComponent<SpriteRenderer>().bounds.extents.y;

            // Calculate the new position
            Vector3 beaconPosition = thrownTeleporter.transform.position;
            Vector3 teleportPosition = new Vector3(beaconPosition.x, beaconPosition.y + characterHalfHeight, beaconPosition.z);

            // Teleport the character
            character.transform.position = teleportPosition;

            // Cleanup
            Destroy(thrownTeleporter);
            thrownTeleporter = null;
            tpIsThrown = false;
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 currentPosition = (Vector2)transform.position + (direction.normalized * launchForce * t)
            + 0.5f * Physics2D.gravity * (t * t);

        return currentPosition;
    }
}
