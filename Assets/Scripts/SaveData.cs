using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    //variables to save
    public string playerName;
    public int highscore = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighscore();
    }

    public int GetHighscore()
    {
        return highscore;
    }

    [System.Serializable]
    class DataToSave
    {
        public string playerName;
        public int score;
    }

    public void SaveHighscore(int score)
    {
        highscore = score;

        DataToSave data = new DataToSave();
        data.score = score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/tdsSaveData", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/tdsSaveData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DataToSave data = JsonUtility.FromJson<DataToSave>(json);
            highscore = data.score;
        }
    }
}
