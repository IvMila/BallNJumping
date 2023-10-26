using UnityEngine;
using System.IO;
public class SaveDataJSON : MonoBehaviour
{

#if UNITY_EDITOR
    private string _saveFilePath => Path.Combine(Application.dataPath, "Saves/{0}.json");
#else
    private string _saveFilePath => Path.Combine(Application.persistentDataPath, "{0}.json");
#endif

    public void SaveDataObjects<T>(T data)
    {
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(string.Format(_saveFilePath, typeof(T).Name), json);
    }

    public T LoadDataObjects<T>()
    {
        var filePath = string.Format(_saveFilePath, typeof(T).Name);
        if(File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        return default;
    }
}
