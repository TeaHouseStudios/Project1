using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{

    GameObject char2;

    float newSpeed;
    float maxSpeed;
    float jumpSpeed;

    public bool isBoosting = false;

    public float speedIncreaseFactor = 4f;
    public float jumpIncreaseFactor = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        char2 = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Order()); // pause 3 seconds
        }
    }

    void updateSpeed()
    {

        newSpeed = char2.GetComponent<CharacterMovement>().speed;
        maxSpeed = char2.GetComponent<CharacterMovement>().maxSpeed;
        jumpSpeed = char2.GetComponent<CharacterMovement>().jumpVelocity;

        Debug.Log("Current speed = " + newSpeed);

        if (isBoosting)
        {
            newSpeed *= speedIncreaseFactor;
            maxSpeed *= speedIncreaseFactor;
            jumpSpeed *= jumpIncreaseFactor;
        }
        else
        {
            newSpeed /= speedIncreaseFactor; // existing walk speed
            maxSpeed /= speedIncreaseFactor;
            jumpSpeed /= jumpIncreaseFactor;
        }

        char2.GetComponent<CharacterMovement>().speed = newSpeed;
        char2.GetComponent<CharacterMovement>().maxSpeed = maxSpeed;
        char2.GetComponent<CharacterMovement>().jumpVelocity = jumpSpeed;

        Debug.Log("Current speed = " + newSpeed);
    }

    IEnumerator Order()
    {
        isBoosting = true; // toggle speedboost

        updateSpeed();

        yield return new WaitForSeconds(3.0f); // 3 second break

        isBoosting = false;

        updateSpeed();

    }
}
