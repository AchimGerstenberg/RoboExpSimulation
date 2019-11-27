using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class robot : MonoBehaviour
{
    float timeUntouch = 0f;
    public static int touchCounter = 0;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (timeUntouch + 1f < Time.time)      // counts new touch only if last "untouch" was more than one second ago --> debounce
            setup.cubeTouches++;            

        if (col.gameObject.name == "redBox")
        {
            setup.writeLogEvent(4);
        }
        if (col.gameObject.name == "greenBox")
        {
            setup.writeLogEvent(5);
        }
        if (col.gameObject.name == "blueBox")
        {            
            setup.writeLogEvent(6);
        }
        ExpLibrary.pushingCube = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {        
        if (col.gameObject.name == "redBox")
        {
            setup.writeLogEvent(4);
        }
        if (col.gameObject.name == "greenBox")
        {
            setup.writeLogEvent(5);
        }
        if (col.gameObject.name == "blueBox")
        {
            setup.writeLogEvent(6);
        }
        ExpLibrary.pushingCube = false;
        timeUntouch = Time.time;
    }

    private void Start()
    {
        
    }

    private void Update()
    {

    }
}