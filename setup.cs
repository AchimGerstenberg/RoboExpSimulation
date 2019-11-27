/* this script includes:
 * - the experiment library in its own class
 * - code classes that inherit the ExpLibrary class. Those code classes shall include the code the participants have written
 * - the setup class which is important for:
 *      - running the codes with the different starting configurations
 *      - writing to the config file to document configuration settings
 *      - documenting the results of executing the code in different starting configs
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ExpLibrary : MonoBehaviour
{   
    // public Exp Lib variables    
    public static int reflectionDownValue = 47;
    public static int speedLeft;
    public static int speedRight;
    public static float driveSpeed = 0.0293f;
    public static float rotationSpeed = 1.0f;
    public enum ColorFlag {Red, Green, Blue, Nothing};
    public static ColorFlag colorflag;
    public static int colorBonus = 0;
    public static int colorPenalty = 0;
    public static int blink;
    public static bool turnflag = false;
    public static long waitCoroutineArg;
    public static int turnCoroutineArg1 = 0;
    public static int turnCoroutineArg2 = 0;
    public static bool pushingCube = false;



    // private Exp Lib variables


    private float timer1;
    private float timer2;
    private float timer3;



    // WAIT     WAIT        WAIT        WAIT        WAIT
    public IEnumerator waitCoroutine(long ms)
    {
        waitCoroutineArg = ms;
        long waitIterations = (long)(ms / Time.deltaTime);
        waitIterations /= 1000;
        for(int i = 0; i < waitIterations; i++)
        {
            yield return new WaitForFixedUpdate();
        }
    }

    
    // BLINK        BLINK       BLINK
    /*
    public IEnumerator blinkCoroutine()
    {
        blink = blinkclass.GetBlinkValue();
        yield return new WaitForFixedUpdate();
        StartCoroutine(blinkCoroutine());
    }
    */

    // Red Reflection
    public int reflectionRedLeft()
    {
        Transform rayStart = GameObject.Find("colorsensor_left").transform;

        RaycastHit2D[] crosshit = new RaycastHit2D[21];

        float[] reflRed = new float[21];
        //initialize with 0:
        for (int i = 0; i <= 20; i++)
            reflRed[i] = 0f;

        for (int i = 0; i <= 20; i++)
        {
            Debug.DrawLine(rayStart.position, rayStart.position + 10 * rayStart.up + (0.5f * (i - 10) * rayStart.right), Color.red);
            crosshit[i] = Physics2D.Linecast(rayStart.position, rayStart.position + 10 * rayStart.up + (0.5f * (i - 10) * rayStart.right), 1 << LayerMask.NameToLayer("boxlayer"));

            if (crosshit[i].collider != null)
            {
                Debug.DrawLine(rayStart.position, rayStart.position + 10 * rayStart.up + (0.5f * (i - 10) * rayStart.right), Color.green);
                float distance = crosshit[i].distance * 9.49223f + 5;      // to adjust from simulated scale to real life centimeters and add 5 centimeters because the real life calibration was made to the box center
                if (crosshit[i].collider.name == "RedCross")                // if the ray hits a red cube use this fit
                    reflRed[i] = -0.0114f * Mathf.Pow(distance, 3f) + 0.8174f * Mathf.Pow(distance, 2f) - 19.525f * distance + 158.94f;
                else // else fit with the curve for hitting the blue or green cube
                    reflRed[i] = -0.1572f * (distance - 5) + 5.5442f;

                if (reflRed[i] < 0)
                    reflRed[i] = 0;
            }
            else
            {
                Debug.DrawLine(rayStart.position, rayStart.position + 10 * rayStart.up + (0.5f * (i - 10) * rayStart.right), Color.red);
                reflRed[i] = 0;
            }
        }

        float reflRedfloat = 0f;
        for (int i = 0; i <= 20; i++)
        {
            reflRedfloat = reflRedfloat + reflRed[i] / 20;
        }

        return (int)(reflRedfloat + 0.5f);
    }

    public int reflectionRedRight()
    {
        Transform rayStart = GameObject.Find("colorsensor_right").transform;

        RaycastHit2D[] crosshit = new RaycastHit2D[21];

        float[] reflRed = new float[21];
        //initialize with 0:
        for(int i = 0; i <= 20; i++)
            reflRed[i] = 0f;

        for (int i = 0; i <= 20; i++)
        {
            Debug.DrawLine(rayStart.position, rayStart.position + 10 * rayStart.up + (0.5f * (i - 10) * rayStart.right), Color.red);
            crosshit[i] = Physics2D.Linecast(rayStart.position, rayStart.position + 10 * rayStart.up + (0.5f * (i - 10) * rayStart.right), 1 << LayerMask.NameToLayer("boxlayer"));

            if (crosshit[i].collider != null)
            {
                Debug.DrawLine(rayStart.position, rayStart.position + 10 * rayStart.up + (0.5f * (i - 10) * rayStart.right), Color.green);
                float distance = crosshit[i].distance * 9.49223f + 5;      // to adjust from simulated scale to real life centimeters and add 5 centimeters because the real life calibration was made to the box center
                if (crosshit[i].collider.name == "RedCross")
                    reflRed[i] = -0.0177f * Mathf.Pow(distance, 3f) + 1.244f * Mathf.Pow(distance, 2f) - 28.979f * distance + 229.03f;
                else
                    reflRed[i] = -0.1572f * (distance - 5) + 5.5442f;

                if (reflRed[i] < 0)
                    reflRed[i] = 0;
            }
            else
            {
                Debug.DrawLine(rayStart.position, rayStart.position + 10 * rayStart.up + (0.5f * (i - 10) * rayStart.right), Color.red);
                reflRed[i] = 0;
            }
        }

        float reflRedfloat = 0f;
        for (int i = 0; i <= 20; i++)
        {
            reflRedfloat = reflRedfloat + reflRed[i] / 20;
        }

        return (int)(reflRedfloat + 0.5f);
    }

    public void PlayTone(int freq, int duration)
    {
        switch(freq)
        {
            case 400:
                colorflag = ColorFlag.Red;
                break;
            case 800:
                colorflag = ColorFlag.Green;
                break;
            case 1600:
                colorflag = ColorFlag.Blue;
                break;
        }
    }

    // TURN     TURN        TURN        TURN        TURN        
    public IEnumerator turnCoroutine(int turnspeed, int degrees)
    {
        turnflag = true;
        turnCoroutineArg1 = turnspeed;
        turnCoroutineArg2 = degrees;

        motor(0, 0);
        if (turnspeed > 60)    // max turnspeed is 100
            turnspeed = 60;
        if (turnspeed < 0)      // if turnspeed is negative, turn the other direction
            degrees = -degrees;

        float nextAngle = GameObject.Find("robot").transform.eulerAngles.z;
        float target = ((360 + GameObject.Find("robot").transform.eulerAngles.z - degrees) % 360);
        float difference = Mathf.DeltaAngle(GameObject.Find("robot").transform.eulerAngles.z, target);    // difference between position now and target

        while (Mathf.Abs(difference) >= 1f)
        {
            if (Mathf.Abs(difference) > 4)
            {
                nextAngle += Mathf.Sign(difference) * 2 * turnspeed * rotationSpeed * Time.deltaTime;
                GameObject.Find("robot").transform.eulerAngles = new Vector3(0, 0, nextAngle);
                difference = Mathf.DeltaAngle(GameObject.Find("robot").transform.eulerAngles.z, target);
                yield return new WaitForFixedUpdate();
            }
            else
            {
                nextAngle += Mathf.Sign(difference) * 25 * rotationSpeed * Time.deltaTime;
                GameObject.Find("robot").transform.eulerAngles = new Vector3(0, 0, nextAngle);
                difference = Mathf.DeltaAngle(GameObject.Find("robot").transform.eulerAngles.z, target);
                yield return new WaitForFixedUpdate();
            }
        }
        motor(0, 0);
        turnflag = false;

        //Debug.Log(target);
        //Debug.Log(transform.eulerAngles.z);
    }
    // END OF TURN      END OF TURN     END OF TURN

    // TIMERS       TIMERS      TIMERS      TIMERS
    public void startTimer1()
    {
        timer1 = Time.time;
        //Debug.Log("timer1 started @:" + timer1);
    }
    public int readTimer1()
    {
        //Debug.Log(Time.time - timer1);
        return (int)(1000*(Time.time - timer1));
    }

    public void startTimer2()
    {
        timer2 = Time.time;
    }
    public int readTimer2()
    {
        return (int)(1000 * (Time.time - timer2));
    }

    public void startTimer3()
    {
        timer3 = Time.time;
    }
    public int readTimer3()
    {
        return (int)(1000 * (Time.time - timer3));
    }

    // RANDOM       RANDOM      RANDOM
    public int random(int min, int max)
    {
        if (min > max)
            return 0;

        return Random.Range(min, max);
    }

    //DISPLAY           DISPLAY         DISPLAY
    public static int LCD_LINE1 = 0;    // just dummies
    public static int LCD_LINE2 = 0;
    public static int LCD_LINE3 = 0;
    public static int LCD_LINE4 = 0;
    public static int LCD_LINE5 = 0;
    public static int LCD_LINE6 = 0;
    public static int LCD_LINE7 = 0;
    public static int LCD_LINE8 = 0;
    public void dispNum(int x, int y, int value)
    {
        //Debug.Log(value);
    }
    public void dispText(int x, int y, string value)
    {
        //Debug.Log(value);
    }

    // ULTRASOUND       ULTRASOUND       ULTRASOUND       ULTRASOUND       ULTRASOUND       
    public int ultrasound()
    {
        
        float viewAngle = 25;
        float angleToRed = 0f;
        float distanceToRed = 200f;

        float angleToGreen = 0f;
        float distanceToGreen = 200f;

        float angleToBlue = 0f;
        float distanceToBlue = 200f;

        float ultrasoundValue = 200f;
        int ultraValue;

        if (GameObject.Find("redBox") != null)      // red box ultrasound
        {
            float orientation = Mathf.DeltaAngle(GameObject.Find("redBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
            orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);
            viewAngle = -13f / 45f * orientation + 35;

            Vector3 toRed = (GameObject.Find("redBox").transform.position - GameObject.Find("ultrasound").transform.position) / (GameObject.Find("redBox").transform.position - GameObject.Find("ultrasound").transform.position).magnitude;    // normalized vector from ultrasound location to red cube location
            angleToRed = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, toRed)) * 360 / (2 * Mathf.PI);      // angle between the robot orientation and the vector from the ultrasound to the red box
            distanceToRed = (GameObject.Find("redBox").transform.position - GameObject.Find("ultrasound").transform.position).magnitude;
            // assuming a simple symmetric 35deg cut-off angle (in reality it depends on the cube orientation realtive to the ultrasound sensor)
            if (angleToRed > viewAngle)
                distanceToRed = 200f;
            /*
            Debug.Log("dot product: " + Vector3.Dot(transform.up, toRed));
            Debug.Log("Acos: " + angleToRed);
            Debug.Log("distance: " + distanceToRed);
            */
        }

        if (GameObject.Find("greenBox") != null)      // green box ultrasound
        {
            float orientation = Mathf.DeltaAngle(GameObject.Find("greenBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
            orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);
            viewAngle = -13f / 45f * orientation + 35;

            Vector3 toGreen = (GameObject.Find("greenBox").transform.position - GameObject.Find("ultrasound").transform.position) / (GameObject.Find("greenBox").transform.position - GameObject.Find("ultrasound").transform.position).magnitude;    // normalized vector from ultrasound location to green cube location
            angleToGreen = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, toGreen)) * 360 / (2 * Mathf.PI);      // angle between the robot orientation and the vector from the ultrasound to the green box
            distanceToGreen = (GameObject.Find("greenBox").transform.position - GameObject.Find("ultrasound").transform.position).magnitude;
            // assuming a simple symmetric 35deg cut-off angle (in reality it depends on the cube orientation realtive to the ultrasound sensor)
            if (angleToGreen > viewAngle)
                distanceToGreen = 200f;
            /*
            Debug.Log("dot product: " + Vector3.Dot(transform.up, toGreen));
            Debug.Log("Acos: " + angleToGreen);
            Debug.Log("distance: " + distanceToGreen);
            */
        }

        if (GameObject.Find("blueBox") != null)      // blue box ultrasound
        {
            float orientation = Mathf.DeltaAngle(GameObject.Find("blueBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
            orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);
            viewAngle = -13f / 45f * orientation + 30;

            Vector3 toBlue = (GameObject.Find("blueBox").transform.position - GameObject.Find("ultrasound").transform.position) / (GameObject.Find("blueBox").transform.position - GameObject.Find("ultrasound").transform.position).magnitude;    // normalized vector from ultrasound location to blue cube location
            angleToBlue = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, toBlue)) * 360 / (2 * Mathf.PI);      // angle between the robot orientation and the vector from the ultrasound to the blue box
            distanceToBlue = (GameObject.Find("blueBox").transform.position - GameObject.Find("ultrasound").transform.position).magnitude;
            // assuming a simple symmetric 35deg cut-off angle (in reality it depends on the cube orientation realtive to the ultrasound sensor)
            if (angleToBlue > viewAngle)
                distanceToBlue = 200f;
            /*
            Debug.Log("dot product: " + Vector3.Dot(transform.up, toBlue));
            Debug.Log("Acos: " + angleToBlue);
            Debug.Log("distance: " + distanceToBlue);
            */
        }

        ultrasoundValue = Mathf.Min(distanceToRed, distanceToGreen, distanceToBlue) * 9.49223f;
        ultraValue = (int) (ultrasoundValue / 0.88f + 0.5f);    // divide 0.88 because the simulated ultrasound shows a too small ultrasound value, + 0.5 for rounding
        if (ultraValue > 200)
            ultraValue = 200;
        return ultraValue;
    }

    // MOTOR        MOTOR       MOTOR       MOTOR
    public void motor(int sLeft, int sRight)
    {
        // if speed > 75 or < -75 set to 75 or -75, 75 becuase the motors max out after 75, so 100 is in reality not faster than 75.
        if(Mathf.Abs(sLeft) > 75)
            sLeft = sLeft/Mathf.Abs(sLeft) * 75;
        if (Mathf.Abs(sRight) > 75)
            sRight = sRight / Mathf.Abs(sRight) * 75;
 
        // drive slower while pushing a cube
        if(pushingCube)
        {
            if(sLeft > 35)  // below 35 there seems to be no loss of speed because the PID control seems to catch it
                sLeft = (int)(0.95f * sLeft);
            if(sRight > 35)
                sRight = (int)(0.95f * sRight);
        }

        speedLeft = sLeft;
        speedRight = sRight;

        /*if (speedLeft == speedRight && speedRight > 2)
        {
            speedLeft -= 2;
            speedRight += 2;
        }
           */

    }

    // REFL DOWN        REFL DOWN       REFL DOWN
    public int reflectionDown()
    {
        return reflectionDownValue;
    }


}




public class setup : code40class {

    // HERE IS PARTICIPANT THE CODE EXECUTION!!! 
    IEnumerator codeExe()
    {

        // goto only for testing, otherwise it messes up the labeling:
        goto p0eval0;

        p0eval0:        // dry run because the first result is always inconsistent for an unknown reason.
        /*
        eval = 0;
        code0class.setCode0Info();
        if (!getCodeStillRunning())
        {
            startingConfigCounter = config.numberOfStartingConfigs;         // just one starting position
          
            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {

                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    Debug.Log(iterationCounter);
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code0");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);


                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }       // waiting

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                            yield return StartCoroutine(waitCoroutine(100));
                            StartCoroutine("code0");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 0");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }
        */
        

        p1eval1:
        config.participantID = config.participantID;    // unchanged
        eval = 1;
        code1class.setCode1Info();
        if (setup.codeActive[0] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        // StopCoroutine("code0");
                        StopCoroutine("code1");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }       // waiting because it makes the results repeatable

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode1");
                            StartCoroutine("code1");
                            writeLogEvent(0);
                            setCodeStillRunning(true);
                        }     
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 1");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p1eval2:
        eval++; // 2
        code2class.setCode2Info();
        if (setup.codeActive[1] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code1");
                        StopCoroutine("code2");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode2");
                            StartCoroutine("code2");
                            setCodeStillRunning(true);
                        }

                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 2");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p1eval3:
        eval++; // 3
        code3class.setCode3Info();
        if (setup.codeActive[2] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code2");
                        StopCoroutine("code3");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode3");
                            StartCoroutine("code3");
                            setCodeStillRunning(true);
                        }

                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 3");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p1eval4:
        eval++; // 4
        code4class.setCode4Info();
        if (setup.codeActive[3] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code3");
                        StopCoroutine("code4");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode4");
                            StartCoroutine("code4");
                            setCodeStillRunning(true);
                        }

                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 4");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p1eval5:
        Debug.Log("p1eval5");
        eval++; // 5
        code5class.setCode5Info();
        if (setup.codeActive[4] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code4");
                        StopCoroutine("code5");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode5");
                            StartCoroutine("code5");
                            setCodeStillRunning(true);
                        }

                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 5");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }




        p2eval1:
        config.participantID++;    // next participant
        eval = 1;                  // reset to eval1
        code6class.setCode6Info();
        if (setup.codeActive[5] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code5");
                        StopCoroutine("code6");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();
                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode6");
                            StartCoroutine("code6");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 6");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p2eval2:
        eval++; // 2
        code7class.setCode7Info();
        if (setup.codeActive[6] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code6");
                        StopCoroutine("code7");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode7");
                            StartCoroutine("code7");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 7");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p2eval3:
        eval++; // 3
        code8class.setCode8Info();
        if (setup.codeActive[7] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code7");
                        StopCoroutine("code8");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode8");
                            StartCoroutine("code8");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 8");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p2eval4:
        eval++; // 4
        code9class.setCode9Info();
        if (setup.codeActive[8] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code8");
                        StopCoroutine("code9");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode9");
                            StartCoroutine("code9");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 9");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p2eval5:
        eval++; // 5
        code10class.setCode10Info();
        if (setup.codeActive[9] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code9");
                        StopCoroutine("code10");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode10");
                            StartCoroutine("code10");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 10");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }




        p3eval1:
        config.participantID++;    // next participant
        eval = 1;                  // reset to eval1
        code11class.setCode11Info();
        if (setup.codeActive[10] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code10");
                        StopCoroutine("code11");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode11");
                            StartCoroutine("code11");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 11");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p3eval2:
        eval++; // 2
        code12class.setCode12Info();
        if (setup.codeActive[11] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code11");
                        StopCoroutine("code12");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode12");
                            StartCoroutine("code12");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 12");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p3eval3:
        eval++; // 3
        code13class.setCode13Info();
        if (setup.codeActive[12] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code12");
                        StopCoroutine("code13");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode13");
                            StartCoroutine("code13");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 13");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p3eval4:
        eval++; // 4
        code14class.setCode14Info();
        if (setup.codeActive[13] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code13");
                        StopCoroutine("code14");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode14");
                            StartCoroutine("code14");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 14");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p3eval5:
        eval++; // 5
        code15class.setCode15Info();
        if (setup.codeActive[14] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code14");
                        StopCoroutine("code15");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode15");
                            StartCoroutine("code15");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 15");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }




        p4eval1:
        config.participantID++;    // next participant
        eval = 1;                  // reset to eval1
        code16class.setCode16Info();
        if (setup.codeActive[15] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code15");
                        StopCoroutine("code16");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode16");
                            StartCoroutine("code16");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 16");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p4eval2:
        eval++; // 2
        code17class.setCode17Info();
        if (setup.codeActive[16] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code16");
                        StopCoroutine("code17");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode17");
                            StartCoroutine("code17");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 17");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p4eval3:
        eval++; // 3
        code18class.setCode18Info();
        if (setup.codeActive[17] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code17");
                        StopCoroutine("code18");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode18");
                            StartCoroutine("code18");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 18");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p4eval4:
        eval++; // 4
        code19class.setCode19Info();
        if (setup.codeActive[18] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code18");
                        StopCoroutine("code19");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode19");
                            StartCoroutine("code19");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 19");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p4eval5:
        eval++; // 5
        code20class.setCode20Info();
        if (setup.codeActive[19] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code19");
                        StopCoroutine("code20");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode20");
                            StartCoroutine("code20");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 20");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }


        p5eval1:
        config.participantID++;    // next participant
        eval = 1;
        code21class.setCode21Info();
        if (setup.codeActive[20] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code20");
                        StopCoroutine("code21");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode21");
                            StartCoroutine("code21");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 21");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p5eval2:
        eval++; // 2
        code22class.setCode22Info();
        if (setup.codeActive[21] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code21");
                        StopCoroutine("code22");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode22");
                            StartCoroutine("code22");
                            setCodeStillRunning(true);
                        }

                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 22");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p5eval3:
        eval++; // 3
        code23class.setCode23Info();
        if (setup.codeActive[22] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code22");
                        StopCoroutine("code23");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode23");
                            StartCoroutine("code23");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 23");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p5eval4:
        eval++; // 4
        code24class.setCode24Info();
        if (setup.codeActive[23] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code23");
                        StopCoroutine("code24");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode24");
                            StartCoroutine("code24");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 24");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p5eval5:
        eval++; // 5
        code25class.setCode25Info();
        if (setup.codeActive[24] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code24");
                        StopCoroutine("code25");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode25");
                            StartCoroutine("code25");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 25");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }




        p6eval1:
        config.participantID++;    // next participant
        eval = 1;                  // reset to eval1
        code26class.setCode26Info();
        if (setup.codeActive[25] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code25");
                        StopCoroutine("code26");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode26");
                            StartCoroutine("code26");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 26");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p6eval2:
        eval++; // 2
        code27class.setCode27Info();
        if (setup.codeActive[26] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code26");
                        StopCoroutine("code27");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode27");
                            StartCoroutine("code27");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 27");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p6eval3:
        eval++; // 3
        code28class.setCode28Info();
        if (setup.codeActive[27] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code27");
                        StopCoroutine("code28");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode28");
                            StartCoroutine("code28");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 28");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p6eval4:
        eval++; // 4
        code29class.setCode29Info();
        if (setup.codeActive[28] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code28");
                        StopCoroutine("code29");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode29");
                            StartCoroutine("code29");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 29");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p6eval5:
        eval++; // 5
        code30class.setCode30Info();
        if (setup.codeActive[29] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code29");
                        StopCoroutine("code30");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode30");
                            StartCoroutine("code30");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 30");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }




        p7eval1:
        config.participantID++;    // next participant
        eval = 1;
        code31class.setCode31Info();
        if (setup.codeActive[30] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code30");
                        StopCoroutine("code31");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode31");
                            StartCoroutine("code31");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 31");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p7eval2:
        eval++; // 2
        code32class.setCode32Info();
        if (setup.codeActive[31] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code31");
                        StopCoroutine("code32");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode32");
                            StartCoroutine("code32");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 32");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p7eval3:
        eval++; // 3
        code33class.setCode33Info();
        if (setup.codeActive[32] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code32");
                        StopCoroutine("code33");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode33");
                            StartCoroutine("code33");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 33");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p7eval4:
        eval++; // 4
        code34class.setCode34Info();
        if (setup.codeActive[33] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code33");
                        StopCoroutine("code34");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode34");
                            StartCoroutine("code34");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 34");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p7eval5:
        eval++; // 5
        code35class.setCode35Info();
        if (setup.codeActive[34] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code34");
                        StopCoroutine("code35");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode35");
                            StartCoroutine("code35");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 35");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }




        p8eval1:
        config.participantID++;    // next participant
        eval = 1;                  // reset to eval1
        code36class.setCode36Info();
        if (setup.codeActive[35] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code35");
                        StopCoroutine("code36");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode36");
                            StartCoroutine("code36");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 36");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p8eval2:
        eval++; // 2
        code37class.setCode37Info();
        if (setup.codeActive[36] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code36");
                        StopCoroutine("code37");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode37");
                            StartCoroutine("code37");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 37");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p8eval3:
        eval++; // 3
        code38class.setCode38Info();
        if (setup.codeActive[37] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code37");
                        StopCoroutine("code38");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode38");
                            StartCoroutine("code38");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 38");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p8eval4:
        eval++; // 4
        code39class.setCode39Info();
        if (setup.codeActive[38] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code38");
                        StopCoroutine("code39");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode39");
                            StartCoroutine("code39");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 39");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }

        p8eval5:
        eval++; // 5
        code40class.setCode40Info();
        if (setup.codeActive[39] && !getCodeStillRunning())
        {
            startingConfigCounter = 1;

            while (startingConfigCounter <= config.numberOfStartingConfigs)
            {
                iterationCounter = 1;
                while (iterationCounter <= config.numberOfIterations)
                {
                    if (getCodeStillRunning())
                    {
                        yield return new WaitForFixedUpdate();
                    }
                    else
                    {
                        StopCoroutine("code39");
                        StopCoroutine("code40");
                        yield return StartCoroutine(waitCoroutine(100));   // provide time for the physics simulation to be established before starting any of this code
                        yield return new WaitWhile(() => turnflag);
                        Debug.Log(codename + "\t" + startingConfigCounter + "\t" + iterationCounter);
                        writeLogEvent(0);
                        resetBoxes();
                        resetRobot(startingConfigCounter);

                        for (int i = 0; i < 100; i++)
                        {
                            yield return new WaitForFixedUpdate();
                        }

                        setIterationStartTime();

                        if (getCodeStillRunning() == false && iterationCounter <= config.numberOfIterations) // just for safety
                        {
                            yield return StartCoroutine(waitCoroutine(100));
                            yield return StartCoroutine("precode40");
                            StartCoroutine("code40");
                            setCodeStillRunning(true);
                        }
                    }
                }
                startingConfigCounter++;
            }
        }
        Debug.Log("waiting for code 40");
        while (getCodeStillRunning())
        {
            yield return new WaitForFixedUpdate();
        }



        motor(0, 0);
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }



    // Use this for initialization
    void Start()
    {
        // initial setup
        documentConfig();
        generateStartingConfigArray(config.numberOfStartingConfigs);
        adjustScale();
        Time.timeScale = config.timeScale;
        initializeCodeActive();
        //StartCoroutine(blinkCoroutine());
        StartCoroutine(codeExe());
    }


    // variables and methods needed for surveilling, documenting and coordinating running the codes 
    static bool redOut = false;                                // these variables save if a box is pushed out.
    static bool greenOut = false;
    static bool blueOut = false;
    static bool robotFellOff = false;
    static bool codeStillRunning = false;
    static float timeRed = -1f;
    static float timeGreen = -1f;
    static float timeBlue = -1f;
    static int summaryCount = 0;
    public static void setRedOut(bool x)                       // these functions can publically set the variables
    {
        redOut = x;
    }
    public static void setGreenOut(bool x)
    {
        greenOut = x;
    }
    public static void setBlueOut(bool x)
    {
        blueOut = x;
    }
    public static void setRobotFellOff(bool x)
    {
        robotFellOff = x;
    }
    public static void setCodeStillRunning(bool x)
    {
        codeStillRunning = x;
    }
    public static bool getRedOut()
    {
        return redOut;
    }
    public static bool getGreenOut()
    {
        return greenOut;
    }
    public static bool getBlueOut()
    {
        return blueOut;
    }
    public static bool getRobotFellOff()
    {
        return robotFellOff;
    }
    public static bool getCodeStillRunning()
    {
        return codeStillRunning;
    }
    public static void setTimeRed(float x)                       // these functions can publically set the variables
    {
        timeRed = x;
    }
    public static void setTimeGreen(float x)                       // these functions can publically set the variables
    {
        timeGreen = x;
    }
    public static void setTimeBlue(float x)                       // these functions can publically set the variables
    {
        timeBlue = x;
    }
    public static float getTimeRed()
    {
        return timeRed;
    }
    public static float getTimeGreen()
    {
        return timeGreen;
    }
    public static float getTimeBlue()
    {
        return timeBlue;
    }
    public static int cubeTouches = 0;
    public static int cardboardTouches = 0;
    public static float fallOffTime = 0f;
    static float distanceDriven = 0f;
    static float turningSum = 0f;
    static float lastTranslateTime = 0f;
    static int success = 0;
    static int timeoutReached = 0;
    static int removedCubes = 0;


    public static bool code1Active = false;
    public static bool code2Active = false;
    public static bool code3Active = false;
    public static bool code4Active = false;
    public static bool code5Active = false;
    public static bool[] codeActive = new bool[40];
    
    void initializeCodeActive()
    {
        for (int i = 0; i < 40; i++)
        {
            codeActive[i] = false;
        }
    }
    
    public static string codename = "";                                       // holds the filename/codename of the code that is currently tested/simulated    
    public static int eval = 1;
    public static bool redblink = false;                               // needs to be set at the beginning af a new code
    public static bool greenblink = false;
    public static bool blueblink = false;
    public static string comments = "";                                // can be used to store comments about this code

    static string logFilePath = config.documentationFilePath + config.participantID.ToString() + "/logFile.txt";
    static string summaryFilePath = config.documentationFilePath + config.participantID.ToString() + "/summaryFile.txt";

    public static float iterationStartTime;                     // time when iteration was started
    public static void setIterationStartTime()
    {
        iterationStartTime = Time.time;
    }
    public static float getIterationTime(float iterationStartTime)
    {
        return Time.time - iterationStartTime;
    }
    public static int iterationCounter = 0;
    static float[,] StartingConfigs = new float[100, 3];               // array that stores randomly generated starting configurations
    public static int startingConfigCounter = 0;

    public static void setCodeInfo(string codeName, bool redBlink, bool greenBlink, bool blueBlink, string comment)
    {
        codename = codeName;
        redblink = redBlink;
        greenblink = greenBlink;
        blueblink = blueBlink;
        comments = comment;
    }

    // fills the array with possible starting conditions
    void generateStartingConfigArray(int numberOfStartingConfigs)
    {
        if (config.numberOfStartingConfigs >= 100)
            Debug.Log("too large number of starting configurations");

        Random.InitState(42);   // a bit of good old hitchhiker's guide, it guarentees to get similar "random" starting coditions each time

        int i = 0;
        while (i <= numberOfStartingConfigs)
        {
            StartingConfigs[i, 0] = config.PlaygroundScaleMultiplier * (9.6f * Random.value - 4.8f);     // x component
            StartingConfigs[i, 1] = config.PlaygroundScaleMultiplier * (6.2f * Random.value - 3.1f);     // y component
            StartingConfigs[i, 2] = Random.value * 360 + 1;                                                  // angle component

            resetBoxes();
            resetRobot(i);
            if (!touchBox())
                i++;
        }
        /*
        int angleperturbation = 0;
        
        //eval1 starting position:
        StartingConfigs[1, 0] = 1.34f;
        StartingConfigs[1, 1] = -3.33f;
        StartingConfigs[1, 2] = 90f + angleperturbation;

        //eval2 starting position:
        StartingConfigs[2, 0] = -0.87f;
        StartingConfigs[2, 1] = 1.34f;
        StartingConfigs[2, 2] = 180f + angleperturbation;

        //eval3 starting position:
        StartingConfigs[3, 0] = -0.87f;
        StartingConfigs[3, 1] = -1.34f;
        StartingConfigs[3, 2] = 0f + angleperturbation;
        
        //eval4 starting position:
        StartingConfigs[4, 0] = 4.61f;
        StartingConfigs[4, 1] = 0.87f;
        StartingConfigs[4, 2] = 270f + angleperturbation;

        //eval5 starting position:
        StartingConfigs[5, 0] = 1.34f;
        StartingConfigs[5, 1] = -2.32f;
        StartingConfigs[5, 2] = 90f + angleperturbation;

        // ---------------------------------

        //6
        StartingConfigs[6, 0] = -4.58f;
        StartingConfigs[6, 1] = 3.29f;
        StartingConfigs[6, 2] = 90f + angleperturbation;

        //7
        StartingConfigs[7, 0] = -4.33f;
        StartingConfigs[7, 1] = 3.29f;
        StartingConfigs[7, 2] = 270f + angleperturbation;

        //8
        StartingConfigs[8, 0] = -0.93f;
        StartingConfigs[8, 1] = 2.83f;
        StartingConfigs[8, 2] = 0f + angleperturbation;

        //9
        StartingConfigs[9, 0] = 4.58f;
        StartingConfigs[9, 1] = 3.29f;
        StartingConfigs[9, 2] = 270f + angleperturbation;

        //10
        StartingConfigs[10, 0] = -5f;
        StartingConfigs[10, 1] = 0.7f;
        StartingConfigs[10, 2] = 0f + angleperturbation;

        //11
        StartingConfigs[11, 0] = 4.37f;
        StartingConfigs[11, 1] = 0.87f;
        StartingConfigs[11, 2] = 90f + angleperturbation;

        //12
        StartingConfigs[12, 0] = -4.61f;
        StartingConfigs[12, 1] = -0.87f;
        StartingConfigs[12, 2] = 90f + angleperturbation;

        //13
        StartingConfigs[13, 0] = -5f;
        StartingConfigs[13, 1] = -1.34f;
        StartingConfigs[13, 2] = 0f + angleperturbation;

        //14
        StartingConfigs[14, 0] = -4.32f;
        StartingConfigs[14, 1] = -1.95f;
        StartingConfigs[14, 2] = 270f + angleperturbation;

        //15
        StartingConfigs[15, 0] = -0.87f;
        StartingConfigs[15, 1] = -0.7f;
        StartingConfigs[15, 2] = 180f + angleperturbation;

        //16
        StartingConfigs[16, 0] = 0.87f;
        StartingConfigs[16, 1] = -0.7f;
        StartingConfigs[16, 2] = 180f + angleperturbation;

        //17
        StartingConfigs[17, 0] = 4.58f;
        StartingConfigs[17, 1] = -3.29f;
        StartingConfigs[17, 2] = 270f + angleperturbation;

        //18
        StartingConfigs[18, 0] = 1.62f;
        StartingConfigs[18, 1] = -3.29f;
        StartingConfigs[18, 2] = 270f + angleperturbation;

        //19
        StartingConfigs[19, 0] = -0.87f;
        StartingConfigs[19, 1] = -3.31f;
        StartingConfigs[19, 2] = 0f + angleperturbation;

        //20
        StartingConfigs[20, 0] = -3f;
        StartingConfigs[20, 1] = -3.31f;
        StartingConfigs[20, 2] = 0f + angleperturbation;
        */
    }
    bool touchBox()             // checks if the robot would touch a box if it would turn on the spot; used to reject starting configurations that are too close to a box.
    {
        // checking if the robot and the box's colliders touch did not work relyably. However, in those positions the boxes moved.
        // Now the idea is to check the distances

        float radiusRobot = 1.2926f * 1.2f * config.RobotScaleMultiplier;   // 1.2 extra factor b/c the initial boxes and robot was smaller than reality
        float radiusBox = 0.849f * 1.2f * config.BoxScaleMultiplier;
        float distanceToBox;

        distanceToBox = Mathf.Abs((GameObject.Find("redBox").transform.position - GameObject.Find("robot").transform.position).magnitude);
        if (distanceToBox < radiusBox + radiusRobot)
            return true;
        distanceToBox = Mathf.Abs((GameObject.Find("greenBox").transform.position - GameObject.Find("robot").transform.position).magnitude);
        if (distanceToBox < radiusBox + radiusRobot)
            return true;
        distanceToBox = Mathf.Abs((GameObject.Find("blueBox").transform.position - GameObject.Find("robot").transform.position).magnitude);
        if (distanceToBox < radiusBox + radiusRobot)
            return true;

        return false;
    }

    void documentConfig()
    {
        StreamWriter sWriter;
        if (!File.Exists(config.documentationFilePath + config.participantID.ToString() + "/config.txt"))
        {
            sWriter = File.CreateText(config.documentationFilePath + config.participantID.ToString() + "/config.txt");
        }
        else
        {
            sWriter = new StreamWriter(config.documentationFilePath + config.participantID.ToString() + "/config.txt", append: true);
        }

        sWriter.WriteLine("participant info:");
        sWriter.WriteLine("participantID = " + config.participantID.ToString());
        sWriter.WriteLine("comments = " + config.comments);
        sWriter.WriteLine("");

        sWriter.WriteLine("simulation info:");
        sWriter.WriteLine("date and time: " + System.DateTime.Now);
        sWriter.WriteLine("numberOfStartingConfigs = " + config.numberOfStartingConfigs.ToString());
        sWriter.WriteLine("numberOfIterations = " + config.numberOfIterations.ToString());
        sWriter.WriteLine("documentationFilePath = " + config.documentationFilePath);
        sWriter.WriteLine("cutofftime = " + config.cutofftime.ToString());
        sWriter.WriteLine("time scale = " + config.timeScale.ToString());
        sWriter.WriteLine("");
        sWriter.WriteLine("PlaygroundScaleMultiplier = " + config.PlaygroundScaleMultiplier);
        sWriter.WriteLine("RedBoxStartX = " + config.RedBoxStartX);
        sWriter.WriteLine("RedBoxStartY = " + config.RedBoxStartY);
        sWriter.WriteLine("GreenBoxStartX = " + config.GreenBoxStartX);
        sWriter.WriteLine("GreenBoxStartY = " + config.GreenBoxStartY);
        sWriter.WriteLine("BlueBoxStartX = " + config.BlueBoxStartX);
        sWriter.WriteLine("BlueBoxStartY = " + config.BlueBoxStartY);
        sWriter.WriteLine("BoxScaleMultiplier =" + config.BoxScaleMultiplier);
        sWriter.WriteLine("RobotScaleMultiplier =" + config.RobotScaleMultiplier);


        sWriter.Close();
    }
    void adjustScale()
    {

        GameObject.Find("white").transform.localScale = new Vector3(config.PlaygroundScaleMultiplier, config.PlaygroundScaleMultiplier, config.PlaygroundScaleMultiplier);
        GameObject.Find("cardboard").transform.localScale = new Vector3(config.PlaygroundScaleMultiplier, config.PlaygroundScaleMultiplier, config.PlaygroundScaleMultiplier);
        GameObject.Find("robot").transform.localScale = new Vector3(config.RobotScaleMultiplier * 0.22f, config.RobotScaleMultiplier * 0.22f, config.RobotScaleMultiplier * 0.22f);
        GameObject.Find("redBox").transform.localScale = new Vector3(config.BoxScaleMultiplier * 1.2f, config.BoxScaleMultiplier * 1.2f, config.BoxScaleMultiplier * 1.2f);     // 1.2 because the boxes need to be 1.2x larger than they were originally entered into the simulation
        GameObject.Find("greenBox").transform.localScale = new Vector3(config.BoxScaleMultiplier * 1.2f, config.BoxScaleMultiplier * 1.2f, config.BoxScaleMultiplier * 1.2f);
        GameObject.Find("blueBox").transform.localScale = new Vector3(config.BoxScaleMultiplier * 1.2f, config.BoxScaleMultiplier * 1.2f, config.BoxScaleMultiplier * 1.2f);
    }
    void resetBoxes()
    {
        GameObject.Find("redBox").transform.position = new Vector3(config.RedBoxStartX, config.RedBoxStartY, 0);
        GameObject.Find("redBox").transform.eulerAngles = new Vector3(0, 0, 0);
        setRedOut(false);
        setTimeRed(-1f);

        GameObject.Find("greenBox").transform.position = new Vector3(config.GreenBoxStartX, config.GreenBoxStartY, 0);
        GameObject.Find("greenBox").transform.eulerAngles = new Vector3(0, 0, 0);
        setGreenOut(false);
        setTimeGreen(-1f);

        GameObject.Find("blueBox").transform.position = new Vector3(config.BlueBoxStartX, config.BlueBoxStartY, 0);
        GameObject.Find("blueBox").transform.eulerAngles = new Vector3(0, 0, 0);
        setBlueOut(false);
        setTimeBlue(-1f);

        cubeTouches = 0;
        colorflag = ColorFlag.Nothing;
        colorBonus = 0;
        colorPenalty = 0;
    }
    void resetRobot(int IDofStartingConfig)
    {
        motor(0, 0);
        GameObject.Find("robot").transform.position = new Vector3(StartingConfigs[IDofStartingConfig, 0], StartingConfigs[IDofStartingConfig, 1], 0);
        GameObject.Find("robot").transform.eulerAngles = new Vector3(0, 0, StartingConfigs[IDofStartingConfig, 2]);

        setRobotFellOff(false);
        cardboardTouches = 0;
        distanceDriven = 0f;
        turningSum = 0f;
        lastTranslateTime = 0f;
        fallOffTime = -1f;
        ExpLibrary.reflectionDownValue = 47;        // always starts on white
        blinkclass.initBlink();                     // initialize the blink array to zero so that the old entries have no influence after the reset
    }
     

    void checkManualAbort()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            resetBoxes();
            resetRobot(startingConfigCounter);
            //startingConfigCounter++;
            Debug.Log("reset: Starting configuration " + startingConfigCounter + " started");
            setIterationStartTime();
        }
    }
    void checkTaskCompleted()
    {
        if((setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut()) || setup.getRobotFellOff() || (Time.time - setup.iterationStartTime) > config.cutofftime)
        {
            if (setup.getRedOut() && setup.getGreenOut() && setup.getBlueOut())
                Debug.Log("task completed: \t" + (Time.time - setup.iterationStartTime));

            if(setup.getRobotFellOff())
                Debug.Log("robot fell off: \t" + (Time.time - setup.iterationStartTime));

            if ((Time.time - setup.iterationStartTime) > config.cutofftime)
                Debug.Log("time out: \t" + (Time.time - setup.iterationStartTime));

            motor(0,0);     // so that the robot cannot fall off while setting up the next iteration

            StopCoroutine(base.waitCoroutine(waitCoroutineArg));    // probably does not work
            StopCoroutine(base.turnCoroutine(turnCoroutineArg1, turnCoroutineArg2));    // probably does not work

            StopCoroutine("precode1");
            StopCoroutine("precode2");
            StopCoroutine("precode3");
            StopCoroutine("precode4");
            StopCoroutine("precode5");
            StopCoroutine("precode6");
            StopCoroutine("precode7");
            StopCoroutine("precode8");
            StopCoroutine("precode9");
            StopCoroutine("precode10");
            StopCoroutine("precode11");
            StopCoroutine("precode12");
            StopCoroutine("precode13");
            StopCoroutine("precode14");
            StopCoroutine("precode15");
            StopCoroutine("precode16");
            StopCoroutine("precode17");
            StopCoroutine("precode18");
            StopCoroutine("precode19");
            StopCoroutine("precode20");
            StopCoroutine("precode21");
            StopCoroutine("precode22");
            StopCoroutine("precode23");
            StopCoroutine("precode24");
            StopCoroutine("precode25");
            StopCoroutine("precode26");
            StopCoroutine("precode27");
            StopCoroutine("precode28");
            StopCoroutine("precode29");
            StopCoroutine("precode30");
            StopCoroutine("precode31");
            StopCoroutine("precode32");
            StopCoroutine("precode33");
            StopCoroutine("precode34");
            StopCoroutine("precode35");
            StopCoroutine("precode36");
            StopCoroutine("precode37");
            StopCoroutine("precode38");
            StopCoroutine("precode39");
            StopCoroutine("precode40");

            StopCoroutine("code1"); 
            StopCoroutine("code2");
            StopCoroutine("code3");
            StopCoroutine("code4");
            StopCoroutine("code5");
            StopCoroutine("code6");
            StopCoroutine("code7");
            StopCoroutine("code8");
            StopCoroutine("code9");
            StopCoroutine("code10");
            StopCoroutine("code11");
            StopCoroutine("code12");
            StopCoroutine("code13");
            StopCoroutine("code14");
            StopCoroutine("code15");
            StopCoroutine("code16");
            StopCoroutine("code17");
            StopCoroutine("code18");
            StopCoroutine("code19");
            StopCoroutine("code20");
            StopCoroutine("code21");
            StopCoroutine("code22");
            StopCoroutine("code23");
            StopCoroutine("code24");
            StopCoroutine("code25");
            StopCoroutine("code26");
            StopCoroutine("code27");
            StopCoroutine("code28");
            StopCoroutine("code29");
            StopCoroutine("code30");
            StopCoroutine("code31");
            StopCoroutine("code32");
            StopCoroutine("code33");
            StopCoroutine("code34");
            StopCoroutine("code35");
            StopCoroutine("code36");
            StopCoroutine("code37");
            StopCoroutine("code38");
            StopCoroutine("code39");
            StopCoroutine("code40");
            
            if(eval != 0)   // dont record test run
            {
                setup.writeIterationSummary();
            }
            
            

            setIterationStartTime();
            setRedOut(false);
            setGreenOut(false);
            setBlueOut(false);
            setup.setRobotFellOff(false);
            setup.setCodeStillRunning(false);
            setup.iterationCounter++;
            
        }
    }

   

    public static void writeIterationSummary()
    {
        /* write into IterationSummary file with Tab seperation about per iteration events:
         * - running number
         * - real life local starting time
         * - participant number
         * - codename that was tested
         * - 2 for indicating that this is simulated data
         * - StartingConfig number
         * - iteration number
         * - starting conditions (x,y,phi)
         * - which lights were set to blink
         * - robot moved?
         * - time until first cube touch
         * - was this the cube that was also pushed out first?
         * - how often was a cube touched
         * - times when cubes are removed
         * - which cube
         * - color bonus
         * - color penalty
         * - overall iteration time
         * - end time of iteration time
         * - reason for end of iteration (task complete, robot fell off, robot stuck, manual abort
         * - code comments
         * */
         
        string localtimedate = System.DateTime.Now.ToString();
        float startingX = StartingConfigs[startingConfigCounter, 0];
        float startingY = StartingConfigs[startingConfigCounter, 1];
        float startingPhi = StartingConfigs[startingConfigCounter, 2];
        float overall = Mathf.Max(getTimeRed(), getTimeGreen(), getTimeBlue());
        float overOverall = overall + colorPenalty - colorBonus;

        if (Mathf.Min(getTimeRed(), getTimeGreen(), getTimeBlue()) == -1f)
        {
            overall = -1f;
            overOverall = -99f;
            success = 0;
        }
        else
        {
            success = 1;
        }

        if ((Time.time - setup.iterationStartTime) >= 400)
            timeoutReached = 1;
        else
            timeoutReached = 0;

        removedCubes = 0;
        if (getTimeRed() != -1)
            removedCubes++;
        if (getTimeGreen() != -1)
            removedCubes++;
        if (getTimeBlue() != -1)
            removedCubes++;




        StreamWriter sWriter;
        if (!File.Exists(summaryFilePath))
        {
            sWriter = File.CreateText(summaryFilePath);
            Debug.Log("Summary File created at: " + summaryFilePath);
            //sWriter.WriteLine("# local date/time participant# eval# codename dataSrcIndicator startingConfig# iteration# startingX startingY startingPhi redblink greenblink blueblink timeRedCube timeGreenCube timeBlueCube colorBonus colorPenalty lastCube overOverall #boxesTouched #cardboardTouches distanceDriven turningSum success? #removedCubes fallOffTime lastMovedTime timoutReached?");
        }
        else
        {
            sWriter = new StreamWriter(summaryFilePath, append: true);
        }

        sWriter.WriteLine(summaryCount + "\t" + localtimedate + "\t" + config.participantID + "\t" + eval + "\t" + codename + "\t" + "2" + "\t" + startingConfigCounter + "\t" + iterationCounter + "\t" + startingX.ToString("F1") + "\t" + startingY.ToString("F1") + "\t" + startingPhi.ToString("F1") + "\t" + redblink + "\t" + greenblink + "\t" + blueblink + "\t" + getTimeRed().ToString("F1") + "\t" + getTimeGreen().ToString("F1") + "\t" + getTimeBlue().ToString("F1") + "\t" + colorBonus + "\t" + colorPenalty + "\t" + overall.ToString("F1") + "\t" + overOverall.ToString("F1") + "\t" + cubeTouches + "\t" + cardboardTouches + "\t" + distanceDriven + "\t" + turningSum + "\t" + success + "\t" + removedCubes + "\t" + fallOffTime + "\t" + lastTranslateTime.ToString("F1") + "\t" + timeoutReached);

        sWriter.Close();
        summaryCount++;
    }
    public static void writeLogEvent(int eventcode)
    {
        /* Tab seperated entries:
         * - codename
         * - starting config
         * - iteration
         * - time in iteration
         * - event code
         *      0 setup initialized
         *      1 robot changed speed
         *      2 white edge reached with refl down sensor
         *      3 cardboard edge reached               
         *      4 red cube touched (initial contact)
         *      5 green cube touched (initial contact)
         *      6 blue cube touched (initial contact)
         *      7 red cube pushed out
         *      8 green cube pushed out
         *      9 blue cube pushed out
         *      
         * - event human readable
         * */

        string eventString = "";

        switch (eventcode)
        {
            case 0: eventString = "setup initialized"; break;
            case 1: eventString = "robot speed changed"; break;
            case 2: eventString = "white edge reached with reflection sensor"; break;
            case 3: eventString = "cardboard edge reached"; break;
            case 4: eventString = "red box touched (initial contact)"; break;
            case 5: eventString = "green box touched (initial contact)"; break;
            case 6: eventString = "blue box touched (initial contact)"; break;
            case 7: eventString = "red cube removed"; break;
            case 8: eventString = "green cube removed"; break;
            case 9: eventString = "blue cube removed"; break;
        }

        StreamWriter sWriter;
        if (!File.Exists(logFilePath))
        {
            sWriter = File.CreateText(logFilePath);
            Debug.Log("append method, File created at: " + logFilePath);
        }
        else
        {
            sWriter = new StreamWriter(logFilePath, append: true);
        }

        sWriter.WriteLine(codename + "\t" + startingConfigCounter + "\t" + iterationCounter + "\t" + getIterationTime(iterationStartTime).ToString("F1") + "\t" + eventcode + "\t" + eventString);
        sWriter.Close();

    }




    int lastSpeedLeft = speedLeft;
    int lastSpeedRight = speedRight;
   
    private void FixedUpdate()
    {
        checkManualAbort();
        checkTaskCompleted();
        
        if(-speedLeft == speedRight)
        {
            if (Mathf.Abs(speedLeft) > 60)
                speedLeft = speedLeft/Mathf.Abs(speedLeft) * 60;
            if (Mathf.Abs(speedRight) > 60)
                speedRight = speedRight / Mathf.Abs(speedRight) * 60;
        }

        // just making a backup of the speed settings to set them back to the original later
        int speedLeftCopy = speedLeft;
        int speedRightCopy = speedRight;

        int maxAccel = 9;
        // to limit acceleration (change of belt speed) to 10 percentage points per fixed Update
        if (speedLeft > lastSpeedLeft + maxAccel)
            speedLeft = lastSpeedLeft + maxAccel;
        if (speedLeft < lastSpeedLeft - maxAccel)
            speedLeft = lastSpeedLeft - maxAccel;

        if (speedRight > lastSpeedRight + maxAccel)
            speedRight = lastSpeedRight + maxAccel;
        if (speedRight < lastSpeedRight - maxAccel)
            speedRight = lastSpeedRight - maxAccel;


        GameObject.Find("robot").transform.Translate(Vector3.up * driveSpeed * (speedLeft + speedRight) / 2 * Time.deltaTime);
        GameObject.Find("robot").transform.Rotate(0f, 0f, (speedRight - speedLeft) * rotationSpeed * Time.deltaTime);

        // remember with which speed the belts were turning for use in next FixedUpdate
        lastSpeedLeft = speedLeft;
        lastSpeedRight = speedRight;

        // to set back to the original speed settings
        speedLeft = speedLeftCopy;
        speedRight = speedRightCopy;

        distanceDriven += Mathf.Abs(driveSpeed * (speedLeft + speedRight));         // overflows at 3.4 * 10^38, should be enough, with larger values I only loose precision but that's fine
        turningSum += Mathf.Abs((speedRight - speedLeft) * rotationSpeed)/100;    // divided by hundred to keep numbers small and I do not loose accuracy in this case        


        if (Mathf.Abs(driveSpeed * (speedLeft + speedRight)) != 0)
            lastTranslateTime = getIterationTime(iterationStartTime);

        
           


        if (Input.GetKey(KeyCode.UpArrow))
        {
            motor(100, 100);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            motor(-100, -100);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            motor(-100, 100);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            motor(100, -100);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            motor(0, 0);
        }
    }

    void Update () {
        
    }
}
