using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData instance;
    public string playerName;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
