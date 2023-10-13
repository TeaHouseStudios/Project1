using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSleepDart : MonoBehaviour
{
    public GameObject dartPrefab;
    public float shootForce = 10.0f;
    int directionToShoot;
    public bool readyToFire = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && readyToFire)
        {

            if (IsFacingRight())
            {
                directionToShoot = 1;
            }
            else
            {
                directionToShoot = -1;
            }

            Vector2 dartSpawnPosition = new Vector2(transform.position.x + 1 * directionToShoot, transform.position.y);

            GameObject dartIns = Instantiate(dartPrefab, dartSpawnPosition, transform.rotation);

            dartIns.GetComponent<Rigidbody2D>().velocity = transform.right * directionToShoot * shootForce;

            StartCoroutine(CooldownDart()); // 10 second cooldown until you can next fire a dart
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

    IEnumerator CooldownDart ()
    {
        readyToFire = false;

        yield return new WaitForSeconds(10.0f); // 10 second break

        readyToFire = true;
    }

}
