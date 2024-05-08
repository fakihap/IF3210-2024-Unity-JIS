using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager
{
    public static bool WriteToFile(string path, string file_contents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, path);

        File.WriteAllText(fullPath, file_contents);
        return true;
    }

    public static bool LoadFromFile(string path, out string file_contents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, path);

        file_contents = File.ReadAllText(fullPath);
        return true;
    }
}
