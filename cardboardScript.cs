using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardboardScript : MonoBehaviour {

    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "reflDownSensor")
        {
            //Debug.Log("on trigger enter cardboard");
            ExpLibrary.reflectionDownValue = 36;
        }
    }
    
    

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "robot")
        {
            //Debug.Log("Robot fell off " + setup.getIterationTime(setup.iterationStartTime));
            col.gameObject.transform.Translate(0, -42f, 0);
            setup.setRobotFellOff(true);
            setup.fallOffTime = setup.getIterationTime(setup.iterationStartTime);
        }
        if (col.gameObject.name == "reflDownSensor")
        {
            //Debug.Log("on floor");
            ExpLibrary.reflectionDownValue = 16;

        }
    }

    


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
