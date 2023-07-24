using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC : MonoBehaviour
{
    public int pcIndex = 1;
    public bool isActive = false;
    public float pulseTimer = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        Debug.Log("PC ACTIVATED");
        Events.onPcActivated.Invoke(pcIndex);
        StartCoroutine(PcTimer());
    }

    public IEnumerator PcTimer()
    {
        yield return new WaitForSeconds(pulseTimer);
        Deactivate();
    }

    public void Deactivate()
    {
        Events.onPcDeactivated.Invoke(pcIndex);
    }
}
