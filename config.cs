/*This file shall be used to declare all kinds of global variables and methods 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class config : MonoBehaviour {

    // participant info
    public static int participantID = 1;
    public static string comments ="just video making for the presentation";

    // simulation config
    public static int numberOfStartingConfigs = 99;
    public static int numberOfIterations = 1;
    public static string documentationFilePath = "C:/TrollLABS/PhD thesis/results/kybtur/simulation/";
    public static float cutofftime = 50f;         
    public static float timeScale = 1.0f;               

    public static float PlaygroundScaleMultiplier = 1f;
    public static float RedBoxStartX = 3f * PlaygroundScaleMultiplier;
    public static float RedBoxStartY = -0.72f * PlaygroundScaleMultiplier;
    public static float GreenBoxStartX = 0.72f * PlaygroundScaleMultiplier;
    public static float GreenBoxStartY = 2f * PlaygroundScaleMultiplier;
    public static float BlueBoxStartX = -3f * PlaygroundScaleMultiplier;
    public static float BlueBoxStartY = 0.72f * PlaygroundScaleMultiplier;
    public static float BoxScaleMultiplier = 1f * PlaygroundScaleMultiplier;
    public static float RobotScaleMultiplier = 1f * PlaygroundScaleMultiplier;





    // Use this for initialization
    void Awake () {
        if (!Directory.Exists(documentationFilePath + participantID.ToString()))
            Directory.CreateDirectory(documentationFilePath + participantID.ToString() + "/");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
