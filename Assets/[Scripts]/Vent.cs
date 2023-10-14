//BROKEN! DONT TOUCH!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{

    public int ventIndex;
    public int exitVentIndex;

    public void OnEnable()
    {
        Events.onVentEnter.AddListener(teleportToNext);
    }

    public void OnDisable()
    {
        Events.onVentEnter.RemoveListener(teleportToNext);
    }

    public void teleportToNext(GameObject player, int nextVentIndex)
    {
        Debug.Log("Peepeepoopoo");

        if(nextVentIndex == ventIndex)
        {
            //tp to exit vent index

            // the player is going to this.gameObject coords

            player.transform.position = this.gameObject.transform.position;

            player.GetComponent<PlayerVent>().charInVentRange = true;
        }
    }

}
