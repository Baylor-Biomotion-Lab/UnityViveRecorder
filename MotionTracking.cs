using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;

public class MotionTracking : MonoBehaviour
{
    // This lets you edit it from the Unity editor
    // Position/rotation vars
    private float xPos, yPos, zPos, wRot,xRot,yRot,zRot;
    private int clickOn, clickOff;
    public Text modelLocation;
    public Text modelRotation;
    public Text recordText;
    public InputField fileName;
    public Button recordButton, stopButton;
    private List<float> xPosList= new List<float>();
    private List<float> yPosList= new List<float>();
    private List<float> zPosList= new List<float>();
    private List<float> wRotList= new List<float>();
    private List<float> xRotList= new List<float>();
    private List<float> yRotList= new List<float>();
    private List<float> zRotList= new List<float>();
    
    // public Text yPosText;
    // public Text zPosText;
    void Start()
    {
        fileName.text = "test";
        // "Listen" to see if the button was clicked
        recordButton.onClick.AddListener(record);
        stopButton.onClick.AddListener(stopRecord);
        clickOn = 0;
        clickOff = 0;
    }

    // Records the scene
    void record()
    {
        // If the record button is clicked, let me know and assign a variable
        if(clickOn == 0)
        {
            // Button is turned on for the first time
            Debug.Log(Application.dataPath +"/"+fileName.text+".csv");
            recordText.text = "Recording ON";
            clickOn = 1;
            clickOff = 0;
        }

    }
    void stopRecord()
    {
        if(clickOn == 1)
        {
            recordText.text = "Recording STOPPED";
            clickOff = 1;
            clickOn = 0;
            // Output gathered data for kix
            // foreach(float f in xPosList){
            //     Debug.Log("xPos: " + f.ToString("F2"));
            // }
            SavePosData();
            SaveRotData();
            Debug.Log("Recording was stopped");
        }
    }

    void FixedUpdate()
    {
        
        //Access position and rotation of model
        xPos = transform.position.x;
        yPos = transform.position.y;
        zPos = transform.position.z;

        wRot = transform.rotation.w;
        xRot = transform.rotation.x;
        yRot = transform.rotation.y;
        zRot = transform.rotation.z;

        //Output position and rotation of model
        modelLocation.text = "Position: ("+xPos.ToString("F2")+","+yPos.ToString("F2")+","+zPos.ToString("F2")+")";
        modelRotation.text = "Orientation: ("+wRot.ToString("F2")+","+xRot.ToString("F2")+","+yRot.ToString("F2")+","+zRot.ToString("F2")+")";
        // Save position/rotation to array
        if(clickOn==1)
        {
            xPosList.Add(xPos);
            yPosList.Add(yPos);
            zPosList.Add(zPos);

            wRotList.Add(wRot);
            xRotList.Add(xRot);
            yRotList.Add(yRot);
            zRotList.Add(zRot);
        }
        


    }

    // Save the position and orientation data to a CSV 
    void SavePosData()
    {
        int length = xPosList.Count;
        string delimiter = ",";
        StringBuilder sb = new StringBuilder();
        for(int index = 0; index<length; index++){
            sb.AppendLine(string.Join(delimiter, xPosList[index], yPosList[index], zPosList[index]));
        }

        string filePath = Application.dataPath +"/"+fileName.text+"ControlLeftPos.csv";
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    void SaveRotData()
    {
        int length = xRotList.Count;
        string delimiter = ",";
        StringBuilder sb = new StringBuilder();
        for(int index = 0; index<length; index++){
            sb.AppendLine(string.Join(delimiter, wRotList[index], xRotList[index], yRotList[index], zRotList[index]));
        }

        string filePath = Application.dataPath +"/"+fileName.text+"ControlLeftRot.csv";
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

}
