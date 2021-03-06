using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class testForfold : MonoBehaviour
{
    
    void Awake()
    {
        string path = "./data";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        
    }

    
}
