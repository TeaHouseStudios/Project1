using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSleepDart : MonoBehaviour
{
    public GameObject dartPrefab;
    public float shootForce = 10.0f;
    int directionToShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
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
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }

}
