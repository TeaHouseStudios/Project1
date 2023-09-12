using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int boxIndex = 1;
    public bool canBeDropped = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canBeDropped = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBeDropped = true;
    }
}
