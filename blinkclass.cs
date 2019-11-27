using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkclass : MonoBehaviour {

    private static int blinkValue = 0;   // used to get the blink value 
    private int blinkNowRed = 0;     // up to date blink value before the delay
    private int blinkNowGreen = 0;
    private int blinkNowBlue = 0;
    private const float distanceScaling = 9.49223f;
    int i = 0;  // count variable in fixed update
    int delayms = 500;  // delay in ms that the blink value will be "released"
    static int[] blinkArray = new int[100];

    /*
    public static int GetBlinkValue()
    {           
        return blinkValue; 
    }
    */


    int blinkfunc()
    {
        if (GameObject.Find("redBox") != null && setup.redblink)      // red box blink
        {
            Vector3 leftToBox = (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_left").transform.position) / (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude;
            Vector3 rightToBox = (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_right").transform.position) / (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude;

            // angle between robot/sensor and cube (important for cut-off angle)
            float angleLeft = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, leftToBox)) * 360 / (2 * Mathf.PI);
            float angleRight = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, rightToBox)) * 360 / (2 * Mathf.PI);
            float viewAngleFactorLeft = (0.000008f * Mathf.Pow(angleLeft, 4f) + 0.00007f * Mathf.Pow(angleLeft, 3f) - 0.0277f * Mathf.Pow(angleLeft, 2) - 0.1567f * angleLeft + 25.023f) / 24f;
            float viewAngleFactorRight = (0.000007f * Mathf.Pow(angleRight, 4) + 0.00007f * Mathf.Pow(angleRight, 3) - 0.0270f * Mathf.Pow(angleRight, 2) - 0.0891f * angleRight + 26.005f) / 24f;

            // if view angle outside of cut-off angle the polynomial regression from above is inaccurate
            if (angleLeft > 45 || viewAngleFactorLeft < 0)
                viewAngleFactorLeft = 0;
            if (angleRight > 45 || viewAngleFactorRight < 0)
                viewAngleFactorRight = 0;

            // cube orientation relative to the robot/sensor
            float orientation = Mathf.DeltaAngle(GameObject.Find("redBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
            orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);
            float orientationCorrection;
            if (orientation < 30)
                orientationCorrection = 0;
            else
                orientationCorrection = 24 - (0.0338f * Mathf.Pow(orientation, 2f) - 3.0929f * orientation + 87.133f);

            // distance between sensor and cube
            float distanceLeft = (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude * distanceScaling;
            float distanceRight = (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude * distanceScaling;

            // the orientation effect is dependent on the distance to the cube, delta corrects for this
            float delta = 3f * Mathf.Pow(10f, -9f) * Mathf.Pow(distanceLeft, 5f) - Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) + 0.0002f * Mathf.Pow(distanceLeft, 3f) - 0.0144f * Mathf.Pow(distanceLeft, 2f) + 0.4817f * distanceLeft - 4.6028f;

            float distanceCalibrationLeft = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) - 0.0011f * Mathf.Pow(distanceLeft, 3f) + 0.144f * Mathf.Pow(distanceLeft, 2f) - 8.55f * distanceLeft + 200f;
            float distanceCalibrationRight = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceRight, 4f) - 0.0011f * Mathf.Pow(distanceRight, 3f) + 0.144f * Mathf.Pow(distanceRight, 2f) - 8.55f * distanceRight + 200f;
            distanceCalibrationLeft *= 1.5f;        // adjusting for wrong initial calibration which maxed at 120 but when the sensor is pointed at the light the max value is 180 --> 180/120 = 1.5
            distanceCalibrationRight *= 1.5f;
            // Let's put it all together
            // distanceCalibration * viewangle * (24 - cubeorientation) * delta
            float leftBlink = viewAngleFactorLeft * distanceCalibrationLeft * ((24f - orientationCorrection * delta) / 24f);
            float rightBlink = viewAngleFactorRight * distanceCalibrationRight * ((24f - orientationCorrection * delta) / 24f);

            if (leftBlink < 0)
                leftBlink = 0;
            if (rightBlink < 0)
                rightBlink = 0;

            if (leftBlink >= rightBlink)
                blinkNowRed = (int)(leftBlink + 0.5f);
            else
                blinkNowRed = (int)(rightBlink + 0.5f);
        }   // end of if red
        else
            blinkNowRed = 0;

        // I should get headphones that play music into my ears and not into the room so everyone can listen to it even when I am gone. 
        // lol, Carlo, Heikki? Who wrote this ;-)

        if (GameObject.Find("greenBox") != null && setup.greenblink)      // green box blink
        {
            Vector3 leftToBox = (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_left").transform.position) / (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude;
            Vector3 rightToBox = (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_right").transform.position) / (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude;

            // angle between robot/sensor and cube (important for cut-off angle)
            float angleLeft = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, leftToBox)) * 360 / (2 * Mathf.PI);
            float angleRight = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, rightToBox)) * 360 / (2 * Mathf.PI);
            float viewAngleFactorLeft = (0.000008f * Mathf.Pow(angleLeft, 4f) + 0.00007f * Mathf.Pow(angleLeft, 3f) - 0.0277f * Mathf.Pow(angleLeft, 2) - 0.1567f * angleLeft + 25.023f) / 25f;
            float viewAngleFactorRight = (0.000007f * Mathf.Pow(angleRight, 4) + 0.00007f * Mathf.Pow(angleRight, 3) - 0.0270f * Mathf.Pow(angleRight, 2) - 0.0891f * angleRight + 26.005f) / 25f;

            // if view angle outside of cut-off angle the polynomial regression from above is inaccurate
            if (angleLeft > 45 || viewAngleFactorLeft < 0)
                viewAngleFactorLeft = 0;
            if (angleRight > 45 || viewAngleFactorRight < 0)
                viewAngleFactorRight = 0;

            // cube orientation relative to the robot/sensor
            float orientation = Mathf.DeltaAngle(GameObject.Find("greenBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
            orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);
            float orientationCorrection;
            if (orientation < 30)
                orientationCorrection = 0;
            else
                orientationCorrection = 24 - (0.0338f * Mathf.Pow(orientation, 2f) - 3.0929f * orientation + 87.133f);

            // distance between sensor and cube
            float distanceLeft = (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude * 9.49223f;
            float distanceRight = (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude * 9.49223f;

            // the orientation effect is dependent on the distance to the cube, delta corrects for this
            float delta = 3f * Mathf.Pow(10f, -9f) * Mathf.Pow(distanceLeft, 5f) - Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) + 0.0002f * Mathf.Pow(distanceLeft, 3f) - 0.0144f * Mathf.Pow(distanceLeft, 2f) + 0.4817f * distanceLeft - 4.6028f;

            float distanceCalibrationLeft = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) - 0.0011f * Mathf.Pow(distanceLeft, 3f) + 0.144f * Mathf.Pow(distanceLeft, 2f) - 8.55f * distanceLeft + 200f;
            float distanceCalibrationRight = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceRight, 4f) - 0.0011f * Mathf.Pow(distanceRight, 3f) + 0.144f * Mathf.Pow(distanceRight, 2f) - 8.55f * distanceRight + 200f;
            distanceCalibrationLeft *= 1.5f;        // adjusting for wrong initial calibration which maxed at 120 but when the sensor is pointed at the light the max value is 180 --> 180/120 = 1.5
            distanceCalibrationRight *= 1.5f;
            // Let's put it all together
            // distanceCalibration * viewangle * (24 - cubeorientation) * delta
            float leftBlink = viewAngleFactorLeft * distanceCalibrationLeft * ((24f - orientationCorrection * delta) / 24f);
            float rightBlink = viewAngleFactorRight * distanceCalibrationRight * ((24f - orientationCorrection * delta) / 24f);

            if (leftBlink < 0)
                leftBlink = 0;
            if (rightBlink < 0)
                rightBlink = 0;

            if (leftBlink >= rightBlink)
                blinkNowGreen = (int)(leftBlink + 0.5f);
            else
                blinkNowGreen = (int)(rightBlink + 0.5f);
        }   // end of if green
        else
            blinkNowGreen = 0;


        if (GameObject.Find("blueBox") != null && setup.blueblink)      // blue box blink
        {
            // normalized vectors between the sensors and the center of the cube, ultra is used because it is in the middle between the light sensors and has no angle to the cube because it is located at the side of the robot
            Vector3 leftToBox = (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_left").transform.position) / (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude;
            Vector3 rightToBox = (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_right").transform.position) / (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude;
            
            // angle between robot/sensor and cube (important for cut-off angle)
            float angleLeft = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, leftToBox)) * 360 / (2 * Mathf.PI);
            float angleRight = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, rightToBox)) * 360 / (2 * Mathf.PI);
            //float angleUltra = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, ultraToBox)) * 360 / (2 * Mathf.PI);
            float viewAngleFactorLeft = (0.000008f * Mathf.Pow(angleLeft, 4f) + 0.00007f * Mathf.Pow(angleLeft, 3f) - 0.0277f * Mathf.Pow(angleLeft, 2) - 0.1567f * angleLeft + 25.023f) / 25f;
            float viewAngleFactorRight = (0.000007f * Mathf.Pow(angleRight, 4) + 0.00007f * Mathf.Pow(angleRight, 3) - 0.0270f * Mathf.Pow(angleRight, 2) - 0.0891f * angleRight + 26.005f) / 25f;


            // if view angle outside of cut-off angle the polynomial regression from above is inaccurate
            if (angleLeft > 45 || viewAngleFactorLeft < 0)
                viewAngleFactorLeft = 0;
            if (angleRight > 45 || viewAngleFactorRight < 0)
                viewAngleFactorRight = 0;

            // cube orientation relative to the robot/sensor
            float orientation = Mathf.DeltaAngle(GameObject.Find("blueBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
            orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);

            float orientationCorrection;
            if (orientation < 30)
                orientationCorrection = 0;
            else
                orientationCorrection = 24 - (0.0338f * Mathf.Pow(orientation, 2f) - 3.0929f * orientation + 87.133f);


            // distance between sensor and cube
            float distanceLeft = (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude * 9.49223f;
            float distanceRight = (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude * 9.49223f;

            // the orientation effect is dependent on the distance to the cube, delta corrects for this
            float delta = 3f * Mathf.Pow(10f, -9f) * Mathf.Pow(distanceLeft, 5f) - Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) + 0.0002f * Mathf.Pow(distanceLeft, 3f) - 0.0144f * Mathf.Pow(distanceLeft, 2f) + 0.4817f * distanceLeft - 4.6028f;

            float distanceCalibrationLeft = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) - 0.0011f * Mathf.Pow(distanceLeft, 3f) + 0.144f * Mathf.Pow(distanceLeft, 2f) - 8.55f * distanceLeft + 200f;
            float distanceCalibrationRight = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceRight, 4f) - 0.0011f * Mathf.Pow(distanceRight, 3f) + 0.144f * Mathf.Pow(distanceRight, 2f) - 8.55f * distanceRight + 200f;
            distanceCalibrationLeft *= 1.5f;        // adjusting for wrong initial calibration which maxed at 120 but when the sensor is pointed at the light the max value is 180 --> 180/120 = 1.5
            distanceCalibrationRight *= 1.5f;

            // Let's put it all together
            float leftBlink = viewAngleFactorLeft * distanceCalibrationLeft * ((24f - orientationCorrection * delta) / 24f);
            float rightBlink = viewAngleFactorRight * distanceCalibrationRight * ((24f - orientationCorrection * delta) / 24f);


            if (leftBlink < 0)
                leftBlink = 0;
            if (rightBlink < 0)
                rightBlink = 0;

            if (leftBlink >= rightBlink)
                blinkNowBlue = (int)(leftBlink + 0.5f);
            else
                blinkNowBlue = (int)(rightBlink + 0.5f);
        }   // end of if blue
        else
            blinkNowBlue = 0;

        blinkValue = Mathf.Max(blinkNowRed, blinkNowGreen, blinkNowBlue);
        if (blinkValue > 200)
            return 0;
        else
            return blinkValue;
    }

    /*
    IEnumerator blinkCoroutine()
    {
        
        while(true)
        {
            /* concept: calculate the angle between the robot and all three cubes. If this angle is below the cut-off angle for the light sensors of the actual robot
             * then calculate the distance between each blink light and each light sensor.
             * Multiply with the fit from distance dependency (data sheet), view angle and cube orientation in relation to the sensor/robot.
             * Take the larger value over both sensors (because this is how the function is written in the real world). Write the maximum of all blinking cubes to blinknow.
             *

            if (GameObject.Find("redBox") != null && setup.redblink)      // red box blink
            {
                Vector3 leftToBox = (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_left").transform.position) / (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude;
                Vector3 rightToBox = (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_right").transform.position) / (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude;

                // angle between robot/sensor and cube (important for cut-off angle)
                float angleLeft = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, leftToBox)) * 360 / (2 * Mathf.PI);
                float angleRight = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, rightToBox)) * 360 / (2 * Mathf.PI);
                float viewAngleFactorLeft = (0.000008f * Mathf.Pow(angleLeft, 4f) + 0.00007f * Mathf.Pow(angleLeft, 3f) - 0.0277f * Mathf.Pow(angleLeft, 2) - 0.1567f * angleLeft + 25.023f) / 25f;
                float viewAngleFactorRight = (0.000007f * Mathf.Pow(angleRight, 4) + 0.00007f * Mathf.Pow(angleRight, 3) - 0.0270f * Mathf.Pow(angleRight, 2) - 0.0891f * angleRight + 26.005f) / 25f;

                // if view angle outside of cut-off angle the polynomial regression from above is inaccurate
                if (angleLeft > 45 || viewAngleFactorLeft < 0)
                    viewAngleFactorLeft = 0;
                if (angleRight > 45 || viewAngleFactorRight < 0)
                    viewAngleFactorRight = 0;

                // cube orientation relative to the robot/sensor
                float orientation = Mathf.DeltaAngle(GameObject.Find("redBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
                orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);
                float orientationCorrection;
                if (orientation < 30)
                    orientationCorrection = 0;
                else
                    orientationCorrection = 24 - (0.0338f * Mathf.Pow(orientation, 2f) - 3.0929f * orientation + 87.133f);

                // distance between sensor and cube
                float distanceLeft = (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude * distanceScaling;
                float distanceRight = (GameObject.Find("redBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude * distanceScaling;

                // the orientation effect is dependent on the distance to the cube, delta corrects for this
                float delta = 3f * Mathf.Pow(10f, -9f) * Mathf.Pow(distanceLeft, 5f) - Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) + 0.0002f * Mathf.Pow(distanceLeft, 3f) - 0.0144f * Mathf.Pow(distanceLeft, 2f) + 0.4817f * distanceLeft - 4.6028f;

                float distanceCalibrationLeft = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) - 0.0011f * Mathf.Pow(distanceLeft, 3f) + 0.144f * Mathf.Pow(distanceLeft, 2f) - 8.55f * distanceLeft + 200f;
                float distanceCalibrationRight = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceRight, 4f) - 0.0011f * Mathf.Pow(distanceRight, 3f) + 0.144f * Mathf.Pow(distanceRight, 2f) - 8.55f * distanceRight + 200f;
                distanceCalibrationLeft *= 1.5f;        // adjusting for wrong initial calibration which maxed at 120 but when the sensor is pointed at the light the max value is 180 --> 180/120 = 1.5
                distanceCalibrationRight *= 1.5f;
                // Let's put it all together
                // distanceCalibration * viewangle * (24 - cubeorientation) * delta
                float leftBlink = viewAngleFactorLeft * distanceCalibrationLeft * ((24f - orientationCorrection * delta) / 24f);
                float rightBlink = viewAngleFactorRight * distanceCalibrationRight * ((24f - orientationCorrection * delta) / 24f);

                if (leftBlink < 0)
                    leftBlink = 0;
                if (rightBlink < 0)
                    rightBlink = 0;

                if (leftBlink >= rightBlink)
                    blinkNowRed = (int)(leftBlink + 0.5f);
                else
                    blinkNowRed = (int)(rightBlink + 0.5f);
            }   // end of if red
            else
                blinkNowRed = 0;


            if (GameObject.Find("greenBox") != null && setup.greenblink)      // green box blink
            {
                Vector3 leftToBox = (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_left").transform.position) / (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude;
                Vector3 rightToBox = (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_right").transform.position) / (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude;

                // angle between robot/sensor and cube (important for cut-off angle)
                float angleLeft = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, leftToBox)) * 360 / (2 * Mathf.PI);
                float angleRight = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, rightToBox)) * 360 / (2 * Mathf.PI);
                float viewAngleFactorLeft = (0.000008f * Mathf.Pow(angleLeft, 4f) + 0.00007f * Mathf.Pow(angleLeft, 3f) - 0.0277f * Mathf.Pow(angleLeft, 2) - 0.1567f * angleLeft + 25.023f) / 25f;
                float viewAngleFactorRight = (0.000007f * Mathf.Pow(angleRight, 4) + 0.00007f * Mathf.Pow(angleRight, 3) - 0.0270f * Mathf.Pow(angleRight, 2) - 0.0891f * angleRight + 26.005f) / 25f;

                // if view angle outside of cut-off angle the polynomial regression from above is inaccurate
                if (angleLeft > 45 || viewAngleFactorLeft < 0)
                    viewAngleFactorLeft = 0;
                if (angleRight > 45 || viewAngleFactorRight < 0)
                    viewAngleFactorRight = 0;

                // cube orientation relative to the robot/sensor
                float orientation = Mathf.DeltaAngle(GameObject.Find("greenBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
                orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);
                float orientationCorrection;
                if (orientation < 30)
                    orientationCorrection = 0;
                else
                    orientationCorrection = 24 - (0.0338f * Mathf.Pow(orientation, 2f) - 3.0929f * orientation + 87.133f);

                // distance between sensor and cube
                float distanceLeft = (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude * 9.49223f;
                float distanceRight = (GameObject.Find("greenBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude * 9.49223f;

                // the orientation effect is dependent on the distance to the cube, delta corrects for this
                float delta = 3f * Mathf.Pow(10f, -9f) * Mathf.Pow(distanceLeft, 5f) - Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) + 0.0002f * Mathf.Pow(distanceLeft, 3f) - 0.0144f * Mathf.Pow(distanceLeft, 2f) + 0.4817f * distanceLeft - 4.6028f;

                float distanceCalibrationLeft = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) - 0.0011f * Mathf.Pow(distanceLeft, 3f) + 0.144f * Mathf.Pow(distanceLeft, 2f) - 8.55f * distanceLeft + 200f;
                float distanceCalibrationRight = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceRight, 4f) - 0.0011f * Mathf.Pow(distanceRight, 3f) + 0.144f * Mathf.Pow(distanceRight, 2f) - 8.55f * distanceRight + 200f;
                distanceCalibrationLeft *= 1.5f;        // adjusting for wrong initial calibration which maxed at 120 but when the sensor is pointed at the light the max value is 180 --> 180/120 = 1.5
                distanceCalibrationRight *= 1.5f;
                // Let's put it all together
                // distanceCalibration * viewangle * (24 - cubeorientation) * delta
                float leftBlink = viewAngleFactorLeft * distanceCalibrationLeft * ((24f - orientationCorrection * delta) / 24f);
                float rightBlink = viewAngleFactorRight * distanceCalibrationRight * ((24f - orientationCorrection * delta) / 24f);

                if (leftBlink < 0)
                    leftBlink = 0;
                if (rightBlink < 0)
                    rightBlink = 0;

                if (leftBlink >= rightBlink)
                    blinkNowGreen = (int)(leftBlink + 0.5f);
                else
                    blinkNowGreen = (int)(rightBlink + 0.5f);
            }   // end of if green
            else
                blinkNowGreen = 0;


            if (GameObject.Find("blueBox") != null && setup.blueblink)      // blue box blink
            {
                // normalized vectors between the sensors and the center of the cube, ultra is used because it is in the middle between the light sensors and has no angle to the cube because it is located at the side of the robot
                Vector3 leftToBox = (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_left").transform.position) / (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude;
                Vector3 rightToBox = (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_right").transform.position) / (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude;
                Vector3 ultraToBox = (GameObject.Find("blueBox").transform.position - GameObject.Find("ultrasound").transform.position) / (GameObject.Find("blueBox").transform.position - GameObject.Find("ultrasound").transform.position).magnitude;
                
                // angle between robot/sensor and cube (important for cut-off angle)
                float angleLeft = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, leftToBox)) * 360 / (2 * Mathf.PI);
                float angleRight = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, rightToBox)) * 360 / (2 * Mathf.PI);
                //float angleUltra = Mathf.Acos(Vector3.Dot(GameObject.Find("robot").transform.up, ultraToBox)) * 360 / (2 * Mathf.PI);
                float viewAngleFactorLeft = (0.000008f * Mathf.Pow(angleLeft, 4f) + 0.00007f * Mathf.Pow(angleLeft, 3f) - 0.0277f * Mathf.Pow(angleLeft, 2) - 0.1567f * angleLeft + 25.023f) / 25f;
                float viewAngleFactorRight = (0.000007f * Mathf.Pow(angleRight, 4) + 0.00007f * Mathf.Pow(angleRight, 3) - 0.0270f * Mathf.Pow(angleRight, 2) - 0.0891f * angleRight + 26.005f) / 25f;


                // if view angle outside of cut-off angle the polynomial regression from above is inaccurate
                if (angleLeft > 45 || viewAngleFactorLeft < 0)
                    viewAngleFactorLeft = 0;
                if (angleRight > 45 || viewAngleFactorRight < 0)
                    viewAngleFactorRight = 0;

                // cube orientation relative to the robot/sensor
                float orientation = Mathf.DeltaAngle(GameObject.Find("blueBox").transform.eulerAngles.z, GameObject.Find("robot").transform.eulerAngles.z);
                orientation = Mathf.Abs(Mathf.Abs(Mathf.Abs(orientation) % 90 - 45) - 45);
                
                float orientationCorrection;
                if (orientation < 30)
                    orientationCorrection = 0;
                else
                    orientationCorrection = 24 - (0.0338f * Mathf.Pow(orientation, 2f) - 3.0929f * orientation + 87.133f);

                
                // distance between sensor and cube
                float distanceLeft = (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_left").transform.position).magnitude * 9.49223f;
                float distanceRight = (GameObject.Find("blueBox").transform.position - GameObject.Find("colorsensor_right").transform.position).magnitude * 9.49223f;

                // the orientation effect is dependent on the distance to the cube, delta corrects for this
                float delta = 3f * Mathf.Pow(10f, -9f) * Mathf.Pow(distanceLeft, 5f) - Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) + 0.0002f * Mathf.Pow(distanceLeft, 3f) - 0.0144f * Mathf.Pow(distanceLeft, 2f) + 0.4817f * distanceLeft - 4.6028f;

                float distanceCalibrationLeft = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceLeft, 4f) - 0.0011f * Mathf.Pow(distanceLeft, 3f) + 0.144f * Mathf.Pow(distanceLeft, 2f) - 8.55f * distanceLeft + 200f;
                float distanceCalibrationRight = 3f * Mathf.Pow(10f, -6f) * Mathf.Pow(distanceRight, 4f) - 0.0011f * Mathf.Pow(distanceRight, 3f) + 0.144f * Mathf.Pow(distanceRight, 2f) - 8.55f * distanceRight + 200f;
                distanceCalibrationLeft *= 1.5f;        // adjusting for wrong initial calibration which maxed at 120 but when the sensor is pointed at the light the max value is 180 --> 180/120 = 1.5
                distanceCalibrationRight *= 1.5f;
                
                // Let's put it all together
                float leftBlink = viewAngleFactorLeft * distanceCalibrationLeft * ((24f - orientationCorrection * delta) / 24f);
                float rightBlink = viewAngleFactorRight * distanceCalibrationRight * ((24f - orientationCorrection * delta) / 24f);


                if (leftBlink < 0)
                    leftBlink = 0;
                if (rightBlink < 0)
                    rightBlink = 0;

                if (leftBlink >= rightBlink)
                    blinkNowBlue = (int)(leftBlink + 0.5f);
                else
                    blinkNowBlue = (int)(rightBlink + 0.5f);
            }   // end of if blue
            else
                blinkNowBlue = 0;
            
            yield return new WaitForSeconds(0.5f); // delay used to simulate the blink delay caused by the "lock-in amplifier"
            
            
            //update the blink value after the delay
            blinkValue = Mathf.Max(blinkNowRed,blinkNowGreen,blinkNowBlue);
            if (blinkValue > 200)
                ExpLibrary.blink = 0;
            else
                ExpLibrary.blink = blinkValue;
        }
    }
    */

    public static void initBlink()
    {
        //initializing blinkArray
        for (int j = 0; j < 100; j++)
        {
            blinkArray[j] = 0;
        }
    }

	// Use this for initialization
	void Start () {

        //initializing blinkArray
        initBlink();

        //StartCoroutine(blinkCoroutine());
    }

    private void FixedUpdate()
    {
        
        blinkArray[(i + (int)(0.001f * delayms / Time.deltaTime)) % 100] = blinkfunc();
        ExpLibrary.blink = blinkArray[i % 100];
        i++;

        // avoid overflow
        if (i >= 30000)
            i = 0;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
