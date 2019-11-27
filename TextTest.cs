using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class TextTest : MonoBehaviour {

    string writePath;

    /*
    float[,] StartingConfigs = new float[100,3];

    void generateStartingConfigArray(int numberOfStartingConfigs)
    {
        Random.InitState(42);   // a bit of good old hitchhiker's guide, it guarentees to get similar "random" starting coditions each time

        for (int i = 0; i <= numberOfStartingConfigs; i++)
        {
            StartingConfigs[i, 0] = config.PlaygroundScaleMultiplier * (9.6f * Random.value - 4.8f);     // x component
            StartingConfigs[i, 1] = config.PlaygroundScaleMultiplier * (6.2f * Random.value - 3.1f);     // y component
            StartingConfigs[i, 2] = Random.value * 360;                                                  // angle component
            Debug.Log(StartingConfigs[i, 0]);
        }
    }
    
    void resetRobot(int IDofStartingConfig)
    {
        GameObject.Find("robot").transform.position = new Vector3(StartingConfigs[IDofStartingConfig, 0], StartingConfigs[IDofStartingConfig, 1], 0);
        GameObject.Find("robot").transform.eulerAngles = new Vector3(0,0, StartingConfigs[IDofStartingConfig, 2]);
    }
    
    void resetBoxes()
    {        
        GameObject.Find("redBox").transform.position = new Vector3(3f, 0.6f, 0);
        GameObject.Find("redBox").transform.eulerAngles = new Vector3(0, 0, 0);

        GameObject.Find("greenBox").transform.position = new Vector3(0.6f, 3f, 0);
        GameObject.Find("greenBox").transform.eulerAngles = new Vector3(0, 0, 0);

        GameObject.Find("blueBox").transform.position = new Vector3(-3f, 0.6f, 0);          
        GameObject.Find("blueBox").transform.eulerAngles = new Vector3(0, 0, 0);
    }
    */

    void createFile(string filePath, string stringContent)
    {
        StreamWriter sWriter;
        if(!File.Exists(filePath))
        {
            sWriter = File.CreateText(filePath);
            Debug.Log("createFile method, File created at: " + filePath);
        }
        else
        {
            sWriter = new StreamWriter(filePath);
        }

        sWriter.WriteLine(stringContent);
        sWriter.Close();
    }

    void append(string filePath, string stringContent)
    {
        StreamWriter sWriter;
        if (!File.Exists(filePath))
        {
            sWriter = File.CreateText(filePath);
            Debug.Log("append method, File created at: " + filePath);
        }
        else
        {
            sWriter = new StreamWriter(filePath, append: true);
        }

        sWriter.WriteLine(System.DateTime.Now);
        sWriter.Close();
    }

  


    // Use this for initialization
    void Start () {

     
        //GameObject tempRed = GameObject.Find("redBox");
        //resetRobot(1);

        //writePath = Application.dataPath + "/txtFiles/testParticipant.txt";
        //writePath = config.documentationFilePath + config.participantID.ToString() + "/test.txt";
        //createFile(writePath, "Hello World!");


        //string temp = System.DateTime.Now.ToString();
        //append(writePath, temp);
    }
	
	// Update is called once per frame
	void Update () {
        

    }
}
