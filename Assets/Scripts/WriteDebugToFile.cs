using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WriteDebugToFile : MonoBehaviour
{
    string fileName = string.Empty;

    private void OnEnable()
    {
        Application.logMessageReceived += Log;  
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }
    // Start is called before the first frame update
    void Start()
    {
        fileName = Application.dataPath + "/LogFile.text";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        TextWriter textWriter = new StreamWriter(fileName, true);
        textWriter.WriteLine("[" + System.DateTime.Now + "]" + logString);
        textWriter.Close();

        //// Log to Player.log
        //System.IO.File.AppendAllText(Application.dataPath + "/Player.log", $"{type}: {logString}\n");

        //// You can also print messages to the console if needed
        //Debug.Log($"{type}: {logString}");

        //// If you want to include stack trace information
        //// Debug.Log($"{type}: {logString}\n{stackTrace}");
    }
}
