using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject targetCharacter = null;

    public GameObject bulletPrefab;
    public float shootForce;

    private void Update()
    {
        if(targetCharacter != null)
        {
            AimAtCharacter();
        }

    }

    public void Fire()
    {
        GameObject bulletIns = Instantiate(bulletPrefab, transform.position, transform.rotation);

        Rigidbody2D bulletRigidBody = bulletIns.GetComponent<Rigidbody2D>();

        Vector2 shootingVector = (targetCharacter.transform.position - transform.position).normalized;

        bulletRigidBody.velocity = shootingVector * shootForce;
    }


    public void AimAtCharacter()
    {
        Vector3 directionToPlayer = targetCharacter.transform.position - transform.position;

        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
