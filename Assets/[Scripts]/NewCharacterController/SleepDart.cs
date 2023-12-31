using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepDart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyAI>().fsm.TransitionTo("Sleeping");
        }

        Destroy(this.gameObject);
    }
}
