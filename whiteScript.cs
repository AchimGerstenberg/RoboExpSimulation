using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class whiteScript : MonoBehaviour {

    // METHODS
    // Use this for initialization
    void Start () {
		// setup configuration here?
	}
	
	// Update is called once per frame
	void Update () {
       
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "reflDownSensor")
        {
            //Debug.Log("on trigger enter white");
            ExpLibrary.reflectionDownValue = 47;

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "redBox")
        {
            //Debug.Log("Bye bye Red" + setup.getIterationTime(setup.iterationStartTime));
            col.gameObject.transform.Translate(42f, 0, 0);
            setup.setRedOut(true);
            setup.setTimeRed(setup.getIterationTime(setup.iterationStartTime));
            setup.writeLogEvent(7);
            if (ExpLibrary.colorflag == ExpLibrary.ColorFlag.Red)
                ExpLibrary.colorBonus += 10;
            else if(ExpLibrary.colorflag != ExpLibrary.ColorFlag.Nothing)
                ExpLibrary.colorPenalty += 10;
        }
        if (col.gameObject.name == "greenBox")
        {
            //Debug.Log("Bye bye Green" + setup.getIterationTime(setup.iterationStartTime));
            col.gameObject.transform.Translate(0, 42f, 0);
            setup.setGreenOut(true);
            setup.setTimeGreen(setup.getIterationTime(setup.iterationStartTime));
            setup.writeLogEvent(8);
            if (ExpLibrary.colorflag == ExpLibrary.ColorFlag.Green)
                ExpLibrary.colorBonus += 10;
            else if (ExpLibrary.colorflag != ExpLibrary.ColorFlag.Nothing)
                ExpLibrary.colorPenalty += 10;
        }
        if (col.gameObject.name == "blueBox")
        {
            //Debug.Log("Bye bye Blue" + setup.getIterationTime(setup.iterationStartTime));
            col.gameObject.transform.Translate(-42f, 0, 0);
            setup.setBlueOut(true);
            setup.setTimeBlue(setup.getIterationTime(setup.iterationStartTime));
            setup.writeLogEvent(9);
            if (ExpLibrary.colorflag == ExpLibrary.ColorFlag.Blue)
                ExpLibrary.colorBonus += 10;
            else if (ExpLibrary.colorflag != ExpLibrary.ColorFlag.Nothing)
                ExpLibrary.colorPenalty += 10;
        }
        if (col.gameObject.name == "reflDownSensor")
        {
            //Debug.Log("on triggerExit white");
            ExpLibrary.reflectionDownValue = 36;
            setup.writeLogEvent(2);
            setup.cardboardTouches++;
        }
    }

    

}
