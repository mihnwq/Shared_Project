using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{

    static string[] paths = new string[10];

    public static void initializePaths()
    {
        paths[0] = "/saveFile.fun1";

        paths[1] = "/saveFile.fun2"; 
    }

    public static void save(Player player, int saveID)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + paths[saveID];

        FileStream stream = new FileStream(path, FileMode.Create);

        EntityData data = new EntityData(player);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static EntityData load(int saveID)
    {
        string path = Application.persistentDataPath + paths[saveID];

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Create);

            EntityData data = formatter.Deserialize(stream) as EntityData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("No save file has been found in " + path + "!");

            return null;
        }  

        

       

    }

}
