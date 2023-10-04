using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C1_AimAndThrow : MonoBehaviour
{
    public Vector2 direction;
    public float launchForce;
    public GameObject tpPrefab;

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector2 throwPos = transform.position;
        direction = mousePos - throwPos;

        FaceMouse();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void FaceMouse()
    {
        transform.right = direction;
    }

    void Shoot()
    {
        GameObject tpIns = Instantiate(tpPrefab, transform.position, transform.rotation);

        tpIns.GetComponent<Rigidbody2D>().AddForce(transform.right * launchForce);
    }
}
