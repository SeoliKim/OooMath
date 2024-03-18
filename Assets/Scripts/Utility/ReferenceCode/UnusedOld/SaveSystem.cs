
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SaveUser(UserData data) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/user.ooomath";
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, data);
        stream.Close();
    }

    public static UserData LoadUser() {
        string path = Application.persistentDataPath + "/user.ooomath";
        if (File.Exists(path)) {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            UserData data= binaryFormatter.Deserialize(stream) as UserData;
            stream.Close();

            return data;

        } else {
            Debug.LogError("save file not found in: " + path);
            return null;
        }
    }
}
