using UnityEngine;
using System.Collections;
public class MyLog : MonoBehaviour
{
    string myLog;
    Queue myLogQueue = new Queue();
    private GUIStyle guiStyle = new GUIStyle(); 

    public Logger myVRLogger;

    private void Start() => Debug.Log("My Log is initiating (...) ");
    
    void OnEnable()
    {
        Application.logMessageReceived += HandleLoggerVR;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLoggerVR;
    }

    private void HandleLoggerVR(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        //string newString = "\n [" + type + "] : " + myLog;
        string newString = "[" + type + "] : " + myLog;

        myLogQueue.Enqueue(newString);
        myVRLogger.AddLoggerText(newString);

        if (type == LogType.Exception)
        {
            newString = "" + stackTrace;
            //newString = " \n " + stackTrace;

            myLogQueue.Enqueue(newString);

            //New looger here
            myVRLogger.AddLoggerText(newString);
        }

        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
    }

    /* Log for Mobile/Desktop applications
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myLog = logString;
        string newString = "\n [" + type + "] : " + myLog;
        myLogQueue.Enqueue(newString);
        if (type == LogType.Exception)
        {
            newString = " \n " + stackTrace;
            myLogQueue.Enqueue(newString);
        }
        myLog = string.Empty;
        foreach (string mylog in myLogQueue)
        {
            myLog += mylog;
        }
        
    void OnGUI()
    {
        guiStyle.normal.textColor = Color.white;
        guiStyle.fontSize = 36; //change the font size
        GUILayout.Label(myLog, guiStyle);

    }*/
}