using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject targetCharacter = null;

    public GameObject bulletPrefab;
    public float shootForce;

    GameObject target;
    float azimuth = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Fire();
        }
    }

    public void Fire()
    {
        Vector2 bulletSpawnPosition = new Vector2(transform.position.x + 1, transform.position.y);

        GameObject bulletIns = Instantiate(bulletPrefab, bulletSpawnPosition, transform.rotation);

        bulletIns.GetComponent<Rigidbody2D>().velocity = transform.right * shootForce;
    }


    public void Aim(float azimuthToAim)
    {
        azimuth = azimuthToAim;
    }


    public void AimAtCharacter()
    {
        // calculate angle to targetCharacter, send angle to Aim()
    }
}
