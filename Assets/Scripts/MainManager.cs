using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance { get; private set; }

    public Color teamColor;

    [SerializeField] string jsonDataFile = "savefile.json";
    [SerializeField] string path;

    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
            return;
        }

        path = $"{Application.persistentDataPath}/{jsonDataFile}";
        instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    [System.Serializable]
    class SaveData
    {
        public Color teamColor;
    }

    public void SaveColor()
    {
        SaveData data = new SaveData();
        data.teamColor = teamColor;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(path, json);
    }

    public void LoadColor()
    {
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            teamColor = data.teamColor;
        }
    }
}
