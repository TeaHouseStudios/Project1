using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropelUpwards : MonoBehaviour
{

    Rigidbody2D fan;
    public float magnitude = 1;

    // Start is called before the first frame update
    void Update()
    {
        fan = GetComponent<Rigidbody2D>();
        fan.AddForce(Vector2.up * magnitude, ForceMode2D.Impulse);
    }

}
