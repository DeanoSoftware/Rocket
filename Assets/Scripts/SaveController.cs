using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveController
{
    public static void Save(UserData userData)
    {
        var formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.dat";

        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, userData);
        fileStream.Close();
    }

    public static UserData Load()
    {
        string path = Application.persistentDataPath + "/user.dat";
        if (File.Exists(path))
        {
            var formatter = new BinaryFormatter();
            var stream = new FileStream(path, FileMode.Open);

            UserData userData = formatter.Deserialize(stream) as UserData;
            stream.Close();

            return userData;
        }
        else
        {
            //Debug.Log("Save file not found in "+ path);
            return null;
        }
    }
}
