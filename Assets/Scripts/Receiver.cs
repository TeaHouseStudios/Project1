using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    public int recIndex = 1;
    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Box>() != null)
        {
            if (collision.GetComponent<Box>().boxIndex == recIndex && isActive == false)
            {
                isActive = true;
                Activate();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Box>() != null)
        {
            if (collision.GetComponent<Box>().boxIndex == recIndex && isActive == true)
            {
                isActive = false;
                Deactivate();
            }
        }
        
    }

    public void Activate()
    {
        Events.onReceiverActivated.Invoke(recIndex);
    }

    public void Deactivate()
    {
        Events.onReceiverDeactivated.Invoke(recIndex);
    }
}
