using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PathManager : MonoBehaviour
{
    //public static PathManager instance = null;
    public const string votedPath = "/Lists/Voted/";
    public const string buildingPath = "/Lists/Building/";
    public const string readydPath = "/Lists/Ready/";

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);

            if (!new DirectoryInfo(Application.persistentDataPath + votedPath).Exists)
                Directory.CreateDirectory(Application.persistentDataPath + votedPath);
            if (!new DirectoryInfo(Application.persistentDataPath + buildingPath).Exists)
                Directory.CreateDirectory(Application.persistentDataPath + buildingPath);
            if (!new DirectoryInfo(Application.persistentDataPath + readydPath).Exists)
                Directory.CreateDirectory(Application.persistentDataPath + readydPath);
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
