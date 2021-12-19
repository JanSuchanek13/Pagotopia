using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadMeScript : MonoBehaviour
{
    void CreateText()
    {
        //Path of the file
        string path = Application.dataPath + "/Readme.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "ReadMe \n\n");
        }

        

    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        CreateText(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
