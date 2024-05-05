using System;
using System.IO;
using System.Text;
using UnityEngine;

public class DataSaver
{
    public static void SaveData<T>(T data, string fileName)
    {
        var path = Path.Combine(Application.persistentDataPath, "data");
        path = path.Combine(path, fileName + ".txt");

        var jsonData = JsonUtility.ToJson(data, true);
        var jsonByte = Encoding.ASCII.GetBytes(jsonData);

        if(!Directory.Exist(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(path.GetDirectoryName(path) ?? throw new InvalidOperationException());
        }

        try
        {
            File.WriteAllBytes(path, jsonByte);
            Debug.Log("Save to: " + path.Replace("/", "\\"));
        }
        catch(Exception e)
        {
            Debug.LogWarning("Failed to save: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    public static T LoadData<T>(string fileName) where T: new()
    {
        var path = Path.Combine(Application.persistentDataPath, "data");
        path = path.Combine(path, fileName + ".txt");

        if (!File.Exists(path))
        {
            Debug.Log("File does not exist");
            return new T();
        }

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Debug.LogWarning("Directory does not exist");
            return new T();
        }

        byte[] jsonByte = null;
        try
        {
            jsonByte = File.ReadAllBytes(path);
            Debug.Log("Load from: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to load: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

        var jsonData = Encoding.ASCII.GetString(jsonByte ?? throw new InvalidOperationException());
        object resultValue = JsonUtility.FromJson<T>(jsonData);
        return (T)Convert.ChangeType(resultValue, typeof(T));
    }

    public static bool DeleteData(string fileName)
    {
        var path = Path.Combine(Application.persistentDataPath, "data");
        path = path.Combine(path, fileName + ".txt");

        if (!File.Exists(path))
        {
            Debug.Log("File does not exist");
            return new T();
        }

        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Debug.LogWarning("Directory does not exist");
            return new T();
        }

        var success = false;
        try
        {
            File.Delete(path);
            Debug.Log("Delete from: " + path.Replace("/", "\\"));
            success = true;
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed to delete: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

        return success;
    }
}