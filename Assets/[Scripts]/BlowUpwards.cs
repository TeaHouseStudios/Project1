using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUpwards : MonoBehaviour
{

    List<GameObject> blowingObjects = new List<GameObject>();

    public float fanMagnitude = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            AddBlowingForce();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.transform.parent)
        {
            blowingObjects.Add(collision.gameObject.transform.parent.gameObject);
        }
        else
        {
            blowingObjects.Add(collision.gameObject);
        }


        Debug.Log("FAN");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent)
        {
            blowingObjects.Remove(collision.gameObject.transform.parent.gameObject);
        }
        else
        {
            blowingObjects.Remove(collision.gameObject);
        }
    }

    private void AddBlowingForce()
    {

        foreach (GameObject entityToBlow in blowingObjects)
        {
            if (entityToBlow.GetComponent<Rigidbody2D>() != null)
            entityToBlow.GetComponent<Rigidbody2D>().AddForce(Vector2.up * fanMagnitude, ForceMode2D.Impulse);
        }

    }

    public void LetFanAffectObject(GameObject toBlow)
    {
        blowingObjects.Add(toBlow);
    }
}
