using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class Async : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Timer timer = new Timer(TimeOut, null, 5000, 0);
    }

    private void TimeOut(object state)
    {
        Debug.Log("5 second");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
