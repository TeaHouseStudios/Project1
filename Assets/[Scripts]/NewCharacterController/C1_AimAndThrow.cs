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
                    }
                }

                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    ClearPointer();
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
    }

    Vector2 PointPosition(float t)
    {
        Vector2 currentPosition = (Vector2)transform.position + (direction.normalized * launchForce * t)
            + 0.5f * Physics2D.gravity * (t * t);

        return currentPosition;
    }
}
