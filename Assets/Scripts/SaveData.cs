using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;

    //variables to save
    public string playerName;
    public string highscoreName;
    public int highscore;

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
        public string highscoreName;
        public int score;
    }

    public void SaveHighscore(int score, string playerName)
    {
        highscore = score;
        highscoreName = playerName;

        DataToSave data = new DataToSave();
        data.score = score;
        data.highscoreName = highscoreName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/tdsSaveData.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/tdsSaveData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            DataToSave data = JsonUtility.FromJson<DataToSave>(json);
            highscore = data.score;
            highscoreName= data.highscoreName;
            
        }
    }
}